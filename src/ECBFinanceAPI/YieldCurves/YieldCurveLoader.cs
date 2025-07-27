using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

/// <summary>
/// Loads yield curves by retrieving yield curve quotes and parameters, and constructing <see cref="YieldCurve"/> instances.
/// </summary>
public class YieldCurveLoader : IYieldCurveLoader
{
    private static readonly IEnumerable<Maturity> _availableMaturities = GetAvailableMaturities();

    private readonly IYieldCurveQuotesLoader _yieldCurveQuotesLoader;
    private readonly IYieldCurveParametersLoader _yieldCurveParametersLoader;

    public YieldCurveLoader(HttpClient httpClient) : this(new YieldCurveQuotesLoader(httpClient), new YieldCurveParametersLoader(httpClient)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurveLoader"/> class with the specified quote loader and parameters loader.
    /// </summary>
    /// <param name="yieldCurveQuotesLoader">The loader responsible for retrieving yield curve quotes.</param>
    /// <param name="yieldCurveParametersLoader">The loader responsible for retrieving Nelson-Siegel-Svensson parameters.</param>
    public YieldCurveLoader(IYieldCurveQuotesLoader yieldCurveQuotesLoader, IYieldCurveParametersLoader yieldCurveParametersLoader)
    {
        _yieldCurveQuotesLoader = yieldCurveQuotesLoader;
        _yieldCurveParametersLoader = yieldCurveParametersLoader;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurveLoader"/> class with default implementations of the quote and parameters loaders, 
    /// which are <see cref="YieldCurveQuotesLoader"/> and <see cref="YieldCurveParametersLoader"/> respectively.
    /// </summary>
    public YieldCurveLoader() : this(new YieldCurveQuotesLoader(), new YieldCurveParametersLoader()) { }

    /// <inheritdoc/>
    /// <remarks>
    /// If you do not require monthly maturities, consider using <see cref="MaturityFrequency.Yearly"/>
    /// as the <paramref name="maturityFrequency"/> parameter to improve performance.
    /// Yearly frequency reduces the number of maturities processed, which will speed up data loading.
    /// </remarks>
    public async Task<YieldCurve> GetYieldCurveAsync(
        GovernmentBondNominalRating governmentBondNominalRating,
        QuoteType quoteType,
        DateTime date,
        MaturityFrequency maturityFrequency = MaturityFrequency.Monthly) =>
        (await GetYieldCurvesAsync(governmentBondNominalRating, quoteType, date, date, maturityFrequency)).Single();

    /// <inheritdoc/>
    /// <remarks>
    /// If you do not require monthly maturities, consider using <see cref="MaturityFrequency.Yearly"/>
    /// as the <paramref name="maturityFrequency"/> parameter to improve performance.
    /// Yearly frequency reduces the number of maturities processed, which will speed up data loading.
    /// </remarks>
    public async Task<IEnumerable<YieldCurve>> GetYieldCurvesAsync(
        GovernmentBondNominalRating governmentBondNominalRating,
        QuoteType quoteType,
        DateTime startDate,
        DateTime endDate,
        MaturityFrequency maturityFrequency = MaturityFrequency.Monthly)
    {
        if (governmentBondNominalRating is GovernmentBondNominalRating.AAAtoAA && (quoteType is not QuoteType.SpotRate || maturityFrequency is not MaturityFrequency.Monthly))
            throw new NotSupportedException($"The combination of {governmentBondNominalRating}, {quoteType}, and {maturityFrequency} is not supported by ECB. For {governmentBondNominalRating}, only spot rate quotes with monthly maturities are supported.");

        IEnumerable<Maturity> maturities = GetMaturities(maturityFrequency, governmentBondNominalRating);

        IEnumerable<Task<YieldCurveQuoteTimeSeries>> allYieldCurveQuotesTasks = maturities.Select(
            async maturity => await _yieldCurveQuotesLoader.GetYieldCurveQuotesAsync(governmentBondNominalRating, quoteType, maturity, startDate, endDate));

        NelsonSiegelSvenssonParametersTimeSeries? nelsonSiegelSvenssonParametersTimeSeries = governmentBondNominalRating is GovernmentBondNominalRating.AAAtoAA ? null :
            await _yieldCurveParametersLoader.GetYieldCurveNelsonSiegelSvenssonParametersAsync(governmentBondNominalRating, startDate, endDate);

        IEnumerable<(DateTime Date, IEnumerable<YieldCurveQuote> Quotes)> dateYieldCurveQuotesPairs =
            (await Task.WhenAll(allYieldCurveQuotesTasks))
            .SelectMany(x => x.TimeSeriesPoints.Select(y => (y.Date, Quote: new YieldCurveQuote(x.Maturity, y.Observable))))
            .GroupBy(x => x.Date)
            .Select(g => (g.Key, g.Select(x => x.Quote)));

        if (nelsonSiegelSvenssonParametersTimeSeries is null)
        {
            return dateYieldCurveQuotesPairs.Select(
                quotes => new YieldCurve(
                    quotes.Quotes,
                    null,
                    governmentBondNominalRating,
                    quoteType,
                    quotes.Date));
        }

        return nelsonSiegelSvenssonParametersTimeSeries.TimeSeriesPoints.GroupJoin(
            dateYieldCurveQuotesPairs,
            parameters => parameters.Date,
            quotes => quotes.Date,
            (parameters, quotes) => new YieldCurve(
                quotes.Single().Quotes,
                parameters.Observable,
                governmentBondNominalRating,
                quoteType,
                parameters.Date)
            );
    }

    private static IEnumerable<Maturity> GetMaturities(MaturityFrequency maturityFrequency, GovernmentBondNominalRating governmentBondNominalRating)
    {
        return maturityFrequency switch
        {
            MaturityFrequency.Yearly => _availableMaturities.Where(m => m.Months == 0),
            MaturityFrequency.Monthly => governmentBondNominalRating is GovernmentBondNominalRating.AAAtoAA ? _availableMaturities.Where(m => m == new Maturity(0, 3) || m == new Maturity(0, 6)) : _availableMaturities,
            _ => throw new NotImplementedException($"Can't get maturities for maturity frequency {maturityFrequency}.")
        };
    }

    private static IEnumerable<Maturity> GetAvailableMaturities()
    {
        IEnumerable<Maturity> firstYearMaturities = Enumerable.Range(3, 9).Select(month => new Maturity(0, month));

        IEnumerable<int> months = Enumerable.Range(0, 11);
        IEnumerable<int> years = Enumerable.Range(1, 29);
        IEnumerable<Maturity> middleYearMaturities = years.SelectMany(year => months.Select(month => new Maturity(year, month)));

        IEnumerable<Maturity> lastYearMaturities = [new Maturity(30)];

        return firstYearMaturities.Concat(middleYearMaturities).Concat(lastYearMaturities);
    }
}


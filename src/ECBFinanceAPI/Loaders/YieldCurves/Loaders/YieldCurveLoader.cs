using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Models;

namespace ECBFinanceAPI.Loaders.YieldCurves.Loaders;

/// <summary>
/// Loads yield curves by retrieving yield curve quotes and parameters, and constructing <see cref="YieldCurve"/> instances.
/// </summary>
public class YieldCurveLoader : IYieldCurveLoader
{
    private static readonly IEnumerable<Maturity> _availableMaturities = GetAvailableMaturities();

    private readonly IYieldCurveQuoteLoader _quoteLoader;
    private readonly IYieldCurveParameterLoader _parameterLoader;

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurveLoader"/> class with default implementations of the quote and parameter loaders, 
    /// which are <see cref="YieldCurveQuoteLoader"/> and <see cref="YieldCurveParameterLoader"/> respectively.
    /// </summary>
    public YieldCurveLoader() : this(new YieldCurveQuoteLoader(), new YieldCurveParameterLoader()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurveLoader"/> class using the provided <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="httpClient">An <see cref="HttpClient"/> used to perform download requests.</param>
    public YieldCurveLoader(HttpClient httpClient) : this(new YieldCurveQuoteLoader(httpClient), new YieldCurveParameterLoader(httpClient)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurveLoader"/> class with the specified quote loader and parameter loader.
    /// </summary>
    /// <param name="yieldCurveQuoteLoader">The loader responsible for retrieving yield curve quotes.</param>
    /// <param name="yieldCurveParameterLoader">The loader responsible for retrieving Nelson-Siegel-Svensson parameters.</param>
    public YieldCurveLoader(IYieldCurveQuoteLoader yieldCurveQuoteLoader, IYieldCurveParameterLoader yieldCurveParameterLoader)
    {
        _quoteLoader = yieldCurveQuoteLoader;
        _parameterLoader = yieldCurveParameterLoader;
    }

    /// <inheritdoc/>
    public async Task<YieldCurve> GetYieldCurveAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, DateTime date, MaturityFrequency maturityFrequency) =>
        (await GetYieldCurvesAsync(governmentBondNominalRating, quoteType, date, date, maturityFrequency)).Single();

    /// <inheritdoc/>
    public async Task<IEnumerable<YieldCurve>> GetYieldCurvesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, DateTime startDate, DateTime endDate, MaturityFrequency maturityFrequency)
    {
        IEnumerable<Maturity> maturities = GetMaturities(maturityFrequency, governmentBondNominalRating);

        IEnumerable<YieldCurveQuote> yieldCurveQuotes = (await Task.WhenAll(maturities.Select(maturity => _quoteLoader.GetYieldCurveQuotesAsync(governmentBondNominalRating, quoteType, maturity, startDate, endDate)))).SelectMany(x => x);

        if (governmentBondNominalRating is GovernmentBondNominalRating.AAAtoAA)
            return yieldCurveQuotes.GroupBy(g => g.Date, (date, quotes) => new YieldCurve(quotes, null, governmentBondNominalRating, quoteType, date));

        Dictionary<DateTime, NelsonSiegelSvenssonParameters> nelsonSiegelSvenssonParameters = (await _parameterLoader.GetYieldCurveNelsonSiegelSvenssonParametersAsync(governmentBondNominalRating, startDate, endDate)).ToDictionary(x => x.Date, x => x);

        return yieldCurveQuotes.GroupBy(g => g.Date, (date, quotes) => new YieldCurve(quotes, nelsonSiegelSvenssonParameters[date], governmentBondNominalRating, quoteType, date));
    }

    private static IEnumerable<Maturity> GetMaturities(MaturityFrequency maturityFrequency, GovernmentBondNominalRating governmentBondNominalRating)
    {
        return maturityFrequency switch
        {
            MaturityFrequency.Yearly => _availableMaturities.Where(m => m.Months == 0),
            MaturityFrequency.Monthly when governmentBondNominalRating is GovernmentBondNominalRating.AAAtoAA => [new Maturity(0, 3), new Maturity(0, 6)],
            MaturityFrequency.Monthly => _availableMaturities,
            _ => throw new NotImplementedException($"Can't get maturities for maturity frequency {maturityFrequency} and government bond nominal rating {governmentBondNominalRating}.")
        };
    }

    private static IEnumerable<Maturity> GetAvailableMaturities()
    {
        IEnumerable<Maturity> firstYearMaturities = Enumerable.Range(3, 9).Select(month => new Maturity(0, month));

        IEnumerable<int> months = Enumerable.Range(0, 11);
        IEnumerable<int> years = Enumerable.Range(1, 29);
        IEnumerable<Maturity> middleYearMaturities = years.SelectMany(year => months.Select(month => new Maturity(year, month)));

        IEnumerable<Maturity> lastYearMaturities = [new Maturity(30)];

        return [.. firstYearMaturities, .. middleYearMaturities, .. lastYearMaturities];
    }
}


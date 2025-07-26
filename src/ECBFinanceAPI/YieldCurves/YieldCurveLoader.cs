using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public class YieldCurveLoader : IYieldCurveLoader
{
    private static readonly IEnumerable<Maturity> _availableMaturities = GetAvailableMaturities();

    private readonly IYieldCurveQuotesLoader _yieldCurveQuotesLoader;
    private readonly IYieldCurveParametersLoader _yieldCurveParametersLoader;

    public YieldCurveLoader(IYieldCurveQuotesLoader yieldCurveQuotesLoader, IYieldCurveParametersLoader yieldCurveParametersLoader)
    {
        _yieldCurveQuotesLoader = yieldCurveQuotesLoader;
        _yieldCurveParametersLoader = yieldCurveParametersLoader;
    }

    public YieldCurveLoader() : this(new YieldCurveQuotesLoader(), new YieldCurveParametersLoader()) { }

    public async Task<YieldCurve> GetYieldCurveAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, DateTime date, MaturityFrequency maturityFrequency = MaturityFrequency.Monthly) =>
        (await GetYieldCurvesAsync(governemtBondNominalRating, yieldCurveQuoteType, date, date, maturityFrequency)).Single();

    public async Task<IEnumerable<YieldCurve>> GetYieldCurvesAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, DateTime startDate, DateTime endDate, MaturityFrequency maturityFrequency = MaturityFrequency.Monthly)
    {
        IEnumerable<Maturity> maturities = GetMaturities(maturityFrequency);

        IEnumerable<Task<IEnumerable<YieldCurveQuote>>> allYieldCurveQuotesTasks = maturities.Select(async maturity => await _yieldCurveQuotesLoader.GetYieldCurveQuotesAsync(governemtBondNominalRating, yieldCurveQuoteType, maturity, startDate, endDate));
        Task<IEnumerable<NelsonSiegelSvenssonParameters>> yieldCurveParametersTask = _yieldCurveParametersLoader.GetYieldCurveNelsonSiegelSvenssonParametersAsync(governemtBondNominalRating, startDate, endDate);

        IEnumerable<YieldCurveQuote> allYieldCurveQuotes = (await Task.WhenAll(allYieldCurveQuotesTasks)).SelectMany(q => q);
        IEnumerable<NelsonSiegelSvenssonParameters> yieldCurveParameters = await yieldCurveParametersTask;

        return yieldCurveParameters.GroupJoin(
            allYieldCurveQuotes,
            parameters => parameters.Date,
            quotes => quotes.Date,
            (parameters, quotes) => new YieldCurve(quotes, parameters, parameters.Date)
            );
    }

    private static IEnumerable<Maturity> GetMaturities(MaturityFrequency maturityFrequency)
    {
        return maturityFrequency switch
        {
            MaturityFrequency.Yearly => _availableMaturities.Where(m => m.Months == 0),
            MaturityFrequency.Monthly => _availableMaturities,
            _ => throw new NotImplementedException($"Can't get maturities for maturity frequency {maturityFrequency}.")
        };
    }

    private static IEnumerable<Maturity> GetAvailableMaturities()
    {
        IEnumerable<Maturity> firstYearMaturities = [
            new Maturity(0, 3),
            new Maturity(0, 6),
        ];

        IEnumerable<int> months = Enumerable.Range(0, 11);
        IEnumerable<int> years = Enumerable.Range(1, 29);
        IEnumerable<Maturity> middleYearMaturities = years.SelectMany(year => months.Select(month => new Maturity(year, month)));

        IEnumerable<Maturity> lastYearMaturities = [new Maturity(30)];

        return [.. firstYearMaturities, .. middleYearMaturities, .. lastYearMaturities];
    }
}

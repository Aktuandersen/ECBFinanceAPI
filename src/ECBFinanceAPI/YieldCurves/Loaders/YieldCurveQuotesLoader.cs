using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;
using Utilities;

namespace ECBFinanceAPI.YieldCurves.Loaders;

/// <summary>
/// Loads yield curve quotes as a <see cref="YieldCurveQuoteTimeSeries"/>.
/// </summary>
public class YieldCurveQuotesLoader : YieldCurveObservablesLoader, IYieldCurveQuotesLoader
{
    public YieldCurveQuotesLoader() : base() { }
    public YieldCurveQuotesLoader(HttpClient httpClient) : base(httpClient) { }

    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType yieldCurveQuoteType, Maturity maturity) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, yieldCurveQuoteType, maturity, null, null);

    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, quoteType, maturity, startDate, endDate);

    private async Task<IEnumerable<YieldCurveQuote>> DownloadYieldCurveQuotesAsync(
        GovernmentBondNominalRating governmentBondNominalRating,
        QuoteType quoteType,
        Maturity maturity,
        DateTime? startDate,
        DateTime? endDate
        )
    {
        YieldCurveObservablesEndpoint yieldCurveObservablesEndpoint = GetYieldCurveQuotesEndpoint(
            governmentBondNominalRating,
            quoteType,
            maturity,
            startDate,
            endDate);

        return (await DownloadYieldCurveObservablesAsync(yieldCurveObservablesEndpoint))
            .Select(x => new YieldCurveQuote(x.Date, maturity, x.Value * UnityConversionFactors.PercentToDecimal));
    }

    private static YieldCurveObservablesEndpoint GetYieldCurveQuotesEndpoint(
        GovernmentBondNominalRating governmentBondNominalRating,
        QuoteType quoteType,
        Maturity maturity,
        DateTime? startDate = null,
        DateTime? endDate = null
        ) => startDate is null && endDate is null ?
            new YieldCurveObservablesEndpoint(governmentBondNominalRating, quoteType, maturity) :
            new YieldCurveObservablesEndpoint(governmentBondNominalRating, quoteType, maturity, startDate!.Value, endDate!.Value);
}

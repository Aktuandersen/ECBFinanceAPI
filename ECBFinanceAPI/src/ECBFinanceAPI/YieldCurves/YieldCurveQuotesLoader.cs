using ECBFinanceAPI.Utilities;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public class YieldCurveQuotesLoader : YieldCurveObservablesLoader
{
    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, Maturity maturity) =>
        await DownloadYieldCurveQuotesAsync(governemtBondNominalRating, yieldCurveQuoteType, maturity, null, null);

    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, Maturity maturity, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveQuotesAsync(governemtBondNominalRating, yieldCurveQuoteType, maturity, startDate, endDate);

    private async Task<IEnumerable<YieldCurveQuote>> DownloadYieldCurveQuotesAsync(
        GovernemtBondNominalRating governemtBondNominalRating,
        YieldCurveQuoteType yieldCurveQuoteType,
        Maturity maturity,
        DateTime? startDate,
        DateTime? endDate
        )
    {
        YieldCurveObservablesEndpoint yieldCurveObservablesEndpoint = GetYieldCurveQuotesEndpoint(
            governemtBondNominalRating,
            yieldCurveQuoteType,
            maturity,
            startDate,
            endDate);
        return (await DownloadYieldCurveObservablesAsync(yieldCurveObservablesEndpoint))
            .Select(x => new YieldCurveQuote(x.Date, yieldCurveQuoteType, maturity, x.Value * UnityConversionFactors.PercentToDecimal));
    }

    private static YieldCurveObservablesEndpoint GetYieldCurveQuotesEndpoint(
        GovernemtBondNominalRating governemtBondNominalRating,
        YieldCurveQuoteType yieldCurveQuoteType,
        Maturity maturity,
        DateTime? startDate = null,
        DateTime? endDate = null
        ) => startDate is null && endDate is null ?
            new YieldCurveObservablesEndpoint(governemtBondNominalRating, yieldCurveQuoteType, maturity) :
            new YieldCurveObservablesEndpoint(governemtBondNominalRating, yieldCurveQuoteType, maturity, startDate!.Value, endDate!.Value);
}

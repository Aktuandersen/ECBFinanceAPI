using ECBFinanceAPI.Utilities;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public class YieldCurveQuotesLoader : YieldCurveObservablesLoader
{
    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveqQuotesAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, Maturity maturity) =>
        await DownloadYieldCurveQuotesAsync(null, null, governemtBondNominalRating, yieldCurveQuoteType, maturity);

    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveqQuotesAsync(DateTime startDate, DateTime endDate, GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, Maturity maturity) =>
        await DownloadYieldCurveQuotesAsync(startDate: startDate, endDate: endDate, governemtBondNominalRating: governemtBondNominalRating, yieldCurveQuoteType: yieldCurveQuoteType, maturity: maturity);

    private async Task<IEnumerable<YieldCurveQuote>> DownloadYieldCurveQuotesAsync(DateTime? startDate,
        DateTime? endDate,
        GovernemtBondNominalRating governemtBondNominalRating,
        YieldCurveQuoteType yieldCurveQuoteType,
        Maturity maturity)
    {
        Uri uri = GetAPIEndpoint(
            governemtBondNominalRating: governemtBondNominalRating,
            yieldCurveQuoteType: yieldCurveQuoteType,
            maturity: maturity,
            startDate: startDate,
            endDate: endDate);
        return (await DownloadYieldCurveObservablesAsync(uri))
            .Select(x => new YieldCurveQuote(x.Date, maturity, x.Value * UnityConversionFactors.PercentToDecimal));
    }

    private static Uri GetAPIEndpoint(
        GovernemtBondNominalRating governemtBondNominalRating,
        YieldCurveQuoteType yieldCurveQuoteType,
        Maturity maturity,
        DateTime? startDate = null,
        DateTime? endDate = null
        ) => new YieldCurveQuotesUriBuilder()
        .WithStartDate(startDate ?? DateTime.MinValue)
        .WithEndDate(endDate ?? DateTime.MaxValue)
        .WithGovernmentBondNominalRating(governemtBondNominalRating)
        .WithYieldCurveQuote(yieldCurveQuoteType, maturity)
        .Build();
}

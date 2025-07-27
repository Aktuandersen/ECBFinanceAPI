using ECBFinanceAPI.Utilities;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

/// <summary>
/// Loads yield curve quotes as a <see cref="YieldCurveQuoteTimeSeries"/>.
/// </summary>
public class YieldCurveQuotesLoader : YieldCurveObservablesLoader, IYieldCurveQuotesLoader
{
    /// <inheritdoc/>
    public async Task<YieldCurveQuoteTimeSeries> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType yieldCurveQuoteType, Maturity maturity) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, yieldCurveQuoteType, maturity, null, null);

    /// <inheritdoc/>
    public async Task<YieldCurveQuoteTimeSeries> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, quoteType, maturity, startDate, endDate);

    private async Task<YieldCurveQuoteTimeSeries> DownloadYieldCurveQuotesAsync(
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

        IEnumerable<TimeSeriesPoint<double>> timeSeriesPoints = (await DownloadYieldCurveObservablesAsync(yieldCurveObservablesEndpoint)).Select(x => new TimeSeriesPoint<double>(x.Date, x.Value * UnityConversionFactors.PercentToDecimal));

        return new YieldCurveQuoteTimeSeries(timeSeriesPoints, governmentBondNominalRating, maturity, quoteType);
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

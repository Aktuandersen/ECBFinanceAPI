using ECBFinanceAPI.Endpoints;
using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Models;
using Utilities;

namespace ECBFinanceAPI.Loaders.YieldCurves.Loaders;

public class YieldCurveQuoteLoader : Loader, IYieldCurveQuoteLoader
{
    public YieldCurveQuoteLoader() : base() { }

    public YieldCurveQuoteLoader(HttpClient httpClient) : base(httpClient) { }

    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType yieldCurveQuoteType, Maturity maturity) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, yieldCurveQuoteType, maturity, null, null);

    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, quoteType, maturity, startDate, endDate);

    private async Task<IEnumerable<YieldCurveQuote>> DownloadYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime? startDate, DateTime? endDate)
    {
        YieldCurveEndpoint yieldCurveEndpoint = new(governmentBondNominalRating, quoteType, maturity, startDate, endDate);

        return (await DownloadAsync(yieldCurveEndpoint)).Select(q => new YieldCurveQuote(q.Date, maturity, q.Value * UnityConversionFactors.PercentToDecimal));
    }
}

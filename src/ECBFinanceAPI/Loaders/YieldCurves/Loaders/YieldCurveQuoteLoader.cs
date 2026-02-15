using ECBFinanceAPI.Endpoints;
using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Models;
using ECBFinanceAPI.Utilities;

namespace ECBFinanceAPI.Loaders.YieldCurves.Loaders;

/// <summary>
/// Loads yield curve quotes for a specific <see cref="GovernmentBondNominalRating"/>, <see cref="QuoteType"/> and <see cref="Maturity"/>.
/// </summary>
public class YieldCurveQuoteLoader : Loader, IYieldCurveQuoteLoader
{
    /// <summary>
    /// Initializes a new instance of <see cref="YieldCurveQuoteLoader"/> using the default HTTP client behavior.
    /// </summary>
    public YieldCurveQuoteLoader() : base() { }

    /// <summary>
    /// Initializes a new instance of <see cref="YieldCurveQuoteLoader"/> with the provided <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="httpClient">An <see cref="HttpClient"/> used to perform download requests.</param>
    public YieldCurveQuoteLoader(HttpClient httpClient) : base(httpClient) { }

    /// <inheritdoc/>
    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType yieldCurveQuoteType, Maturity maturity) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, yieldCurveQuoteType, maturity, null, null);

    /// <inheritdoc/>
    public async Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveQuotesAsync(governmentBondNominalRating, quoteType, maturity, startDate, endDate);

    private async Task<IEnumerable<YieldCurveQuote>> DownloadYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime? startDate, DateTime? endDate)
    {
        YieldCurveEndpoint yieldCurveEndpoint = new(governmentBondNominalRating, quoteType, maturity, startDate, endDate);

        return (await DownloadAsync(yieldCurveEndpoint)).Select(q => new YieldCurveQuote(q.Date, maturity, q.Value * UnityConversionFactors.PercentToDecimal));
    }
}

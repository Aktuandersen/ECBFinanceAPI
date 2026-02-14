using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves.Loaders;

/// <summary>
/// Defines methods to asynchronously load yield curve quotes over time for a specified <see cref="GovernmentBondNominalRating"/>, <see cref="QuoteType"/>, and <see cref="Maturity"/>.
/// </summary>
public interface IYieldCurveQuotesLoader
{
    /// <summary>
    /// Retrieves the full time series of yield curve quotes for the specified government bond rating, quote type, and maturity.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating(s) of the government bonds.</param>
    /// <param name="quoteType">The type of yield quote.</param>
    /// <param name="maturity">The maturity for which to load the quotes.</param>
    /// <returns>
    /// A <see cref="YieldCurveQuoteTimeSeries"/> containing all available yield curve quotes for the specified parameters.
    /// </returns>
    public Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity);

    /// <summary>
    /// Retrieves a time series of yield curve quotes for the specified government bond rating, quote type, and maturity within a given date range.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating(s) of the government bonds.</param>
    /// <param name="quoteType">The type of yield quote.</param>
    /// <param name="maturity">The maturity for which to load the quotes.</param>
    /// <param name="startDate">The start date of the time series (inclusive).</param>
    /// <param name="endDate">The end date of the time series (inclusive).</param>
    /// <returns>
    /// A <see cref="YieldCurveQuoteTimeSeries"/> containing yield curve quotes observed within the specified date range.
    /// </returns>
    public Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime startDate, DateTime endDate);
}

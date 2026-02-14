using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Models;

namespace ECBFinanceAPI.Loaders.YieldCurves.Loaders;

/// <summary>
/// Defines methods to asynchronously load yield curve quotes over time for a specified
/// <see cref="GovernmentBondNominalRating"/>, <see cref="QuoteType"/>, and <see cref="Maturity"/>.
/// </summary>
public interface IYieldCurveQuoteLoader
{

    /// <summary>
    /// Asynchronously retrieves yield curve quotes for the specified government bond nominal rating,
    /// quote type and maturity across all available dates.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating of the government bonds to load quotes for.</param>
    /// <param name="quoteType">The type of quote to retrieve (for example, spot rate, par rate or instantaneous forward rate).</param>
    /// <param name="maturity">The maturity for which to retrieve quotes.</param>
    /// <returns>A task that resolves to a collection of <see cref="YieldCurveQuote"/> instances for the requested maturity across available dates.</returns>
    public Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity);


    /// <summary>
    /// Asynchronously retrieves yield curve quotes for the specified government bond nominal rating,
    /// quote type and maturity within the inclusive date range defined by <paramref name="startDate"/> and <paramref name="endDate"/>.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating of the government bonds to load quotes for.</param>
    /// <param name="quoteType">The type of quote to retrieve.</param>
    /// <param name="maturity">The maturity for which to retrieve quotes.</param>
    /// <param name="startDate">Inclusive start date of the range to retrieve quotes for.</param>
    /// <param name="endDate">Inclusive end date of the range to retrieve quotes for.</param>
    /// <returns>A task that resolves to a collection of <see cref="YieldCurveQuote"/> instances within the requested date range.</returns>
    public Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime startDate, DateTime endDate);
}

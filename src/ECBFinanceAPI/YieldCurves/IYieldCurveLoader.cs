using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

/// <summary>
/// Defines methods to asynchronously load yield curve data.
/// Implementations retrieve yield curves based on government bond ratings,
/// quote types, dates, and maturity frequencies.
/// </summary>
public interface IYieldCurveLoader
{
    /// <summary>
    /// Asynchronously gets a single yield curve for a specified government bond nominal rating,
    /// quote type, date, and maturity frequency.
    /// </summary>
    /// <param name="governmentBondNominalRating">The government bond nominal rating for which to retrieve the yield curve.</param>
    /// <param name="quoteType">The type of yield curve quote.</param>
    /// <param name="date">The date for which to retrieve the yield curve.</param>
    /// <param name="maturityFrequency">The frequency of maturities on the yield curve. Defaults to <see cref="MaturityFrequency.Monthly"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved <see cref="YieldCurve"/>.</returns>
    Task<YieldCurve> GetYieldCurveAsync(
        GovernmentBondNominalRating governmentBondNominalRating,
        QuoteType quoteType,
        DateTime date,
        MaturityFrequency maturityFrequency = MaturityFrequency.Monthly);

    /// <summary>
    /// Asynchronously gets a collection of yield curves for a specified government bond nominal rating,
    /// quote type, date range, and maturity frequency.
    /// </summary>
    /// <param name="governmentBondNominalRating">The government bond nominal rating for which to retrieve the yield curves.</param>
    /// <param name="quoteType">The type of yield curve quote.</param>
    /// <param name="startDate">The start date of the date range for which to retrieve yield curves.</param>
    /// <param name="endDate">The end date of the date range for which to retrieve yield curves.</param>
    /// <param name="maturityFrequency">The frequency of maturities on the yield curves. Defaults to <see cref="MaturityFrequency.Monthly"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of retrieved <see cref="YieldCurve"/> instances.</returns>
    Task<IEnumerable<YieldCurve>> GetYieldCurvesAsync(
        GovernmentBondNominalRating governmentBondNominalRating,
        QuoteType quoteType,
        DateTime startDate,
        DateTime endDate,
        MaturityFrequency maturityFrequency = MaturityFrequency.Monthly);
}


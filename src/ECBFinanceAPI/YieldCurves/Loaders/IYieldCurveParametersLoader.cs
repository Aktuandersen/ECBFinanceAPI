using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves.Loaders;

/// <summary>
/// Defines methods to asynchronously load Nelson-Siegel-Svensson (NSS) yield curve parameters over time for a specified <see cref="GovernmentBondNominalRating"/>.
/// </summary>
public interface IYieldCurveParametersLoader
{
    /// <summary>
    /// Loads the entire available time series of Nelson-Siegel-Svensson parameters
    /// for the given government bond nominal rating.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating(s) of the government bonds.</param>
    /// <returns>
    /// A <see cref="NelsonSiegelSvenssonParametersTimeSeries"/> containing the historical Nelson-Siegel-Svensson parameters.
    /// </returns>
    public Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(
        GovernmentBondNominalRating governmentBondNominalRating);

    /// <summary>
    /// Loads a time series of Nelson-Siegel-Svensson parameters for the specified
    /// government bond nominal rating, limited to the given date range.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating(s) of the government bonds.</param>
    /// <param name="startDate">The start date of the time series (inclusive).</param>
    /// <param name="endDate">The end date of the time series (inclusive).</param>
    /// <returns>
    /// A <see cref="NelsonSiegelSvenssonParametersTimeSeries"/> containing the Nelson-Siegel-Svensson parameters within the specified date range.
    /// </returns>
    public Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(
        GovernmentBondNominalRating governmentBondNominalRating,
        DateTime startDate,
        DateTime endDate);
}


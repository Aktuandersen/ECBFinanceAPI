using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Models;

namespace ECBFinanceAPI.Loaders.YieldCurves.Loaders;


/// <summary>
/// Defines methods for loading Nelson–Siegel–Svensson (NSS) model parameters for government bond yield curves.
/// Implementations are responsible for retrieving parameter sets for specified ratings and optional date ranges.
/// </summary>
public interface IYieldCurveParameterLoader
{

    /// <summary>
    /// Asynchronously retrieves NSS parameters for the specified government bond nominal rating across all available dates.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating of government bonds to retrieve parameters for.</param>
    /// <returns>A task that resolves to a collection of <see cref="NelsonSiegelSvenssonParameters"/> instances.</returns>
    public Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating);


    /// <summary>
    /// Asynchronously retrieves NSS parameters for the specified government bond nominal rating within the inclusive date range
    /// defined by <paramref name="startDate"/> and <paramref name="endDate"/>.
    /// </summary>
    /// <param name="governmentBondNominalRating">The nominal rating of government bonds to retrieve parameters for.</param>
    /// <param name="startDate">Inclusive start date of the range to retrieve parameters for.</param>
    /// <param name="endDate">Inclusive end date of the range to retrieve parameters for.</param>
    /// <returns>A task that resolves to a collection of <see cref="NelsonSiegelSvenssonParameters"/> instances within the requested date range.</returns>
    public Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating, DateTime startDate, DateTime endDate);
}


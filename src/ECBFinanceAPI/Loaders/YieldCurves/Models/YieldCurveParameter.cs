using ECBFinanceAPI.Loaders.YieldCurves.Enums;

namespace ECBFinanceAPI.Loaders.YieldCurves.Models;

/// <summary>
/// Represents a single Nelson–Siegel–Svensson (NSS) model parameter observation for a specific date
/// and government bond nominal rating.
/// </summary>
/// <param name="Date">The date the parameter value was observed.</param>
/// <param name="GovernmentBondNominalRating">The government bond nominal rating this parameter applies to.</param>
/// <param name="NelsonSiegelSvenssonParameter">The NSS parameter identifier (e.g. Beta0, Tau1).</param>
/// <param name="Value">The numeric value of the parameter.</param>
public record YieldCurveParameter(DateTime Date, GovernmentBondNominalRating GovernmentBondNominalRating, NelsonSiegelSvenssonParameter NelsonSiegelSvenssonParameter, double Value);

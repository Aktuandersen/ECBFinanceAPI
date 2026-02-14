using ECBFinanceAPI.Loaders.YieldCurves.Enums;

namespace ECBFinanceAPI.Loaders.YieldCurves.Models;

/// <summary>
/// Parameters for the Nelson–Siegel–Svensson (NSS) yield curve model.
/// The NSS model uses a combination of level, slope and two curvature components to fit observed yields across maturities.
/// </summary>
/// <param name="Date">The date for which the parameters were estimated or observed.</param>
/// <param name="GovernmentBondNominalRating">The nominal rating of the government bond these parameters apply to.</param>
/// <param name="Beta0">Long-term level of the yield curve (as maturity → ∞).</param>
/// <param name="Beta1">Short-term component; controls the initial slope of the yield curve.</param>
/// <param name="Beta2">Medium-term curvature; introduces a hump or dip at intermediate maturities.</param>
/// <param name="Beta3">Additional long-term curvature to capture more complex shapes at longer maturities.</param>
/// <param name="Tau1">Decay factor for <c>Beta1</c> and <c>Beta2</c>; determines how quickly their effects diminish with maturity (expressed in years).</param>
/// <param name="Tau2">Decay factor for <c>Beta3</c>; controls the influence of the second curvature term over time (expressed in years).</param>
public record NelsonSiegelSvenssonParameters(DateTime Date, GovernmentBondNominalRating GovernmentBondNominalRating, double Beta0, double Beta1, double Beta2, double Beta3, double Tau1, double Tau2);

namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents the set of parameters for the Nelson-Siegel-Svensson (NSS) model
/// used to estimate and describe the shape of a yield curve at a given point in time.
/// </summary>
/// <param name="Date">The date of the yield curve the parameters apply to.</param>
/// <param name="Beta0">The level parameter; determines the long-term interest rate (as time approaches infinity).</param>
/// <param name="Beta1">The slope parameter; influences the short-term interest rate and initial decline of the curve.</param>
/// <param name="Beta2">The curvature parameter; controls the hump-shaped behavior of the curve over medium-term maturities.</param>
/// <param name="Tau1">The decay factor for Beta1 and Beta2; controls where the loading on the slope and curvature reaches maximum effect.</param>
/// <param name="Tau2">The decay factor for the second curvature term in the extended NSS model; adds flexibility for long-term curvature.</param>
public record NelsonSiegelSvenssonParameters(DateTime Date, double Beta0, double Beta1, double Beta2, double Tau1, double Tau2);

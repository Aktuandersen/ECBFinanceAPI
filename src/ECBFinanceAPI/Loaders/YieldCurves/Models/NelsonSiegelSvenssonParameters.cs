using ECBFinanceAPI.Loaders.YieldCurves.Enums;

namespace ECBFinanceAPI.Loaders.YieldCurves.Models;

/// <summary>
/// Represents the parameters of the Nelson-Siegel-Svensson (NSS) model, a widely used model for fitting and describing the yield curve.
/// 
/// <para>• <b>Beta0</b> — Long-term level of the yield curve (as maturity → ∞).</para>
/// <para>• <b>Beta1</b> — Short-term component, controls the initial slope of the yield curve.</para>
/// <para>• <b>Beta2</b> — Medium-term curvature, introduces a hump or dip at intermediate maturities.</para>
/// <para>• <b>Beta3</b> — Additional long-term curvature, allows the model to capture more complex shapes at longer maturities.</para>
/// <para>• <b>Tau1</b> — Decay factor for Beta1 and Beta2, determines how quickly their effects diminish with maturity.</para>
/// <para>• <b>Tau2</b> — Decay factor for Beta3, controls the influence of the second curvature term over time.</para>
/// </summary>
public record NelsonSiegelSvenssonParameters(DateTime Date, GovernmentBondNominalRating GovernmentBondNominalRating, double Beta0, double Beta1, double Beta2, double Beta3, double Tau1, double Tau2);

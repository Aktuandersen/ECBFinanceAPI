using System.Collections.Immutable;

namespace ECBFinanceAPI.Loaders.YieldCurves.Enums;

/// <summary>
/// Identifies a parameter of the Nelson–Siegel–Svensson yield curve model.
/// </summary>
public enum NelsonSiegelSvenssonParameter
{
    /// <summary>
    /// The long-term level of the yield curve (as maturity → ∞).
    /// </summary>
    Beta0,

    /// <summary>
    /// The short-term component that controls the initial slope of the yield curve.
    /// </summary>
    Beta1,

    /// <summary>
    /// The medium-term curvature component which introduces a hump or dip at intermediate maturities.
    /// </summary>
    Beta2,

    /// <summary>
    /// An additional long-term curvature component to capture more complex shapes at longer maturities.
    /// </summary>
    Beta3,

    /// <summary>
    /// Decay factor for <c>Beta1</c> and <c>Beta2</c>, determining how quickly their effects diminish with maturity.
    /// </summary>
    Tau1,

    /// <summary>
    /// Decay factor for <c>Beta3</c>, controlling the influence of the second curvature term over time.
    /// </summary>
    Tau2,
}

internal static class NelsonSiegelSvenssonParameterExtensions
{
    private static readonly ImmutableDictionary<NelsonSiegelSvenssonParameter, string> _nelsonSiegelSvenssonParameterToCode = new Dictionary<NelsonSiegelSvenssonParameter, string>
    {
        {NelsonSiegelSvenssonParameter.Beta0, "BETA0"},
        {NelsonSiegelSvenssonParameter.Beta1, "BETA1"},
        {NelsonSiegelSvenssonParameter.Beta2, "BETA2"},
        {NelsonSiegelSvenssonParameter.Beta3, "BETA3"},
        {NelsonSiegelSvenssonParameter.Tau1, "TAU1"},
        {NelsonSiegelSvenssonParameter.Tau2, "TAU2"},
    }.ToImmutableDictionary();

    public static string ToECBCode(this NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter) => _nelsonSiegelSvenssonParameterToCode[nelsonSiegelSvenssonParameter];
}

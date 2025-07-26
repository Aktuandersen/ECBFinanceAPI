using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum NelsonSiegelSvenssonParameter
{
    Beta0,
    Beta1,
    Beta2,
    Beta3,
    Tau1,
    Tau2,
}

internal static class NelsonSiegelSvenssonParameterMappings
{
    public static readonly ImmutableDictionary<NelsonSiegelSvenssonParameter, string> NelsonSiegelSvenssonParameterToCode = new Dictionary<NelsonSiegelSvenssonParameter, string>
    {
        {NelsonSiegelSvenssonParameter.Beta0, "BETA0"},
        {NelsonSiegelSvenssonParameter.Beta1, "BETA1"},
        {NelsonSiegelSvenssonParameter.Beta2, "BETA2"},
        {NelsonSiegelSvenssonParameter.Beta3, "BETA3"},
        {NelsonSiegelSvenssonParameter.Tau1, "TAU1"},
        {NelsonSiegelSvenssonParameter.Tau2, "TAU2"},
    }.ToImmutableDictionary();
}
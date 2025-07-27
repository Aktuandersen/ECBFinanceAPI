using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum GovernmentBondNominalRating
{
    AAA,
    AllRatings,
    AAAtoAA
}

internal static class GovernmentBondNominalRatingMappings
{
    public static readonly ImmutableDictionary<GovernmentBondNominalRating, string> GovernmentBondNominalRatingToCode = new Dictionary<GovernmentBondNominalRating, string>()
    {
        { GovernmentBondNominalRating.AAA, "G_N_A" },
        { GovernmentBondNominalRating.AllRatings, "G_N_C" },
        { GovernmentBondNominalRating.AAAtoAA, "G_N_W" },
    }.ToImmutableDictionary();
}
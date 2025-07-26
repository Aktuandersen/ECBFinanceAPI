using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum GovernemtBondNominalRating
{
    AAA,
    AllRatings,
    AAAtoAA
}

internal static class GovernemtBondNominalRatingMappings
{
    public static readonly ImmutableDictionary<GovernemtBondNominalRating, string> GovernemtBondNominalRatingToCode = new Dictionary<GovernemtBondNominalRating, string>()
    {
        { GovernemtBondNominalRating.AAA, "G_N_A" },
        { GovernemtBondNominalRating.AllRatings, "G_N_C" },
        { GovernemtBondNominalRating.AAAtoAA, "G_N_W" },
    }.ToImmutableDictionary();
}
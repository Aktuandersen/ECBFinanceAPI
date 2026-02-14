using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum GovernmentBondNominalRating
{
    AAA,
    AllRatings,
    AAAtoAA
}

internal static class GovernmentBondNominalRatingExtensions
{
    private static readonly ImmutableDictionary<GovernmentBondNominalRating, string> _governmentBondNominalRatingToCode = new Dictionary<GovernmentBondNominalRating, string>()
    {
        { GovernmentBondNominalRating.AAA, "G_N_A" },
        { GovernmentBondNominalRating.AllRatings, "G_N_C" },
        { GovernmentBondNominalRating.AAAtoAA, "G_N_W" },
    }.ToImmutableDictionary();

    public static string ToECBCode(this GovernmentBondNominalRating governmentBondNominalRating) => _governmentBondNominalRatingToCode[governmentBondNominalRating];
}
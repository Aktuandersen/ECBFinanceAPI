using System.Collections.Immutable;

namespace ECBFinanceAPI.Loaders.YieldCurves.Enums;

/// <summary>
/// Represents the nominal credit rating buckets used by the ECB when publishing government bond yield data.
/// </summary>
public enum GovernmentBondNominalRating
{
    /// <summary>
    /// AAA-rated government bonds.
    /// </summary>
    AAA,

    /// <summary>
    /// All available ratings.
    /// </summary>
    AllRatings,

    /// <summary>
    /// AAA to AA rating bucket.
    /// </summary>
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

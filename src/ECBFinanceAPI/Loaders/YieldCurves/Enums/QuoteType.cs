using System.Collections.Immutable;

namespace ECBFinanceAPI.Loaders.YieldCurves.Enums;

/// <summary>
/// Specifies the type of quote used when observing or constructing a yield curve.
/// </summary>
public enum QuoteType
{
    /// <summary>
    /// Instantaneous forward rate (short-term forward rate derived from the yield curve).
    /// </summary>
    InstantaneousForwardRate,

    /// <summary>
    /// Par rate (coupon rate for which the bond trades at par given the yield curve).
    /// </summary>
    ParRate,

    /// <summary>
    /// Spot rate (zero-coupon yield for the given maturity).
    /// </summary>
    SpotRate,
}

internal static class QuoteTypeExtensions
{
    private static readonly ImmutableDictionary<QuoteType, string> _quoteTypeToCode = new Dictionary<QuoteType, string>
    {
        {QuoteType.InstantaneousForwardRate, "IF"},
        {QuoteType.ParRate, "PY"},
        {QuoteType.SpotRate, "SR"},
    }.ToImmutableDictionary();

    public static string ToECBCode(this QuoteType quoteType) => _quoteTypeToCode[quoteType];
}

using System.Collections.Immutable;

namespace ECBFinanceAPI.Loaders.YieldCurves.Enums;

public enum QuoteType
{
    InstantaneousForwardRate,
    ParRate,
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
using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum QuoteType
{
    InstantaneousForwardRate,
    ParRate,
    SpotRate,
}

internal static class QuoteTypeMappings
{
    public static readonly ImmutableDictionary<QuoteType, string> QuoteTypeToCode = new Dictionary<QuoteType, string>
    {
        {QuoteType.InstantaneousForwardRate, "IF"},
        {QuoteType.ParRate, "PY"},
        {QuoteType.SpotRate, "SR"},
    }.ToImmutableDictionary();
}
using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum YieldCurveQuoteType
{
    InstantaneousForwardRate,
    ParRate,
    SpotRate,
}

internal static class YieldCurveQuoteTypeMappings
{
    public static readonly ImmutableDictionary<YieldCurveQuoteType, string> YieldCurveQuoteTypeToCode = new Dictionary<YieldCurveQuoteType, string>
    {
        {YieldCurveQuoteType.InstantaneousForwardRate, "IF"},
        {YieldCurveQuoteType.ParRate, "PR"},
        {YieldCurveQuoteType.SpotRate, "SR"},
    }.ToImmutableDictionary();
}
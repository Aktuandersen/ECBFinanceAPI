using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum FinancialMarketProvider
{
    ECB,
    Bloomberg,
    CMA,
    Bundesbank,
    DataStream,
    FitchRatings,
    JPMorgan,
    Moodys,
    Refinitiv,
    StandardAndPoors
}

internal static class FinancialMarketProviderMappings
{
    public static readonly ImmutableDictionary<FinancialMarketProvider, string> FinancialMarketProviderToCode = new Dictionary<FinancialMarketProvider, string>()
    {
        { FinancialMarketProvider.ECB, "4F"},
        { FinancialMarketProvider.Bloomberg, "BL"},
        { FinancialMarketProvider.CMA, "CM"},
        { FinancialMarketProvider.Bundesbank, "DE"},
        { FinancialMarketProvider.DataStream, "DS"},
        { FinancialMarketProvider.FitchRatings, "FI"},
        { FinancialMarketProvider.JPMorgan, "JP"},
        { FinancialMarketProvider.Moodys, "MO"},
        { FinancialMarketProvider.Refinitiv, "RT"},
        { FinancialMarketProvider.StandardAndPoors, "SP" },
    }.ToImmutableDictionary();
}

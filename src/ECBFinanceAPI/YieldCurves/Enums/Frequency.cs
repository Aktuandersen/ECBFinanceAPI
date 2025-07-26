using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Enums;

public enum Frequency
{
    Annual,
    BusinessDaily,
    Daily,
    HalfYearly,
    Monthly,
    Minutely,
    Quarterly,
    Semester,
    Weekly
}

internal static class FrequencyMappings
{
    public static readonly ImmutableDictionary<Frequency, string> FrequencyToCode = new Dictionary<Frequency, string>
    {
        { Frequency.Annual, "A" },
        { Frequency.BusinessDaily, "B" },
        { Frequency.Daily, "D" },
        { Frequency.HalfYearly, "H" },
        { Frequency.Monthly, "M" },
        { Frequency.Minutely, "N" },
        { Frequency.Quarterly, "Q" },
        { Frequency.Semester, "S" },
        { Frequency.Weekly, "W" }
    }.ToImmutableDictionary();
}

using CsvHelper.Configuration.Attributes;

namespace ECBFinanceAPI.Loaders;

internal class ECBData
{
    [Name("TIME_PERIOD")] public DateTime Date { get; init; }
    [Name("OBS_VALUE")] public double Value { get; init; }
}

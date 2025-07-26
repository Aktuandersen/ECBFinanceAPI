using CsvHelper.Configuration.Attributes;

namespace ECBFinanceAPI.YieldCurves.Models;

public record YieldCurveObservable
{
    [Name("TIME_PERIOD")] public DateTime Date { get; init; }
    [Name("OBS_VALUE")] public double Value { get; init; }
}

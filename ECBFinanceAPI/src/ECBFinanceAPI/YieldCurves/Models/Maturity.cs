namespace ECBFinanceAPI.YieldCurves.Models;

public record Maturity
{
    public int Years { get; }
    public int Months { get; }

    public Maturity(int years, int months)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(years, nameof(years));
        ArgumentOutOfRangeException.ThrowIfNegative(months, nameof(months));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(months, 11, nameof(months));

        Years = years;
        Months = months;
    }

    public Maturity(int years) : this(years, 0) { }

    public override string ToString() => $"{Years}Y{(Months == 0 ? string.Empty : $"{Months}M")}";
}

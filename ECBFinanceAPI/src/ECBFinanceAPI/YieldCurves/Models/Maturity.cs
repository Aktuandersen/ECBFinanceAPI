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
        if (years == 0 && months == 0)
            throw new ArgumentException("Years and months can't both be 0.");

        Years = years;
        Months = months;
    }

    public Maturity(int years) : this(years, 0) { }

    public override string ToString() => $"{(Years == 0 ? string.Empty : $"{Years}Y")}{(Months == 0 ? string.Empty : $"{Months}M")}";
}

namespace ECBFinanceAPI.Loaders.YieldCurves.Models;

/// <summary>
/// Represents the maturity of a bond expressed in years and months.
/// </summary>
public record Maturity : IComparable<Maturity>
{
    /// <summary>
    /// Gets the number of whole years in the maturity period.
    /// </summary>
    public int Years { get; }

    /// <summary>
    /// Gets the number of additional months (0–11) in the maturity period.
    /// </summary>
    public int Months { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Maturity"/> record with the specified number of years and months.
    /// </summary>
    /// <param name="Years">The number of years. Must be zero or greater.</param>
    /// <param name="Months">The number of months. Must be between 0 and 11 inclusive.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="Years"/> is negative, or <paramref name="Months"/> is not between 0 and 11.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when both <paramref name="Years"/> and <paramref name="Months"/> are zero,
    /// as a maturity of zero is not valid in a yield curve context.
    /// </exception>
    public Maturity(int Years, int Months)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(Years, nameof(Years));
        ArgumentOutOfRangeException.ThrowIfNegative(Months, nameof(Months));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(Months, 11, nameof(Months));
        if (Years == 0 && Months == 0)
            throw new ArgumentException("Years and months can't both be 0.");

        this.Years = Years;
        this.Months = Months;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Maturity"/> record with only a year component. The months component is set to zero.
    /// </summary>
    /// <param name="Years">The number of years. Must be greater than zero.</param>
    public Maturity(int Years) : this(Years, 0) { }

    /// <summary>
    /// Returns a string representation of the maturity, using the format "{n}Y{m}M".
    /// For example: "5Y6M", "2Y", or "3M".
    /// </summary>
    /// <returns>A string describing the maturity in years and months.</returns>
    public override string ToString() => $"{(Years == 0 ? string.Empty : $"{Years}Y")}{(Months == 0 ? string.Empty : $"{Months}M")}";

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public int CompareTo(Maturity? other) => other is null ? 1 : ToMonths().CompareTo(other.ToMonths());
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    private int ToMonths() => Years * 12 + Months;
}

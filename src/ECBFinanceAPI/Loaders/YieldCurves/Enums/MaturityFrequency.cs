namespace ECBFinanceAPI.Loaders.YieldCurves.Enums;

/// <summary>
/// Specifies how maturities are selected when constructing or retrieving yield curves.
/// </summary>
public enum MaturityFrequency
{
    /// <summary>
    /// Use yearly maturities only (e.g. 1 year, 2 years, ...).
    /// </summary>
    Yearly,

    /// <summary>
    /// Use monthly maturities (includes shorter-term monthly points, e.g. 3 months, 6 months, and monthly up to the maximum term).
    /// </summary>
    Monthly,
}

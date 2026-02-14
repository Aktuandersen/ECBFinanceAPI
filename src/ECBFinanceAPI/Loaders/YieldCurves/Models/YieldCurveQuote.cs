namespace ECBFinanceAPI.Loaders.YieldCurves.Models;

/// <summary>
/// Immutable record representing a single quote on a yield curve.
/// </summary>
/// <param name="Date">The date the quote was observed.</param>
/// <param name="Maturity">The <see cref="Maturity"/> that identifies the term associated with this quote.</param>
/// <param name="Yield">The yield value for the specified maturity (expressed as a decimal, e.g. 0.05 for 5%).</param>
/// <remarks>
/// Instances of this record represent a single point on a yield curve; a collection of these
/// for the same date but different maturities forms a full yield curve observation.
/// </remarks>
public record YieldCurveQuote(DateTime Date, Maturity Maturity, double Yield);

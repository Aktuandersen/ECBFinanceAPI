namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents a single quote on a yield curve,
/// consisting of a specific <see cref="Models.Maturity"/> and the corresponding yield value.
/// </summary>
/// <param name="Maturity">The <see cref="Models.Maturity"/> associated with this yield quote.</param>
/// <param name="Yield">The yield value for the specified maturity.</param>
public record YieldCurveQuote(DateTime Date, Maturity Maturity, double Yield);

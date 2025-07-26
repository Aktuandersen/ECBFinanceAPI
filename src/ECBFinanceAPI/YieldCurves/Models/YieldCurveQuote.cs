using ECBFinanceAPI.YieldCurves.Enums;

namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents a single quote or observation on a yield curve, including its associated metadata such as rating, quote type, maturity, and yield.
/// </summary>
/// <param name="Date">The date of the yield.</param>
/// <param name="GovernemtBondNominalRating">The nominal rating of the government bond the quote refers to.</param>
/// <param name="YieldCurveQuoteType">The type of yield quote, such as zero-coupon yield or par yield.</param>
/// <param name="Maturity">The maturity associated with the yield quote.</param>
/// <param name="Yield">The observed yield value, typically expressed as a decimal (e.g., 0.025 for 2.5%).</param>
public record YieldCurveQuote(DateTime Date, GovernemtBondNominalRating GovernemtBondNominalRating, YieldCurveQuoteType YieldCurveQuoteType, Maturity Maturity, double Yield);

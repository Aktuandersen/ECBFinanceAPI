using ECBFinanceAPI.YieldCurves.Enums;

namespace ECBFinanceAPI.YieldCurves.Models;

public record YieldCurveQuote(DateTime Date, YieldCurveQuoteType YieldCurveQuoteType, Maturity Maturity, double Yield);

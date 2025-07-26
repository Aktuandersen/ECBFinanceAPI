namespace ECBFinanceAPI.YieldCurves.Models;

public record YieldCurveQuote(DateTime Date, Maturity Maturity, double Yield);

namespace ECBFinanceAPI.YieldCurves.Models;

public record NelsonSiegelSvenssonParameters(DateTime Date, double Beta0, double Beta1, double Beta2, double Tau1, double Tau2);

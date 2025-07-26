using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public interface IYieldCurveLoader
{
    public Task<YieldCurve> GetYieldCurveAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, DateTime date, MaturityFrequency maturityFrequency = MaturityFrequency.Monthly);
    public Task<IEnumerable<YieldCurve>> GetYieldCurvesAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, DateTime startDate, DateTime endDate, MaturityFrequency maturityFrequency = MaturityFrequency.Monthly);
}

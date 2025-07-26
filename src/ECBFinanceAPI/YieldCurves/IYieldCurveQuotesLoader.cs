using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public interface IYieldCurveQuotesLoader
{
    public Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, Maturity maturity);
    public Task<IEnumerable<YieldCurveQuote>> GetYieldCurveQuotesAsync(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveQuoteType, Maturity maturity, DateTime startDate, DateTime endDate);
}

using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public interface IYieldCurveParametersLoader
{
    public Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernemtBondNominalRating governemtBondNominalRating);
    public Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernemtBondNominalRating governemtBondNominalRating, DateTime startDate, DateTime endDate);
}

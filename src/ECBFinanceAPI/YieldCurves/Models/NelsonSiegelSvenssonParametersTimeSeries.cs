using ECBFinanceAPI.YieldCurves.Enums;

namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents a <see cref="YieldCurveTimeSeries{T}"/> of <see cref="NelsonSiegelSvenssonParameters"/>.
/// </summary>
public class NelsonSiegelSvenssonParametersTimeSeries : YieldCurveTimeSeries<NelsonSiegelSvenssonParameters>
{
    public NelsonSiegelSvenssonParametersTimeSeries(IEnumerable<TimeSeriesPoint<NelsonSiegelSvenssonParameters>> timeSeriesPoints, GovernmentBondNominalRating governmentBondNominalRating) : base(timeSeriesPoints, governmentBondNominalRating) { }
}

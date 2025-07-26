using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public class YieldCurveParametersLoader : YieldCurveObservablesLoader
{
    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernemtBondNominalRating governemtBondNominalRating) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(governemtBondNominalRating, null, null);

    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernemtBondNominalRating governemtBondNominalRating, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(governemtBondNominalRating, startDate, endDate);

    private async Task<IEnumerable<NelsonSiegelSvenssonParameters>> DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(GovernemtBondNominalRating governemtBondNominalRating, DateTime? startDate, DateTime? endDate)
    {
        Task<IEnumerable<YieldCurveObservable>> beta0Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governemtBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> beta1Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governemtBondNominalRating, NelsonSiegelSvenssonParameter.Beta1, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> beta2Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governemtBondNominalRating, NelsonSiegelSvenssonParameter.Beta2, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> beta3Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governemtBondNominalRating, NelsonSiegelSvenssonParameter.Beta3, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> tau1Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governemtBondNominalRating, NelsonSiegelSvenssonParameter.Tau1, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> tau2Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governemtBondNominalRating, NelsonSiegelSvenssonParameter.Tau2, startDate, endDate));

        await Task.WhenAll([
            beta0Task,
            beta1Task,
            beta2Task,
            beta3Task,
            tau1Task,
            tau2Task
        ]);

        return from beta0 in beta0Task.Result
               join beta1 in beta1Task.Result on beta0.Date equals beta1.Date
               join beta2 in beta2Task.Result on beta0.Date equals beta2.Date
               join beta3 in beta3Task.Result on beta0.Date equals beta3.Date
               join tau1 in tau1Task.Result on beta0.Date equals tau1.Date
               join tau2 in tau2Task.Result on beta0.Date equals tau2.Date
               select new NelsonSiegelSvenssonParameters(
                   beta0.Date,
                   beta0.Value,
                   beta1.Value,
                   beta2.Value,
                   tau1.Value,
                   tau2.Value
               );
    }

    private static YieldCurveObservablesEndpoint GetYieldCurveObservablesEndpoint(
        GovernemtBondNominalRating governemtBondNominalRating,
        NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter,
        DateTime? startDate = null,
        DateTime? endDate = null
        ) => startDate is null && endDate is null ?
            new YieldCurveObservablesEndpoint(governemtBondNominalRating, nelsonSiegelSvenssonParameter) :
            new YieldCurveObservablesEndpoint(governemtBondNominalRating, nelsonSiegelSvenssonParameter, startDate!.Value, endDate!.Value);
}

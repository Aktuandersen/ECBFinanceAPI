using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

public class YieldCurveParametersLoader : YieldCurveObservablesLoader
{
    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernemtBondNominalRating governemtBondNominalRating) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(null, null, governemtBondNominalRating);

    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(DateTime startDate, DateTime endDate, GovernemtBondNominalRating governemtBondNominalRating) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(startDate, endDate, governemtBondNominalRating);

    private async Task<IEnumerable<NelsonSiegelSvenssonParameters>> DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(DateTime? startDate, DateTime? endDate, GovernemtBondNominalRating governemtBondNominalRating)
    {
        Task<IEnumerable<YieldCurveObservable>> beta0Task = DownloadYieldCurveObservablesAsync(GetAPIEndpoint(startDate: startDate, endDate: endDate, governemtBondNominalRating: governemtBondNominalRating, nelsonSiegelSvenssonParameter: NelsonSiegelSvenssonParameter.Beta0));
        Task<IEnumerable<YieldCurveObservable>> beta1Task = DownloadYieldCurveObservablesAsync(GetAPIEndpoint(startDate: startDate, endDate: endDate, governemtBondNominalRating: governemtBondNominalRating, nelsonSiegelSvenssonParameter: NelsonSiegelSvenssonParameter.Beta1));
        Task<IEnumerable<YieldCurveObservable>> beta2Task = DownloadYieldCurveObservablesAsync(GetAPIEndpoint(startDate: startDate, endDate: endDate, governemtBondNominalRating: governemtBondNominalRating, nelsonSiegelSvenssonParameter: NelsonSiegelSvenssonParameter.Beta2));
        Task<IEnumerable<YieldCurveObservable>> beta3Task = DownloadYieldCurveObservablesAsync(GetAPIEndpoint(startDate: startDate, endDate: endDate, governemtBondNominalRating: governemtBondNominalRating, nelsonSiegelSvenssonParameter: NelsonSiegelSvenssonParameter.Beta3));
        Task<IEnumerable<YieldCurveObservable>> tau1Task = DownloadYieldCurveObservablesAsync(GetAPIEndpoint(startDate: startDate, endDate: endDate, governemtBondNominalRating: governemtBondNominalRating, nelsonSiegelSvenssonParameter: NelsonSiegelSvenssonParameter.Tau1));
        Task<IEnumerable<YieldCurveObservable>> tau2Task = DownloadYieldCurveObservablesAsync(GetAPIEndpoint(startDate: startDate, endDate: endDate, governemtBondNominalRating: governemtBondNominalRating, nelsonSiegelSvenssonParameter: NelsonSiegelSvenssonParameter.Tau2));

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

    private static Uri GetAPIEndpoint(
        GovernemtBondNominalRating governemtBondNominalRating,
        NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter,
        DateTime? startDate = null,
        DateTime? endDate = null) =>
        new YieldCurveQuotesUriBuilder()
        .WithStartDate(startDate ?? DateTime.MinValue)
        .WithEndDate(endDate ?? DateTime.MaxValue)
        .WithNelsonSiegelSvenssonParameter(nelsonSiegelSvenssonParameter)
        .WithGovernmentBondNominalRating(governemtBondNominalRating)
        .Build();
}

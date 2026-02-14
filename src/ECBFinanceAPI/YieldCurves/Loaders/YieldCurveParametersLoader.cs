using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves.Loaders;

/// <summary>
/// Loads Nelson-Siegel-Svensson model parameters as a <see cref="NelsonSiegelSvenssonParametersTimeSeries"/>.
/// </summary>
public class YieldCurveParametersLoader : YieldCurveObservablesLoader, IYieldCurveParametersLoader
{
    private static readonly HashSet<GovernmentBondNominalRating> _ratingsWithParameters = [GovernmentBondNominalRating.AAA, GovernmentBondNominalRating.AllRatings];

    public YieldCurveParametersLoader() : base() { }
    public YieldCurveParametersLoader(HttpClient httpClient) : base(httpClient) { }

    /// <inheritdoc/>
    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(governmentBondNominalRating, null, null);

    /// <inheritdoc/>
    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(governmentBondNominalRating, startDate, endDate);

    private async Task<IEnumerable<NelsonSiegelSvenssonParameters>> DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating, DateTime? startDate, DateTime? endDate)
    {
        if (!_ratingsWithParameters.Contains(governmentBondNominalRating))
            throw new NotSupportedException($"Nelson-Siegel-Svensson parameters are only supported by ECB for government bond nominal ratings [{string.Join(", ", _ratingsWithParameters)}], but {governmentBondNominalRating} was provided.");

        Task<IEnumerable<YieldCurveObservable>> beta0Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> beta1Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta1, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> beta2Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta2, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> beta3Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta3, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> tau1Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Tau1, startDate, endDate));
        Task<IEnumerable<YieldCurveObservable>> tau2Task = DownloadYieldCurveObservablesAsync(GetYieldCurveObservablesEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Tau2, startDate, endDate));

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
                       governmentBondNominalRating,
                       beta0.Value,
                       beta1.Value,
                       beta2.Value,
                       beta3.Value,
                       tau1.Value,
                       tau2.Value
                       );
    }

    private static YieldCurveObservablesEndpoint GetYieldCurveObservablesEndpoint(
        GovernmentBondNominalRating governmentBondNominalRating,
        NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter,
        DateTime? startDate = null,
        DateTime? endDate = null
        ) => startDate is null && endDate is null ?
            new YieldCurveObservablesEndpoint(governmentBondNominalRating, nelsonSiegelSvenssonParameter) :
            new YieldCurveObservablesEndpoint(governmentBondNominalRating, nelsonSiegelSvenssonParameter, startDate!.Value, endDate!.Value);
}

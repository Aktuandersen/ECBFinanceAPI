using ECBFinanceAPI.Endpoints;
using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Models;

namespace ECBFinanceAPI.Loaders.YieldCurves.Loaders;

/// <summary>
/// Loads Nelson–Siegel–Svensson (NSS) parameters for government bond yield curves.
/// </summary>
public class YieldCurveParameterLoader : Loader, IYieldCurveParameterLoader
{
    private static readonly HashSet<GovernmentBondNominalRating> _supportedRatings = [GovernmentBondNominalRating.AAA, GovernmentBondNominalRating.AllRatings];

    /// <summary>
    /// Initializes a new instance of <see cref="YieldCurveParameterLoader"/> using the default HTTP client behavior.
    /// </summary>
    public YieldCurveParameterLoader() : base() { }

    /// <summary>
    /// Initializes a new instance of <see cref="YieldCurveParameterLoader"/> with the provided <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="httpClient">An <see cref="HttpClient"/> used to perform download requests.</param>
    public YieldCurveParameterLoader(HttpClient httpClient) : base(httpClient) { }

    /// <inheritdoc/>
    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(governmentBondNominalRating, null, null);

    /// <inheritdoc/>
    public async Task<IEnumerable<NelsonSiegelSvenssonParameters>> GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating, DateTime startDate, DateTime endDate) =>
        await DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(governmentBondNominalRating, startDate, endDate);

    private async Task<IEnumerable<NelsonSiegelSvenssonParameters>> DownloadYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating governmentBondNominalRating, DateTime? startDate, DateTime? endDate)
    {
        if (!_supportedRatings.Contains(governmentBondNominalRating))
            throw new ArgumentException($"Nelson-Siegel-Svensson parameters are only supported by ECB for government bond nominal ratings [{string.Join(", ", _supportedRatings)}]", nameof(governmentBondNominalRating));

        Task<IEnumerable<ECBData>> beta0Task = DownloadAsync(new YieldCurveEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, startDate, endDate));
        Task<IEnumerable<ECBData>> beta1Task = DownloadAsync(new YieldCurveEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta1, startDate, endDate));
        Task<IEnumerable<ECBData>> beta2Task = DownloadAsync(new YieldCurveEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta2, startDate, endDate));
        Task<IEnumerable<ECBData>> beta3Task = DownloadAsync(new YieldCurveEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta3, startDate, endDate));
        Task<IEnumerable<ECBData>> tau1Task = DownloadAsync(new YieldCurveEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Tau1, startDate, endDate));
        Task<IEnumerable<ECBData>> tau2Task = DownloadAsync(new YieldCurveEndpoint(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Tau2, startDate, endDate));

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
}

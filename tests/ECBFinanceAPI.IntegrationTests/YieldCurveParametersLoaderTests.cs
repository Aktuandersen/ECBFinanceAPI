using ECBFinanceAPI.YieldCurves;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;
using Moq;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveParametersLoaderTests
{
    private static readonly HttpClient _httpClient = new();

    private readonly YieldCurveParametersLoader _sut = new(_httpClient);

    [Fact]
    public async Task GetYieldCurveParametersAsync_WithinDays_ReturnsCorrectParameters()
    {
        DateTime startDate = new(2025, 07, 20);
        DateTime endDate = new(2025, 07, 25);

        IEnumerable<TimeSeriesPoint<NelsonSiegelSvenssonParameters>> target = [
            new TimeSeriesPoint<NelsonSiegelSvenssonParameters>(new DateTime(2025, 7, 21), new NelsonSiegelSvenssonParameters(1.105742805, 0.7564117816, -1.3581030431, 6.9992421331, 2.4521471905, 12.2004845728)),
            new TimeSeriesPoint<NelsonSiegelSvenssonParameters>(new DateTime(2025, 7, 22), new NelsonSiegelSvenssonParameters(1.1061056825, 0.7819678976, -1.4235650836, 6.9599742874, 2.3841923894, 12.2070728938)),
            new TimeSeriesPoint<NelsonSiegelSvenssonParameters>(new DateTime(2025, 7, 23), new NelsonSiegelSvenssonParameters(1.1099329528, 0.7816474761, -1.1617577792, 6.8820565167, 2.0559072485, 12.8099441287)),
            new TimeSeriesPoint<NelsonSiegelSvenssonParameters>(new DateTime(2025, 7, 24), new NelsonSiegelSvenssonParameters(1.1172532667, 0.753722585, -1.2307826949, 7.1594531476, 2.6392780028, 12.2082261613)),
        ];

        NelsonSiegelSvenssonParametersTimeSeries result = await _sut.GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating.AAA, startDate, endDate);

        Assert.Equal(target, result.TimeSeriesPoints);
    }

    [Fact]
    public async Task GetYieldCurveParametersAsync_AAAtoAA_ThrowsNotSupportedException()
    {
        await Assert.ThrowsAsync<NotSupportedException>(() => _sut.GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating.AAAtoAA));
    }

    [Fact]
    public async Task GetYieldCurveParametersAsync_AAAtoAAWithinDates_ThrowsNotSupportedException()
    {
        await Assert.ThrowsAsync<NotSupportedException>(() => _sut.GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating.AAAtoAA, It.IsAny<DateTime>(), It.IsAny<DateTime>()));
    }
}
using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Loaders;
using ECBFinanceAPI.Loaders.YieldCurves.Models;
using Moq;

namespace ECBFinanceAPI.IntegrationTests;

[Collection("IntegrationTests")]
public class YieldCurveParameterLoaderTests
{
    private readonly HttpClient _client;

    public YieldCurveParameterLoaderTests(HttpClientFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task GetYieldCurveParametersAsync_WithinDays_ReturnsCorrectParameters()
    {
        // Arrange
        GovernmentBondNominalRating governmentBondNominalRating = GovernmentBondNominalRating.AAA;
        DateTime startDate = new(2025, 07, 20);
        DateTime endDate = new(2025, 07, 25);

        IEnumerable<NelsonSiegelSvenssonParameters> target = [
            new NelsonSiegelSvenssonParameters(new DateTime(2025, 7, 21), governmentBondNominalRating, 1.105742805, 0.7564117816, -1.3581030431, 6.9992421331, 2.4521471905, 12.2004845728),
            new NelsonSiegelSvenssonParameters(new DateTime(2025, 7, 22), governmentBondNominalRating, 1.1061056825, 0.7819678976, -1.4235650836, 6.9599742874, 2.3841923894, 12.2070728938),
            new NelsonSiegelSvenssonParameters(new DateTime(2025, 7, 23), governmentBondNominalRating, 1.1099329528, 0.7816474761, -1.1617577792, 6.8820565167, 2.0559072485, 12.8099441287),
            new NelsonSiegelSvenssonParameters(new DateTime(2025, 7, 24), governmentBondNominalRating, 1.1172532667, 0.753722585, -1.2307826949, 7.1594531476, 2.6392780028, 12.2082261613),
            new NelsonSiegelSvenssonParameters(new DateTime(2025, 7, 25), governmentBondNominalRating, 1.1441523197, 0.7563860093, -1.187131076, 7.1627403875, 2.6487263413, 12.1914444074),
        ];

        YieldCurveParameterLoader sut = new(_client);

        // Act
        IEnumerable<NelsonSiegelSvenssonParameters> result = await sut.GetYieldCurveNelsonSiegelSvenssonParametersAsync(governmentBondNominalRating, startDate, endDate);

        // Assert
        Assert.Equal(target, result);
    }

    [Fact]
    public async Task GetNelsonSiegelSvenssonParameterAsync_WithinDays_ReturnsCorrectParameters()
    {
        // Arrange
        GovernmentBondNominalRating governmentBondNominalRating = GovernmentBondNominalRating.AAA;
        DateTime startDate = new(2025, 07, 20);
        DateTime endDate = new(2025, 07, 25);

        IEnumerable<YieldCurveParameter> target = [
            new YieldCurveParameter(new DateTime(2025, 7, 21), governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, 1.105742805),
            new YieldCurveParameter(new DateTime(2025, 7, 22), governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, 1.1061056825),
            new YieldCurveParameter(new DateTime(2025, 7, 23), governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, 1.1099329528),
            new YieldCurveParameter(new DateTime(2025, 7, 24), governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, 1.1172532667),
            new YieldCurveParameter(new DateTime(2025, 7, 25), governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, 1.1441523197),
        ];

        YieldCurveParameterLoader sut = new();

        // Act
        IEnumerable<YieldCurveParameter> result = await sut.GetNelsonSiegelSvenssonParameterAsync(governmentBondNominalRating, NelsonSiegelSvenssonParameter.Beta0, startDate, endDate);

        // Assert
        Assert.Equal(target, result);
    }

    [Fact]
    public async Task GetYieldCurveParametersAsync_AAAtoAA_ThrowsNotSupportedException()
    {
        YieldCurveParameterLoader sut = new(_client);

        await Assert.ThrowsAsync<ArgumentException>(() => sut.GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating.AAAtoAA));
    }

    [Fact]
    public async Task GetYieldCurveParametersAsync_AAAtoAAWithinDates_ThrowsNotSupportedException()
    {
        YieldCurveParameterLoader sut = new(_client);

        await Assert.ThrowsAsync<ArgumentException>(() => sut.GetYieldCurveNelsonSiegelSvenssonParametersAsync(GovernmentBondNominalRating.AAAtoAA, It.IsAny<DateTime>(), It.IsAny<DateTime>()));
    }

    [Fact]
    public async Task GetNelsonSiegelSvenssonParameterAsync_AAAtoAA_ThrowsNotSupportedException()
    {
        YieldCurveParameterLoader sut = new();

        await Assert.ThrowsAsync<ArgumentException>(() => sut.GetNelsonSiegelSvenssonParameterAsync(GovernmentBondNominalRating.AAAtoAA, It.IsAny<NelsonSiegelSvenssonParameter>()));
    }

    [Fact]
    public async Task GetNelsonSiegelSvenssonParameterAsync_AAAtoAAWithinDates_ThrowsNotSupportedException()
    {
        YieldCurveParameterLoader sut = new();

        await Assert.ThrowsAsync<ArgumentException>(() => sut.GetNelsonSiegelSvenssonParameterAsync(GovernmentBondNominalRating.AAAtoAA, It.IsAny<NelsonSiegelSvenssonParameter>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()));
    }
}
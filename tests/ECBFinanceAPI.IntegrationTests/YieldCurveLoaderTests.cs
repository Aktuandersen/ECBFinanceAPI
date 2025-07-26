using ECBFinanceAPI.YieldCurves;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveLoaderTests
{
    [Fact]
    public async Task GetYieldCurveAsync_ReturnsCorrectCurve()
    {
        YieldCurveLoader sut = new();

        List<YieldCurve> result = (await sut.GetYieldCurves(
            GovernemtBondNominalRating.AllRatings,
            YieldCurveQuoteType.SpotRate,
            new DateTime(2025, 7, 1),
            new DateTime(2025, 7, 31),
            MaturityFrequency.Yearly
        )).ToList();

        Assert.NotNull(result);
    }
}
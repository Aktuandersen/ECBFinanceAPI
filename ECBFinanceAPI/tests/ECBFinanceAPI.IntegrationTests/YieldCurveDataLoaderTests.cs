using ECBFinanceAPI.YieldCurves;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveDataLoaderTests
{
    [Fact]
    public async Task Test1()
    {
        YieldCurveQuotesLoader sut = new();

        IEnumerable<YieldCurveQuote> result = await sut.GetYieldCurveqQuotesAsync(GovernemtBondNominalRating.AllRatings, YieldCurveQuoteType.SpotRate, new Maturity(10));

        Assert.NotEmpty(result);
    }
}
using ECBFinanceAPI.YieldCurves;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveQuotesLoaderTests
{
    [Fact]
    public async Task GetYieldCurveQuotesAsync_WithinDays_ReturnsCorrectQuotes()
    {
        GovernemtBondNominalRating governemtBondNominalRating = GovernemtBondNominalRating.AllRatings;
        YieldCurveQuoteType yieldCurveQuoteType = YieldCurveQuoteType.SpotRate;
        IEnumerable<YieldCurveQuote> target = [
            new YieldCurveQuote(new DateTime(2025, 07, 21), governemtBondNominalRating, yieldCurveQuoteType, new Maturity(10), 0.031679857663),
            new YieldCurveQuote(new DateTime(2025, 07, 22), governemtBondNominalRating, yieldCurveQuoteType, new Maturity(10), 0.031511832005),
            new YieldCurveQuote(new DateTime(2025, 07, 23), governemtBondNominalRating, yieldCurveQuoteType, new Maturity(10), 0.031509712562000004),
            new YieldCurveQuote(new DateTime(2025, 07, 24), governemtBondNominalRating, yieldCurveQuoteType, new Maturity(10), 0.03237902676),
        ];
        YieldCurveQuotesLoader sut = new();

        IEnumerable<YieldCurveQuote> result = await sut.GetYieldCurveQuotesAsync(GovernemtBondNominalRating.AllRatings, yieldCurveQuoteType, new Maturity(10), new DateTime(2025, 07, 20), new DateTime(2025, 07, 25));

        Assert.Equal(target, result);
    }
}
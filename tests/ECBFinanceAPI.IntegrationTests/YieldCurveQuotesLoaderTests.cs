using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Loaders;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveQuotesLoaderTests
{
    [Fact]
    public async Task GetYieldCurveQuotesAsync_WithinDays_ReturnsCorrectQuotes()
    {
        // Arrange
        GovernmentBondNominalRating governmentBondNominalRating = GovernmentBondNominalRating.AllRatings;
        QuoteType yieldCurveQuoteType = QuoteType.SpotRate;
        Maturity maturity = new(10);
        DateTime startDate = new(2025, 07, 20);
        DateTime endDate = new(2025, 07, 25);

        IEnumerable<YieldCurveQuote> target = [
            new YieldCurveQuote(new DateTime(2025, 07, 21), maturity, 0.031679857663),
            new YieldCurveQuote(new DateTime(2025, 07, 22), maturity, 0.031511832005),
            new YieldCurveQuote(new DateTime(2025, 07, 23), maturity, 0.031509712562000004),
            new YieldCurveQuote(new DateTime(2025, 07, 24), maturity, 0.03237902676),
            new YieldCurveQuote(new DateTime(2025, 07, 25), maturity, 0.032731336557),
        ];

        YieldCurveQuotesLoader sut = new();

        // Act
        IEnumerable<YieldCurveQuote> result = await sut.GetYieldCurveQuotesAsync(governmentBondNominalRating, yieldCurveQuoteType, maturity, startDate, endDate);

        // Assert
        Assert.Equal(target, result);
    }
}
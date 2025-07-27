using ECBFinanceAPI.YieldCurves;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveQuotesLoaderTests
{
    [Fact]
    public async Task GetYieldCurveQuotesAsync_WithinDays_ReturnsCorrectQuotes()
    {
        GovernmentBondNominalRating governmentBondNominalRating = GovernmentBondNominalRating.AllRatings;
        QuoteType yieldCurveQuoteType = QuoteType.SpotRate;
        IEnumerable<TimeSeriesPoint<double>> target = [
            new TimeSeriesPoint<double>(new DateTime(2025, 7, 21), 0.031679857663),
            new TimeSeriesPoint<double>(new DateTime(2025, 7, 22), 0.031511832005),
            new TimeSeriesPoint<double>(new DateTime(2025, 7, 23), 0.031509712562000004),
            new TimeSeriesPoint<double>(new DateTime(2025, 7, 24), 0.03237902676),
        ];
        YieldCurveQuotesLoader sut = new();

        YieldCurveQuoteTimeSeries result = await sut.GetYieldCurveQuotesAsync(governmentBondNominalRating, yieldCurveQuoteType, new Maturity(10), new DateTime(2025, 07, 20), new DateTime(2025, 07, 25));

        Assert.Equal(target, result.TimeSeriesPoints);
    }
}
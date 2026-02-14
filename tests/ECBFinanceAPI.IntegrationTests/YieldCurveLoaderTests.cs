using CsvHelper;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Loaders;
using ECBFinanceAPI.YieldCurves.Models;
using System.Globalization;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveLoaderTests
{
    private static readonly HttpClient _httpClient = new();

    [Theory]
    [InlineData(GovernmentBondNominalRating.AAA, QuoteType.ParRate, MaturityFrequency.Monthly)]
    [InlineData(GovernmentBondNominalRating.AAA, QuoteType.ParRate, MaturityFrequency.Yearly)]
    [InlineData(GovernmentBondNominalRating.AAA, QuoteType.SpotRate, MaturityFrequency.Monthly)]
    [InlineData(GovernmentBondNominalRating.AAA, QuoteType.SpotRate, MaturityFrequency.Yearly)]
    [InlineData(GovernmentBondNominalRating.AAA, QuoteType.InstantaneousForwardRate, MaturityFrequency.Monthly)]
    [InlineData(GovernmentBondNominalRating.AAA, QuoteType.InstantaneousForwardRate, MaturityFrequency.Yearly)]
    [InlineData(GovernmentBondNominalRating.AllRatings, QuoteType.ParRate, MaturityFrequency.Monthly)]
    [InlineData(GovernmentBondNominalRating.AllRatings, QuoteType.ParRate, MaturityFrequency.Yearly)]
    [InlineData(GovernmentBondNominalRating.AllRatings, QuoteType.SpotRate, MaturityFrequency.Monthly)]
    [InlineData(GovernmentBondNominalRating.AllRatings, QuoteType.SpotRate, MaturityFrequency.Yearly)]
    [InlineData(GovernmentBondNominalRating.AllRatings, QuoteType.InstantaneousForwardRate, MaturityFrequency.Monthly)]
    [InlineData(GovernmentBondNominalRating.AllRatings, QuoteType.InstantaneousForwardRate, MaturityFrequency.Yearly)]
    [InlineData(GovernmentBondNominalRating.AAAtoAA, QuoteType.SpotRate, MaturityFrequency.Monthly)]

    public async Task GetYieldCurveAsync_ReturnsCorrectCurve(GovernmentBondNominalRating rating, QuoteType quoteType, MaturityFrequency maturityFrequency)
    {
        IEnumerable<YieldCurveQuote> targetQuotes = LoadTargetYieldCurveQuotes(rating, quoteType, maturityFrequency);
        YieldCurveLoader sut = new(_httpClient);

        YieldCurve result = await sut.GetYieldCurveAsync(
            rating,
            quoteType,
            new DateTime(2025, 7, 24),
            maturityFrequency
        );

        Assert.Equal(targetQuotes, result.Quotes);
    }

    private static IEnumerable<YieldCurveQuote> LoadTargetYieldCurveQuotes(GovernmentBondNominalRating rating, QuoteType quoteType, MaturityFrequency maturityFrequency)
    {
        using StreamReader streamReader = new($"targets/yield_curve_quotes_{rating}_{quoteType}_{maturityFrequency}.csv");
        using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);

        return [.. csvReader.GetRecords<YieldCurveQuote>()];
    }
}
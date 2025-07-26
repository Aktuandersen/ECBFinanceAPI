using ECBFinanceAPI.YieldCurves;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.UnitTests.YieldCurvesTests;

public class YieldCurveObservablesEndpointTests
{
    [Theory]
    [InlineData(GovernemtBondNominalRating.AAA, YieldCurveQuoteType.SpotRate, 10, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_A.SV_C_YM.SR_10Y?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    [InlineData(GovernemtBondNominalRating.AAAtoAA, YieldCurveQuoteType.ParRate, 5, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_W.SV_C_YM.PR_5Y?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    [InlineData(GovernemtBondNominalRating.AllRatings, YieldCurveQuoteType.InstantaneousForwardRate, 15, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_C.SV_C_YM.IF_15Y?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    public void YieldCurveQuotesEndpoint_Constructor_CreatesCorrectEndpoint(GovernemtBondNominalRating rating, YieldCurveQuoteType quoteType, int maturityYears, string targetUrl)
    {
        YieldCurveObservablesEndpoint sut = new(rating, quoteType, new Maturity(maturityYears), new DateTime(2025, 07, 20), new DateTime(2025, 07, 25));

        Assert.Equal(targetUrl, sut.ToString());
    }

    [Theory]
    [InlineData(GovernemtBondNominalRating.AAA, NelsonSiegelSvenssonParameter.Beta0, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_A.SV_C_YM.BETA0?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    [InlineData(GovernemtBondNominalRating.AAA, NelsonSiegelSvenssonParameter.Beta1, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_A.SV_C_YM.BETA1?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    [InlineData(GovernemtBondNominalRating.AllRatings, NelsonSiegelSvenssonParameter.Beta2, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_C.SV_C_YM.BETA2?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    [InlineData(GovernemtBondNominalRating.AllRatings, NelsonSiegelSvenssonParameter.Beta3, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_C.SV_C_YM.BETA3?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    [InlineData(GovernemtBondNominalRating.AAAtoAA, NelsonSiegelSvenssonParameter.Tau1, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_W.SV_C_YM.TAU1?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    [InlineData(GovernemtBondNominalRating.AAAtoAA, NelsonSiegelSvenssonParameter.Tau2, "https://data-api.ecb.europa.eu/service/data/YC/B.U2.EUR.4F.G_N_W.SV_C_YM.TAU2?startPeriod=2025-07-20&endPeriod=2025-07-25&format=csvdata")]
    public void YieldCurveParametersEndpoint_Constructor_CreatesCorrectEndpoint(GovernemtBondNominalRating rating, NelsonSiegelSvenssonParameter parameter, string targetUrl)
    {
        YieldCurveObservablesEndpoint sut = new(rating, parameter, new DateTime(2025, 07, 20), new DateTime(2025, 07, 25));

        Assert.Equal(targetUrl, sut.ToString());
    }
}

using ECBFinanceAPI.YieldCurves;
using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.IntegrationTests;

public class YieldCurveLoaderTests
{
    [Fact]
    public async Task GetYieldCurveAsync_ReturnsCorrectCurve()
    {
        IEnumerable<YieldCurveQuote> target = [
            new YieldCurveQuote(new Maturity(1), 0.018404697904),
            new YieldCurveQuote(new Maturity(2), 0.018858153399),
            new YieldCurveQuote(new Maturity(3), 0.019751779601),
            new YieldCurveQuote(new Maturity(4), 0.020876124204000004),
            new YieldCurveQuote(new Maturity(5), 0.022095242153000003),
            new YieldCurveQuote(new Maturity(6), 0.023322633895),
            new YieldCurveQuote(new Maturity(7), 0.024504903711),
            new YieldCurveQuote(new Maturity(8), 0.025610678225000003),
            new YieldCurveQuote(new Maturity(9), 0.026623106663),
            new YieldCurveQuote(new Maturity(10), 0.027534798034),
            new YieldCurveQuote(new Maturity(11), 0.028344414707),
            new YieldCurveQuote(new Maturity(12), 0.029054390483),
            new YieldCurveQuote(new Maturity(13), 0.029669410905000002),
            new YieldCurveQuote(new Maturity(14), 0.030195409277000002),
            new YieldCurveQuote(new Maturity(15), 0.03063891071),
            new YieldCurveQuote(new Maturity(16), 0.031006610335000004),
            new YieldCurveQuote(new Maturity(17), 0.031305108379),
            new YieldCurveQuote(new Maturity(18), 0.03154074977),
            new YieldCurveQuote(new Maturity(19), 0.031719532878),
            new YieldCurveQuote(new Maturity(20), 0.031847063499),
            new YieldCurveQuote(new Maturity(21), 0.031928538051),
            new YieldCurveQuote(new Maturity(22), 0.031968745217),
            new YieldCurveQuote(new Maturity(23), 0.031972078868),
            new YieldCurveQuote(new Maturity(24), 0.031942557548),
            new YieldCurveQuote(new Maturity(25), 0.031883847392),
            new YieldCurveQuote(new Maturity(26), 0.031799286491),
            new YieldCurveQuote(new Maturity(27), 0.031691909418),
            new YieldCurveQuote(new Maturity(28), 0.031564471141),
            new YieldCurveQuote(new Maturity(29), 0.031419469853),
            new YieldCurveQuote(new Maturity(30), 0.031259168481)
        ];
        YieldCurveLoader sut = new();

        YieldCurve result = await sut.GetYieldCurveAsync(
            GovernmentBondNominalRating.AAA,
            QuoteType.SpotRate,
            new DateTime(2025, 7, 24),
            MaturityFrequency.Yearly
        );

        Assert.Equal(target, result.Quotes);
    }
}
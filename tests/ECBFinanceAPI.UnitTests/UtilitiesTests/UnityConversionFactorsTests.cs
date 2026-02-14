using Utilities;

namespace ECBFinanceAPI.UnitTests.UtilitiesTests;

public class UnityConversionFactorsTests
{
    [Fact]
    public void PercentToDecimal_ConvertsToDecimal()
    {
        double aPercentage = 5.0;
        double expectedDecimal = 0.05;

        double result = aPercentage * UnityConversionFactors.PercentToDecimal;

        Assert.Equal(expectedDecimal, result);
    }
}
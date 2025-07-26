using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.UnitTests.YieldCurvesTests;
public class MaturityTests
{
    [Theory]
    [InlineData(5, 0)]
    [InlineData(0, 11)]
    [InlineData(2, 6)]
    public void Constructor_ValidArguments_SetsProperties(int years, int months)
    {
        Maturity maturity = new(years, months);

        Assert.Equal(years, maturity.Years);
        Assert.Equal(months, maturity.Months);
    }

    [Fact]
    public void Constructor_YearsOnly_SetsMonthsToZero()
    {
        Maturity maturity = new(1);

        Assert.Equal(0, maturity.Months);
    }

    [Theory]
    [InlineData(1, 0, "1Y")]
    [InlineData(0, 6, "6M")]
    [InlineData(2, 3, "2Y3M")]
    public void ToString_ReturnsExpectedFormat(int years, int months, string expected)
    {
        Maturity maturity = new(years, months);

        Assert.Equal(expected, maturity.ToString());
    }

    [Theory]
    [InlineData(-1, 0)] // Negative year
    [InlineData(0, -1)] // Negative month
    [InlineData(-5, -2)] // Both negative
    public void Constructor_NegativeArguments_ThrowsArgumentOutOfRangeException(int years, int months)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Maturity(years, months));
    }

    [Fact]
    public void Constructor_MonthsGreaterThan11_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Maturity(0, 12));
    }

    [Fact]
    public void Constructor_BothZero_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Maturity(0, 0));
    }

    [Fact]
    public void Constructor_YearsOnlyZero_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Maturity(0));
    }
}

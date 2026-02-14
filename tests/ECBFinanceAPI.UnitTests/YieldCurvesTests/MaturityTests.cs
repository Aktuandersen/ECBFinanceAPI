using ECBFinanceAPI.Loaders.YieldCurves.Models;

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
    [InlineData(1, 0, 2, 0)]
    [InlineData(1, 6, 1, 7)]
    [InlineData(0, 5, 0, 6)]
    public void Comparison_SmallerThan_ComparesCorrectly(int years1, int months1, int years2, int months2)
    {
        Maturity maturity1 = new(years1, months1);
        Maturity maturity2 = new(years2, months2);

        int result = maturity1.CompareTo(maturity2);

        Assert.True(result < 0);
    }

    [Theory]
    [InlineData(2, 0, 1, 0)]
    [InlineData(2, 6, 2, 5)]
    [InlineData(0, 6, 0, 5)]
    public void Comparison_GreaterThan_ComparesCorrectly(int years1, int months1, int years2, int months2)
    {
        Maturity maturity1 = new(years1, months1);
        Maturity maturity2 = new(years2, months2);

        int result = maturity1.CompareTo(maturity2);

        Assert.True(result > 0);
    }

    [Theory]
    [InlineData(2, 0, 2, 0)]
    [InlineData(2, 5, 2, 5)]
    [InlineData(0, 5, 0, 5)]
    public void Comparison_EqualTo_ComparesCorrectly(int years1, int months1, int years2, int months2)
    {
        Maturity maturity1 = new(years1, months1);
        Maturity maturity2 = new(years2, months2);

        int result = maturity1.CompareTo(maturity2);

        Assert.Equal(0, result);
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

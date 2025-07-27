using ECBFinanceAPI.YieldCurves.Enums;

namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents a <see cref="YieldCurveTimeSeries{T}"/> of quotes.
/// </summary>
public class YieldCurveQuoteTimeSeries : YieldCurveTimeSeries<double>
{
    /// <summary>
    /// Gets the maturity for which the yield quotes are provided.
    /// </summary>
    public Maturity Maturity { get; }

    /// <summary>
    /// Gets the type of quote.
    /// </summary>
    public QuoteType QuoteType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurveQuoteTimeSeries"/> class using the provided quote time points, maturity, bond rating, and quote type.
    /// </summary>
    /// <param name="timeSeriesPoints">The sequence of time series points containing yield values.</param>
    /// <param name="maturity">The maturity associated with the yield curve quotes.</param>
    /// <param name="governmentBondNominalRating">The nominal rating of the government bond.</param>
    /// <param name="quoteType">The type of quote.</param>
    public YieldCurveQuoteTimeSeries(IEnumerable<TimeSeriesPoint<double>> timeSeriesPoints, GovernmentBondNominalRating governmentBondNominalRating, Maturity maturity, QuoteType quoteType) : base(timeSeriesPoints, governmentBondNominalRating)
    {
        Maturity = maturity;
        QuoteType = quoteType;
    }
}

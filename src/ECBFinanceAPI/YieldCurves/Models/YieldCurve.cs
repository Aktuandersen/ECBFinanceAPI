using ECBFinanceAPI.YieldCurves.Enums;
using System.Collections;
using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents a yield curve composed of an <see cref="IEnumerable{T}"/> of type <see cref="YieldCurveQuote"/>,
/// along with associated metadata such as the fitted <see cref="Models.NelsonSiegelSvenssonParameters"/>,
/// the <see cref="Enums.GovernmentBondNominalRating"/>, the <see cref="Enums.QuoteType"/>, and date of the curve.
/// </summary>
/// <remarks>
/// The yield curve maps maturities to yields and provides access to the underlying quotes.
/// It supports enumeration over itself and provides an indexer for lookup of yields by maturity.
/// </remarks>
public class YieldCurve : IEnumerable<YieldCurveQuote>
{
    private readonly ImmutableSortedDictionary<Maturity, double> _maturityToYield;

    /// <summary>
    /// Gets the collection of quotes that make up the yield curve.
    /// </summary>
    public IEnumerable<YieldCurveQuote> Quotes { get; }

    /// <summary>
    /// Gets the parameters of the Nelson-Siegel-Svensson model fitted to this yield curve.
    /// </summary>
    public NelsonSiegelSvenssonParameters? NelsonSiegelSvenssonParameters { get; }

    /// <summary>
    /// Gets the government bond nominal rating associated with this yield curve.
    /// </summary>
    public GovernmentBondNominalRating GovernmentBondNominalRating { get; }

    /// <summary>
    /// Gets the type of quote used in this yield curve.
    /// </summary>
    public QuoteType QuoteType { get; }

    /// <summary>
    /// Gets the date for the yield curve.
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Gets the maturities available in the yield curve.
    /// </summary>
    public IEnumerable<Maturity> Maturities => _maturityToYield.Keys;

    /// <summary>
    /// Gets the yields corresponding to the maturities in the yield curve.
    /// </summary>
    public IEnumerable<double> Yields => _maturityToYield.Values;

    /// <summary>
    /// Gets the yield for the specified <paramref name="maturity"/>.
    /// </summary>
    /// <param name="maturity">The maturity for which to retrieve the yield.</param>
    /// <returns>The yield corresponding to the specified maturity.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the maturity is not found in the yield curve.</exception>
    public double this[Maturity maturity] => _maturityToYield[maturity];

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurve"/> class with the specified quotes,
    /// Nelson-Siegel-Svensson parameters, government bond rating, quote type, and date.
    /// </summary>
    /// <param name="quotes">The collection of yield curve quotes.</param>
    /// <param name="nelsonSiegelSvenssonParameters">The NSS parameters fitted to the curve.</param>
    /// <param name="governmentBondNominalRating">The government bond nominal rating for the curve.</param>
    /// <param name="quoteType">The type of quote represented in the curve.</param>
    /// <param name="date">The date the curve is valid for.</param>
    public YieldCurve(
        IEnumerable<YieldCurveQuote> quotes,
        NelsonSiegelSvenssonParameters? nelsonSiegelSvenssonParameters,
        GovernmentBondNominalRating governmentBondNominalRating,
        QuoteType quoteType,
        DateTime date)
    {
        _maturityToYield = quotes.ToImmutableSortedDictionary(y => y.Maturity, y => y.Yield);

        Quotes = quotes;
        NelsonSiegelSvenssonParameters = nelsonSiegelSvenssonParameters;
        GovernmentBondNominalRating = governmentBondNominalRating;
        QuoteType = quoteType;
        Date = date;
    }

    public IEnumerator<YieldCurveQuote> GetEnumerator() => Quotes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

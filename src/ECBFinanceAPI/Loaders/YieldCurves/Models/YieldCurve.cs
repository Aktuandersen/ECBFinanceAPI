using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using System.Collections;
using System.Collections.Immutable;

namespace ECBFinanceAPI.Loaders.YieldCurves.Models;

/// <summary>
/// Represents a yield curve composed of a collection of <see cref="YieldCurveQuote"/> items.
/// The curve provides access to the underlying quotes, an indexer for retrieving yields by <see cref="Maturity"/>,
/// and optional fitted <see cref="Models.NelsonSiegelSvenssonParameters"/> describing the curve shape.
/// </summary>
public class YieldCurve : IEnumerable<YieldCurveQuote>
{
    private readonly ImmutableSortedDictionary<Maturity, double> _maturityToYield;

    /// <summary>
    /// The collection of quotes that make up this yield curve.
    /// </summary>
    public IEnumerable<YieldCurveQuote> Quotes { get; }

    /// <summary>
    /// The parameters of the Nelson–Siegel–Svensson model fitted to this curve, if available.
    /// </summary>
    public NelsonSiegelSvenssonParameters? NelsonSiegelSvenssonParameters { get; }

    /// <summary>
    /// The government bond nominal rating associated with this yield curve.
    /// </summary>
    public GovernmentBondNominalRating GovernmentBondNominalRating { get; }

    /// <summary>
    /// The type of quote (e.g. spot rate, par rate) represented by the curve.
    /// </summary>
    public QuoteType QuoteType { get; }

    /// <summary>
    /// The date the yield curve is valid for.
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// The maturities available on the yield curve.
    /// </summary>
    public IEnumerable<Maturity> Maturities => _maturityToYield.Keys;

    /// <summary>
    /// The yields corresponding to the maturities in the curve, in the same order as <see cref="Maturities"/>.
    /// </summary>
    public IEnumerable<double> Yields => _maturityToYield.Values;

    /// <summary>
    /// Gets the yield for the specified <paramref name="maturity"/>.
    /// </summary>
    /// <param name="maturity">The maturity for which to retrieve the yield.</param>
    /// <returns>The yield corresponding to the specified maturity.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the maturity is not present in the curve.</exception>
    public double this[Maturity maturity] => _maturityToYield[maturity];

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurve"/> class.
    /// </summary>
    /// <param name="quotes">The collection of yield curve quotes.</param>
    /// <param name="nelsonSiegelSvenssonParameters">The NSS parameters fitted to the curve, if any.</param>
    /// <param name="governmentBondNominalRating">The government bond nominal rating for the curve.</param>
    /// <param name="quoteType">The type of quote represented in the curve.</param>
    /// <param name="date">The date the curve is valid for.</param>
    public YieldCurve(IEnumerable<YieldCurveQuote> quotes, NelsonSiegelSvenssonParameters? nelsonSiegelSvenssonParameters, GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, DateTime date)
    {
        _maturityToYield = quotes.ToImmutableSortedDictionary(y => y.Maturity, y => y.Yield);

        Quotes = quotes;
        NelsonSiegelSvenssonParameters = nelsonSiegelSvenssonParameters;
        GovernmentBondNominalRating = governmentBondNominalRating;
        QuoteType = quoteType;
        Date = date;
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public IEnumerator<YieldCurveQuote> GetEnumerator() => Quotes.GetEnumerator();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

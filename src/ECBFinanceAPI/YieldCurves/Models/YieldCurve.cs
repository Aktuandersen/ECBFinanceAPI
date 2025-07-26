using ECBFinanceAPI.YieldCurves.Enums;
using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Models;

public class YieldCurve
{
    private readonly ImmutableSortedDictionary<Maturity, double> _maturityToYield;
    public GovernemtBondNominalRating GovernemtBondNominalRating => YieldCurveQuotes.DistinctBy(y => y.GovernemtBondNominalRating).Single().GovernemtBondNominalRating;
    public YieldCurveQuoteType YieldCurveQuoteType => YieldCurveQuotes.DistinctBy(y => y.YieldCurveQuoteType).Single().YieldCurveQuoteType;
    public IEnumerable<YieldCurveQuote> YieldCurveQuotes { get; }
    public IEnumerable<Maturity> Maturities => _maturityToYield.Select(x => x.Key);
    public NelsonSiegelSvenssonParameters NelsonSiegelSvenssonParameters { get; }
    public DateTime Date { get; }
    public double this[Maturity maturity] => _maturityToYield[maturity];

    public YieldCurve(IEnumerable<YieldCurveQuote> yieldCurveQuotes, NelsonSiegelSvenssonParameters nelsonSiegelSvenssonParameters, DateTime date)
    {
        _maturityToYield = yieldCurveQuotes.ToImmutableSortedDictionary(y => y.Maturity, y => y.Yield);
        YieldCurveQuotes = yieldCurveQuotes;
        NelsonSiegelSvenssonParameters = nelsonSiegelSvenssonParameters;
        Date = date;
    }
}

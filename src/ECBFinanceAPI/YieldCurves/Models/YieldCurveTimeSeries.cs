using ECBFinanceAPI.YieldCurves.Enums;
using System.Collections.Immutable;

namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents a time series of observable values of type <typeparamref name="TObservable"/>, indexed by date.
/// </summary>
/// <typeparam name="TObservable">The type of the observable values in the time series.</typeparam>
public class YieldCurveTimeSeries<TObservable>
{
    private readonly ImmutableSortedDictionary<DateTime, TObservable> _dateTimeToObservable;

    /// <summary>
    /// Gets the collection of time series points, each containing a date and an associated observable value.
    /// </summary>
    public IEnumerable<TimeSeriesPoint<TObservable>> TimeSeriesPoints { get; }

    /// <summary>
    /// Gets the government bond nominal rating associated with this time series.
    /// </summary>
    public GovernmentBondNominalRating GovernmentBondNominalRating { get; }

    /// <summary>
    /// Gets the dates for which observations are available in the time series.
    /// </summary>
    public IEnumerable<DateTime> Dates => _dateTimeToObservable.Keys;

    /// <summary>
    /// Gets the observable values corresponding to the available dates in the time series.
    /// </summary>
    public IEnumerable<TObservable> Observables => _dateTimeToObservable.Values;

    /// <summary>
    /// Gets the observable value for the specified <paramref name="date"/>.
    /// </summary>
    /// <param name="date">The date of the observable value to retrieve.</param>
    /// <returns>The observable value associated with the specified date.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified date is not present in the time series.</exception>
    public TObservable this[DateTime date] => _dateTimeToObservable[date];

    /// <summary>
    /// Initializes a new instance of the <see cref="YieldCurveTimeSeries{TObservable}"/> class
    /// using the provided collection of time series points and associated government bond nominal rating.
    /// </summary>
    /// <param name="timeSeriesPoints">A collection of time series points, each containing a date and an observable value.</param>
    /// <param name="governmentBondNominalRating">The government bond nominal rating associated with this time series.</param>
    public YieldCurveTimeSeries(
        IEnumerable<TimeSeriesPoint<TObservable>> timeSeriesPoints,
        GovernmentBondNominalRating governmentBondNominalRating)
    {
        _dateTimeToObservable = timeSeriesPoints.ToImmutableSortedDictionary(p => p.Date, p => p.Observable);

        TimeSeriesPoints = timeSeriesPoints;
        GovernmentBondNominalRating = governmentBondNominalRating;
    }
}


namespace ECBFinanceAPI.YieldCurves.Models;

/// <summary>
/// Represents a single data point in a time series,
/// consisting of a timestamp and an associated observable value of type <typeparamref name="TObservable"/>.
/// </summary>
/// <typeparam name="TObservable">
/// The type of the observable value at the given date.
/// </typeparam>
/// <param name="Date">The date and time of the observation.</param>
/// <param name="Observable">The observed value at the specified date.</param>
public record TimeSeriesPoint<TObservable>(DateTime Date, TObservable Observable);

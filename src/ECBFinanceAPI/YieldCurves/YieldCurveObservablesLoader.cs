using CsvHelper;
using ECBFinanceAPI.YieldCurves.Models;
using System.Globalization;

namespace ECBFinanceAPI.YieldCurves;

/// <summary>
/// Provides functionality to load yield curve observables from a specified <see cref="YieldCurveObservablesEndpoint"/>.
/// </summary>
public class YieldCurveObservablesLoader
{
    private protected readonly HttpClient _httpClient = new();

    private protected async Task<IEnumerable<YieldCurveObservable>> DownloadYieldCurveObservablesAsync(YieldCurveObservablesEndpoint endPoint)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to get yield curve observable from end point {endPoint}.");

        string content = await response.Content.ReadAsStringAsync();

        using StringReader stringReader = new(content);
        using CsvReader csvReader = new(stringReader, CultureInfo.InvariantCulture);

        return [.. csvReader.GetRecords<YieldCurveObservable>()];
    }
}

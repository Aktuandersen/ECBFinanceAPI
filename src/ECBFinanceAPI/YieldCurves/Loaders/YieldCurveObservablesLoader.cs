using CsvHelper;
using ECBFinanceAPI.YieldCurves.Models;
using System.Globalization;
using System.Net;

namespace ECBFinanceAPI.YieldCurves.Loaders;

public abstract class YieldCurveObservablesLoader
{
    private static readonly int _thirtySeconds = 30_000;

    private protected readonly HttpClient _httpClient;

    public YieldCurveObservablesLoader() : this(new HttpClient()) { }

    public YieldCurveObservablesLoader(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private protected async Task<IEnumerable<YieldCurveObservable>> DownloadYieldCurveObservablesAsync(YieldCurveObservablesEndpoint endPoint)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
        if (response.StatusCode is HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(_thirtySeconds);
            return await DownloadYieldCurveObservablesAsync(endPoint);
        }
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to get yield curve observable from end point {endPoint}.\nHttpStatusCode: {response.StatusCode}");

        string content = await response.Content.ReadAsStringAsync();

        using StringReader stringReader = new(content);
        using CsvReader csvReader = new(stringReader, CultureInfo.InvariantCulture);

        return [.. csvReader.GetRecords<YieldCurveObservable>()];
    }
}

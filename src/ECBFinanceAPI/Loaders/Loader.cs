using CsvHelper;
using ECBFinanceAPI.Endpoints;
using System.Globalization;
using System.Net;

namespace ECBFinanceAPI.Loaders;

public abstract class Loader
{
    private static readonly int _thirtySeconds = 30_000;

    private protected readonly HttpClient _httpClient;

    public Loader() : this(new HttpClient()) { }

    public Loader(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private protected async Task<IEnumerable<ECBData>> DownloadAsync(Endpoint endPoint)
    {
        Uri uri = endPoint.Uri;
        HttpResponseMessage response = await _httpClient.GetAsync(uri);

        if (response.StatusCode is HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(_thirtySeconds);
            return await DownloadAsync(endPoint);
        }

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to get ECB data from endpoint {uri}.\nHttpStatusCode: {response.StatusCode}.");

        Stream stream = await response.Content.ReadAsStreamAsync();

        using StreamReader reader = new(stream);
        using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);

        return [.. csvReader.GetRecords<ECBData>()];
    }
}

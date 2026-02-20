namespace ECBFinanceAPI.IntegrationTests;

[CollectionDefinition("IntegrationTests")]
public class IntegrationTestCollection : ICollectionFixture<HttpClientFixture> { }

public sealed class HttpClientFixture : IDisposable
{
    public HttpClient Client { get; }

    public HttpClientFixture()
    {
        Client = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5)
        };
    }

    public void Dispose()
    {
        Client.Dispose();
    }
}
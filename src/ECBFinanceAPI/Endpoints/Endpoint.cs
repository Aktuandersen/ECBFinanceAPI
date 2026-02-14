namespace ECBFinanceAPI.Endpoints;

internal abstract class Endpoint
{
    private static readonly string _baseUrl = "https://data-api.ecb.europa.eu/service/data";

    private readonly DateTime? _startDate;
    private readonly DateTime? _endDate;

    private protected abstract string DatasetCode { get; }
    private protected abstract string FrequencyCode { get; }
    private protected abstract string ReferenceAreaCode { get; }
    private protected abstract string CurrencyCode { get; }
    private protected abstract string FinancialMarketProviderCode { get; }
    private protected abstract string FinancialMarketInstrumentCode { get; }
    private protected abstract string FinancialMarketProviderIdentifierCode { get; }
    private protected abstract string FinancialMarketDataTypeCode { get; }

    public Uri Uri => new(GetUriString());

    public Endpoint(DateTime? startDate, DateTime? endDate)
    {
        (_startDate, _endDate) = (startDate, endDate);
    }

    private string GetUriString()
    {
        DateTime startDate = _startDate is null ? DateTime.MinValue : _startDate.Value;
        DateTime endDate = _endDate is null ? DateTime.MaxValue : _endDate.Value;

        return $"{_baseUrl}/{DatasetCode}/{FrequencyCode}.{ReferenceAreaCode}.{CurrencyCode}.{FinancialMarketProviderCode}.{FinancialMarketInstrumentCode}.{FinancialMarketProviderIdentifierCode}.{FinancialMarketDataTypeCode}?detail=dataonly&startPeriod={startDate:yyyy-MM-dd}&endPeriod={endDate:yyyy-MM-dd}&format=csvdata";
    }
}

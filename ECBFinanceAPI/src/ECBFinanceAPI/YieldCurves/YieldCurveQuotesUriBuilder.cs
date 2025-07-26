using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

internal class YieldCurveQuotesUriBuilder
{
    private static readonly string _baseUrl = "https://data-api.ecb.europa.eu/service/data";
    private static readonly string _dataset = "YC";
    private static readonly string _frequencyCode = "B";
    private static readonly string _referenceAreaCode = "U2";
    private static readonly string _currency = "EUR";
    private static readonly string _financialMarketProviderCode = "4F";
    private static readonly string _financialMarketProviderIdentifierCode = "SV_C_YM";

    private DateTime startDate = DateTime.MinValue;
    private DateTime endDate = DateTime.MaxValue;
    private string? financialMarketInstrumentCode;
    private string? financialMarketDataType;

    public YieldCurveQuotesUriBuilder WithStartDate(DateTime startDate)
    {
        this.startDate = startDate;
        return this;
    }

    public YieldCurveQuotesUriBuilder WithEndDate(DateTime endDate)
    {
        this.endDate = endDate;
        return this;
    }

    public YieldCurveQuotesUriBuilder WithGovernmentBondNominalRating(GovernemtBondNominalRating governemtBondNominalRating)
    {
        financialMarketInstrumentCode = GovernemtBondNominalRatingMappings.GovernemtBondNominalRatingToCode[governemtBondNominalRating];
        return this;
    }

    public YieldCurveQuotesUriBuilder WithYieldCurveQuote(YieldCurveQuoteType yieldCurveRateType, Maturity maturity)
    {
        financialMarketDataType = $"{YieldCurveQuoteTypeMappings.YieldCurveQuoteTypeToCode[yieldCurveRateType]}_{maturity}";
        return this;
    }

    public YieldCurveQuotesUriBuilder WithNelsonSiegelSvenssonParameter(NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter)
    {
        financialMarketDataType = NelsonSiegelSvenssonParameterMappings.NelsonSiegelSvenssonParameterToCode[nelsonSiegelSvenssonParameter];
        return this;
    }

    public Uri Build() => new($"{_baseUrl}/{_dataset}/{_frequencyCode}.{_referenceAreaCode}.{_currency}.{_financialMarketProviderCode}.{financialMarketInstrumentCode}.{_financialMarketProviderIdentifierCode}.{financialMarketDataType}?startPeriod={startDate:yyyy-MM-dd}&endPeriod={endDate:yyyy-MM-dd}&format=csvdata");
}

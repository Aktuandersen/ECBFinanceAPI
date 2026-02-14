using ECBFinanceAPI.YieldCurves.Enums;

namespace ECBFinanceAPI.YieldCurves.Models;

internal class YieldCurveObservablesEndpoint : Uri
{
    private static readonly string _baseUrl = "https://data-api.ecb.europa.eu/service/data";
    private static readonly string _dataset = "YC";
    private static readonly string _frequencyCode = "B";
    private static readonly string _referenceAreaCode = "U2";
    private static readonly string _currency = "EUR";
    private static readonly string _financialMarketProviderCode = "4F";
    private static readonly string _financialMarketProviderIdentifierCode = "SV_C_YM";

    public YieldCurveObservablesEndpoint(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity) :
        this(governmentBondNominalRating, GetQuoteTypeAndMaturityFinancialMarketDataTypeString(quoteType, maturity))
    { }

    public YieldCurveObservablesEndpoint(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime startDate, DateTime endDate) :
        this(governmentBondNominalRating, GetQuoteTypeAndMaturityFinancialMarketDataTypeString(quoteType, maturity), startDate, endDate)
    { }

    public YieldCurveObservablesEndpoint(GovernmentBondNominalRating governmentBondNominalRating, NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter) :
        this(governmentBondNominalRating, nelsonSiegelSvenssonParameter.ToECBCode())
    { }

    public YieldCurveObservablesEndpoint(GovernmentBondNominalRating governmentBondNominalRating, NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter, DateTime startDate, DateTime endDate) :
        this(governmentBondNominalRating, nelsonSiegelSvenssonParameter.ToECBCode(), startDate, endDate)
    { }

    private YieldCurveObservablesEndpoint(GovernmentBondNominalRating governmentBondNominalRating, string financialMarketDataType, DateTime? startDate = null, DateTime? endDate = null) :
        base(GetUriString(governmentBondNominalRating, financialMarketDataType, startDate, endDate))
    { }

    private static string GetQuoteTypeAndMaturityFinancialMarketDataTypeString(QuoteType quoteType, Maturity maturity) => $"{quoteType.ToECBCode()}_{maturity}";

    private static string GetUriString(GovernmentBondNominalRating governmentBondNominalRating, string financialMarketDataType, DateTime? startDate, DateTime? endDate)
    {
        string financialMarketInstrumentCode = governmentBondNominalRating.ToECBCode();
        startDate ??= DateTime.MinValue;
        endDate ??= DateTime.MaxValue;

        return $"{_baseUrl}/{_dataset}/{_frequencyCode}.{_referenceAreaCode}.{_currency}.{_financialMarketProviderCode}.{financialMarketInstrumentCode}.{_financialMarketProviderIdentifierCode}.{financialMarketDataType}?detail=dataonly&startPeriod={startDate:yyyy-MM-dd}&endPeriod={endDate:yyyy-MM-dd}&format=csvdata";
    }
}

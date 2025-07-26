using ECBFinanceAPI.YieldCurves.Enums;
using ECBFinanceAPI.YieldCurves.Models;

namespace ECBFinanceAPI.YieldCurves;

internal class YieldCurveObservablesEndpoint : Uri
{
    private static readonly string _baseUrl = "https://data-api.ecb.europa.eu/service/data";
    private static readonly string _dataset = "YC";
    private static readonly string _frequencyCode = "B";
    private static readonly string _referenceAreaCode = "U2";
    private static readonly string _currency = "EUR";
    private static readonly string _financialMarketProviderCode = "4F";
    private static readonly string _financialMarketProviderIdentifierCode = "SV_C_YM";

    public YieldCurveObservablesEndpoint(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveRateType, Maturity maturity) :
        this(governemtBondNominalRating, $"{YieldCurveQuoteTypeMappings.YieldCurveQuoteTypeToCode[yieldCurveRateType]}_{maturity}")
    { }

    public YieldCurveObservablesEndpoint(GovernemtBondNominalRating governemtBondNominalRating, YieldCurveQuoteType yieldCurveRateType, Maturity maturity, DateTime startDate, DateTime endDate) :
        this(governemtBondNominalRating, $"{YieldCurveQuoteTypeMappings.YieldCurveQuoteTypeToCode[yieldCurveRateType]}_{maturity}", startDate, endDate)
    { }

    public YieldCurveObservablesEndpoint(GovernemtBondNominalRating governemtBondNominalRating, NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter) :
        this(governemtBondNominalRating, NelsonSiegelSvenssonParameterMappings.NelsonSiegelSvenssonParameterToCode[nelsonSiegelSvenssonParameter])
    { }

    public YieldCurveObservablesEndpoint(GovernemtBondNominalRating governemtBondNominalRating, NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter, DateTime startDate, DateTime endDate) :
        this(governemtBondNominalRating, NelsonSiegelSvenssonParameterMappings.NelsonSiegelSvenssonParameterToCode[nelsonSiegelSvenssonParameter], startDate, endDate)
    { }

    private YieldCurveObservablesEndpoint(GovernemtBondNominalRating governemtBondNominalRating, string financialMarketDataType, DateTime? startDate = null, DateTime? endDate = null) :
        base(GetUriString(governemtBondNominalRating, financialMarketDataType, startDate, endDate))
    { }


    private static string GetUriString(GovernemtBondNominalRating governemtBondNominalRating, string financialMarketDataType, DateTime? startDate = null, DateTime? endDate = null)
    {
        string financialMarketInstrumentCode = GovernemtBondNominalRatingMappings.GovernemtBondNominalRatingToCode[governemtBondNominalRating];
        startDate = startDate ?? DateTime.MinValue;
        endDate = endDate ?? DateTime.MaxValue;

        return $"{_baseUrl}/{_dataset}/{_frequencyCode}.{_referenceAreaCode}.{_currency}.{_financialMarketProviderCode}.{financialMarketInstrumentCode}.{_financialMarketProviderIdentifierCode}.{financialMarketDataType}?startPeriod={startDate:yyyy-MM-dd}&endPeriod={endDate:yyyy-MM-dd}&format=csvdata";
    }
}

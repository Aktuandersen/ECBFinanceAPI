using ECBFinanceAPI.Loaders.YieldCurves.Enums;
using ECBFinanceAPI.Loaders.YieldCurves.Models;

namespace ECBFinanceAPI.Endpoints;

internal class YieldCurveEndpoint : Endpoint
{
    private protected override string DatasetCode => "YC";
    private protected override string FrequencyCode => "B";
    private protected override string ReferenceAreaCode => "U2";
    private protected override string CurrencyCode => "EUR";
    private protected override string FinancialMarketProviderCode => "4F";
    private protected override string FinancialMarketInstrumentCode { get; }
    private protected override string FinancialMarketProviderIdentifierCode => "SV_C_YM";
    private protected override string FinancialMarketDataTypeCode { get; }

    public YieldCurveEndpoint(GovernmentBondNominalRating governmentBondNominalRating, QuoteType quoteType, Maturity maturity, DateTime? startDate, DateTime? endDate) : base(startDate, endDate)
    {
        FinancialMarketInstrumentCode = governmentBondNominalRating.ToECBCode();
        FinancialMarketDataTypeCode = $"{quoteType.ToECBCode()}_{maturity}";
    }

    public YieldCurveEndpoint(GovernmentBondNominalRating governmentBondNominalRating, NelsonSiegelSvenssonParameter nelsonSiegelSvenssonParameter, DateTime? startDate, DateTime? endDate) : base(startDate, endDate)
    {
        FinancialMarketInstrumentCode = governmentBondNominalRating.ToECBCode();
        FinancialMarketDataTypeCode = nelsonSiegelSvenssonParameter.ToECBCode();
    }
}

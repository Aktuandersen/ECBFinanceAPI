---
_disableToc: true
---

# Getting started

The library is based around _loaders_, that loads collections of _models_ given the _data specifications_.

Whenever you want to access some data, you instantiate a loader and call it with your data specifications, and that will return a collection of the model associated with that call.

Let's say we want to load all the available 10-year spot rates for AAA rated governmet bonds in the euro area.

```cs
var quoteLoader = new YieldCurveQuoteLoader();

var rating = GovernmentBondNominalRating.AAA;
var quoteType = QuoteType.SpotRate;
var maturity = new Maturity(Years: 10);

IEnumerable<YieldCurveQuote> tenYearAAASpotRates = await quoteLoader.GetYieldCurveQuotesAsync(rating, quoteType, maturity);
```

---

We can also specify the time inverval:

```cs
var startDate = new DateTime(2025, 01, 01);
var endDate = new DateTime(2025, 12, 31);

IEnumerable<YieldCurveQuote> tenYearAAASpotRatesOf2025 = await quoteLoader.GetYieldCurveQuotesAsync(rating, quoteType, maturity, startDate, endDate);
```

---

The `YieldCurveQuote` model is a simple data container, but some loaders will return more sophisticated models:

```cs
var yieldCurveLoader = new YieldCurveLoader();

var date = new DateTime(2025, 01, 31);
var maturityFrequency = MaturityFrequency.Yearly;

YieldCurve curve = await yieldCurveLoader.GetYieldCurveAsync(rating, quoteType, date, maturityFrequency);
```
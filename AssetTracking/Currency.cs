using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracking
{
    internal class Currency(string currencyKey, string currencyName, double conversionRateToUSD)
    {
        public string Key { get; set; } = currencyKey;
        public string Name { get; set; } = currencyName;
        public double ConversionRateToUSD { get; set; } = conversionRateToUSD;
    }
}

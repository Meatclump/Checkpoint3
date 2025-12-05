using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracking
{
    internal class Office(string id, string Name, string currencyKey)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = Name;
        public string CurrencyKey { get; set; } = currencyKey;
    }
}

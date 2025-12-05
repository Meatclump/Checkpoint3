using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracking
{
    internal class Asset(string brand, string model, double price, DateTime purchaseDate, string officeKey)
    {
        public string Brand { get; set; } = brand;
        public string Model { get; set; } = model;
        public double Price { get; set; } = price;
        public DateTime PurchaseDate { get; set; } = purchaseDate;
        public string OfficeKey { get; set; } = officeKey;
    }
}

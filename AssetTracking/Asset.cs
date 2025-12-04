using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracking
{
    internal class Asset(string brand, string model, string type, double price, DateTime purchaseDate)
    {
        public string Brand { get; set; } = brand;
        public string Model { get; set; } = model;
        public string Type { get; set; } = type;
        public double Price { get; set; } = price;
        public DateTime PurchaseDate { get; set; } = purchaseDate;
    }
}

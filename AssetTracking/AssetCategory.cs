using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracking
{
    internal class AssetCategory(string name, List<Asset> assets)
    {
        public string Name { get; set; } = name;
        public List<Asset> Assets { get; set; } = assets;
    }
}

using System;
using System.Collections.Generic;

namespace ItemLibrary
{
    public abstract class Item
    {
        //not needed at the moment
        //public ulong ItemCode { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string ItemURL { get; set; }
        public string ShopName { get; set; }
        public abstract List<Item> FindSimilar(List<Item> list);

    }
}

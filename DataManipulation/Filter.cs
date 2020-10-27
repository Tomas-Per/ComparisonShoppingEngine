using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManipulation
{
    public class Filter
    {
        protected List<Item> _items { get; set; }

        public Filter(List<Item> items)
        {
            _items = items;
        }

        //Filters item list by the manufacturer and sets the private field to the filtered list
        public List<Item> FilterByManufacturer(string manufacturer)
        {
            List<Item> result = _items.Where(item => item.ManufacturerName == manufacturer).ToList();
            _items = result;
            return result;
        }

        //Filters items by a price range, sets private field to the filtered list
        public List<Item> FilterByPrice(double minRange, double maxRange)
        {
            List<Item> result = _items.Where(item => item.Price >= minRange && item.Price <= maxRange).ToList();
            _items = result;
            return result;
        }
    }
}

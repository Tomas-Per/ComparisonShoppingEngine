using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManipulation
{
    public class Filter
    {
        private List<Item> _items { get; set; }

        public Filter(List<Item> items)
        {
            _items = items;
        }

        //Filters item list by a specific name and sets the private field to the filtered list
        public List<Item> FilterByName(string name)
        {
            List<Item> result = _items.Where(item => item.Name == name).ToList();
            _items = result;
            return result;
        }

        //Filters item list by the manufacturer and sets the private field to the filtered list
        public List<Item> FilterByManufacturer(string manufacturer)
        {
            List<Item> result = _items.Where(item => item.ManufacturerName == manufacturer).ToList();
            _items = result;
            return result;
        }
    }
}

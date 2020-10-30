using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManipulation
{
    public class Filter<T> where T : Item
    {
        protected List<T> _items { get; private set; }

        public Filter(List<T> items)
        {
            _items = items;
        }


        //Filters item list by the manufacturer and sets the private field to the filtered list
        public List<T> FilterByManufacturer(string manufacturer)
        {

            List<T> result = _items.Where(item => item.ManufacturerName == manufacturer).ToList();
            return result;
        }

        //Filters items by a price range, sets private field to the filtered list
        public List<T> FilterByPrice(double minRange, double maxRange)
        {

            List<T> result = _items.Where(item => item.Price >= minRange && item.Price <= maxRange).ToList();
            return result;
        }

        public void UpdateList(List<T> items)
        {
            _items = items;
        }

    }
}

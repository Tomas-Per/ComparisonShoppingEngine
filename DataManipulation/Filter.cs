using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace DataManipulation
{
    public class Filter<T> where T : Item
    {
        public delegate void FilterList(List<T> list);

        //Filters item list by the manufacturer and sets the private field to the filtered list
        public void FilterByManufacturer(string manufacturer, List<T> items, FilterList filter)
        {
            filter(items.Where(item => item.ManufacturerName == manufacturer).ToList());
        }

        //Filters items by a price range, sets private field to the filtered list
        public void FilterByPrice(double minRange, double maxRange, List<T> items, FilterList filter)
        {
            filter(items.Where(item => item.Price >= minRange && item.Price <= maxRange).ToList());
        }

    }
}

using ItemLibrary;
using System.Collections.Generic;
using System.Linq;

namespace DataManipulation
{
    public class Sorter
    {
        private List<Item> _items { get; set; }

        public Sorter(List<Item> items)
        {
            _items = items;
        }

        //Sorts items by their names in an ascending order
        public List<Item> SortByNameAsc()
        {
            List<Item> result = _items.OrderBy(x => x.Name).ToList();
            return result;
        }

        //Sorts items by their prices in an ascending order
        public List<Item> SortByPriceAsc()
        {
            List<Item> result = _items.OrderBy(x => x.Price).ToList();
            return result;
        }

        //Sorts items by their names in a descending order
        public List<Item> SortByNameDesc()
        {
            List<Item> result = _items.OrderByDescending(x => x.Name).ToList();
            return result;
        }

        //Sorts items by their prices in a descending order
        public List<Item> SortByPriceDesc()
        {
            List<Item> result = _items.OrderByDescending(x => x.Price).ToList();
            return result;
        }

        public void UpdateList(List<Item> items)
        {
            _items = items;
        }
    }
}

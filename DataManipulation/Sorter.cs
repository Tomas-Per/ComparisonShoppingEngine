using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataManipulation
{
    class Sorter
    {
        private List<Item> _items { get; set; }

        public Sorter(List<Item> items)
        {
            _items = items;
        }

        public List<Item> SortByNameAsc()
        {
            List<Item> result = _items.OrderBy(x => x.Name).ToList();
            return result;
        }

        public List<Item> SortByPriceAsc()
        {
            List<Item> result = _items.OrderBy(x => x.Price).ToList();
            return result;
        }


        public List<Item> SortByNameDesc()
        {
            List<Item> result = _items.OrderByDescending(x => x.Name).ToList();
            return result;
        }

        public List<Item> SortByPriceDesc()
        {
            List<Item> result = _items.OrderByDescending(x => x.Price).ToList();
            return result;
        }
    }
}

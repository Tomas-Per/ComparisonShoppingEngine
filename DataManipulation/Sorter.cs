using ItemLibrary;
using System;
using System.Collections.Generic;
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

    }
}

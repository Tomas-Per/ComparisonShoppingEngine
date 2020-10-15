using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopParser
{
    public interface IParser
    {
        public List<Item> parseShop(string url);
    }
}

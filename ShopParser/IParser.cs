using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopParser
{
    public interface IParser<T> where T :   Item
    {
        
        public List<T> ParseShop(string url);
    }
}

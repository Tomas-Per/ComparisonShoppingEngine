using ItemLibrary;
using System.Collections.Generic;

namespace ShopParser
{
    public interface IParser<T> where T :   Item
    { 
        public List<T> ParseShop();

        public T ParseWindow(T t);

    }
}

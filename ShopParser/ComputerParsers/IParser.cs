using ItemLibrary;
using System.Collections.Generic;

namespace WebParser.ComputerParsers
{
    public interface IParser<T> where T :   Item
    { 
        public List<T> ParseShop();

        public T ParseWindow(string url);
    }
}

using ModelLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebParser
{
    public interface IParser<T> where T : Item
    { 
        public Task<List<T>> ParseShop();

        public Task<T> ParseWindow(string url);
    }
}

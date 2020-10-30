using ItemLibrary;
using System.Collections.Generic;

namespace DataContent
{ 
    public interface IData<T> where T : Item
    {
        public List<T> ReadData(string path);
        public void WriteData(List<T> list);
    }
}

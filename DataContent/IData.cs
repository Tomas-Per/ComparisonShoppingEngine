using ItemLibrary;
using System.Collections.Generic;

namespace DataContent
{ 
    public interface IData<T> where T : Item
    {
        public List<T> ReadData();
        public void WriteData(List<T> list);
    }
}

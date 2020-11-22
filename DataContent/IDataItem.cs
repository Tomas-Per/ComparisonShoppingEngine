using ItemLibrary;
using System.Collections.Generic;

namespace DataContent
{ 
    public interface IDataItem<T> where T : Item
    {
        public IEnumerable<T> ReadData();
        public void WriteData(IEnumerable<T> list);
        public T GetDataByID(int id);
        public void UpdateData(T ob);
        public void DeleteData(int id);
    }
}

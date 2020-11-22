using ItemLibrary;
using System.Collections.Generic;

namespace DataContent
{ 
    public interface IData<T> where T : Item
    {
        public T ReadData();
        public void WriteData(T list);
        public object GetDataByID(int id);
        public void UpdateData(object ob);
        public void DeleteData(int id);
    }
}

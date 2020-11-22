using System.Collections.Generic;

namespace DataContent
{ 
    public interface IData<T>
    {
        public IEnumerable<T> ReadData();
        public void WriteData(IEnumerable<T> list);
        public T GetDataByID(int id);
        public void UpdateData(T obj);
        public void DeleteData(int id);
    }
}

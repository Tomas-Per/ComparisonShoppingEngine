using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent
{
    interface IDataComponent<T> where T : Component
    {
        public IEnumerable<T> ReadData();
        public void WriteData(IEnumerable<T> list);
        public T GetDataByID(int id);
        public void UpdateData(T obj);
        public void DeleteData(int id);
    }
}

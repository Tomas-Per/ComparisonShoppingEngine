using System.Collections.Generic;

namespace DataContent
{ 
    public interface IData<T> where T : IEnumerable<object>
    {
        public T ReadData();
        public void WriteData(T list);
    }
}

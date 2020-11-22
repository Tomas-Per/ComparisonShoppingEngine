using System.Collections.Generic;

namespace DataContent
{ 
    public interface IData<T>
    {
        public IEnumerable<T> ReadData();
        public void WriteData(IEnumerable<T> list);
    }
}

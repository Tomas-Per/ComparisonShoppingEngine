using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContent
{ 
    public interface IReadData<T> where T :Item
    {
        public List<T> ReadData(string path);
        public void WriteCSVFile(string path, List<T> list);
    }
}

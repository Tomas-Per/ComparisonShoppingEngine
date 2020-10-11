using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContent
{ 
    interface IReadData
    {
        public List<Item> ReadData(string path);
    }
}

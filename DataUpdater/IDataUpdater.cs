using ItemLibrary;
using System.Collections.Generic;

namespace DataUpdater
{
    public interface IDataUpdater<T> where T : Item
    {
        public List<T> GetItemListFromWeb();
        public void UpdateItemListFile(List<T> data);
    }
}

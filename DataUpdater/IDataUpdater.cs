using ItemLibrary;
using System.Collections.Generic;

namespace DataUpdater
{
    public interface IDataUpdater<T> where T : Item
    {
        public List<T> GetComputerListFromWeb();
        public void UpdateComputerListFile(List<T> data);
    }
}

using ShopParser;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using DataContent.ReadingCSV;
using System.IO;

namespace DataUpdater
{
    public class ComputerDataUpdater : IDataUpdater<Computer>
    {
        IParser<Computer> _parser;

        public ComputerDataUpdater (IParser<Computer> parser)
        {
            _parser = parser;
        }

        public List<Computer> GetComputerListFromWeb()
        {
            List<Computer> data = _parser.ParseShop();
            return data;
        }

        public void UpdateComputerListFile(List<Computer> data)
        {
            new LaptopServiceCSV(MainPath.GetComputerPath(), FileMode.Append).WriteData(GetComputerListFromWeb());
        }
    }
}


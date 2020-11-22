using WebParser.ComputerParsers;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using PathLibrary;
using WebParser;

namespace DataUpdater
{
    public class ComputerDataUpdater : IDataUpdater<Computer>
    {
        private IParser<Computer> _parser { get; set; }

        public ComputerDataUpdater (IParser<Computer> parser)
        {
            _parser = parser;
        }


        //calls shop parser and returns parsed item list
        public List<Computer> GetItemListFromWeb()
        {
            List<Computer> data = _parser.ParseShop();
            return data;
        }

        //updates CSV file with new data
        public void UpdateItemListFile(List<Computer> data)
        {
            new LaptopServiceCSV(MainPath.GetComputerPath()).WriteData(data);
        }
    }
}


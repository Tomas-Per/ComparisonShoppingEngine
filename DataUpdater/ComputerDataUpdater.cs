using WebParser.ComputerParsers;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using PathLibrary;
using WebParser;
using DataContent.ReadingDB.Services;
using DataContent;

namespace DataUpdater
{
    public class ComputerDataUpdater : IDataUpdater<Computer>
    {
        private IParser<Computer> _parser { get; set; }
        private IData<IEnumerable<Computer>> _dataService;

        public ComputerDataUpdater (IParser<Computer> parser, IData<IEnumerable<Computer>> dataService)
        {
            _parser = parser;
            _dataService = dataService;
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
            _dataService.WriteData(data);
        }
    }
}


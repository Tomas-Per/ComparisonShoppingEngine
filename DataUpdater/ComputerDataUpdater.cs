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
    public class ComputerDataUpdater<T> where T : Item
    {
        private IParser<T> _parser { get; set; }
        private IData<IEnumerable<T>> _dataService;

        public ComputerDataUpdater (IParser<T> parser, IData<IEnumerable<T>> dataService)
        {
            _parser = parser;
            _dataService = dataService;
        }


        //calls shop parser and returns parsed item list
        public List<T> GetItemListFromWeb()
        {
            List<T> data = _parser.ParseShop();
            return data;
        }

        //updates CSV file with new data
        public void UpdateItemListFile(List<T> data)
        {
            _dataService.WriteData(data);
        }
    }
}


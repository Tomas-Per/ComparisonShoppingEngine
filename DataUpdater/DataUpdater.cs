using WebParser.ComputerParsers;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using PathLibrary;
using WebParser;
using DataContent.ReadingDB.Services;
using DataContent;
using static ItemLibrary.Categories;

namespace DataUpdater
{
    public class DataUpdater<T> where T : Item
    {
        private IParser<T> _parser { get; set; }
        private IData<IEnumerable<T>> _dataService;

        public DataUpdater(IParser<T> parser, IData<IEnumerable<T>> dataService)
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

        public List<T> GetItemCategoryListFromWeb(ItemCategory itemCategory)
        {
            List<T> data = new List<T>();

            switch (itemCategory)
            {
                case ItemCategory.Laptop:

                    
                    return null;

                case ItemCategory.Smartphone:


                    return null;


                case ItemCategory.DesktopComputer:

                    return null;

                default:
                    return null;
            }
        }
    }
}   
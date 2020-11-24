using WebParser.ComputerParsers;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using PathLibrary;
using WebParser;
using DataContent.ReadingDB.Services;
using DataContent;
using static ItemLibrary.Categories;
using System.Linq;
using System;
using System.Threading.Tasks;
using WebParser.SmartphoneParsers;

namespace DataUpdater
{
    public class DataUpdater<T> where T : Item
    {
        private IData<IEnumerable<T>> _dataService;
        private ItemCategory _itemCategory { get; set; }

        public DataUpdater(IData<IEnumerable<T>> dataService, ItemCategory itemCategory)
        {
            _dataService = dataService;
            _itemCategory = itemCategory;


        }

        //calls shop parser and returns parsed item list
        public async Task<List<T>> GetItemListFromWeb(IParser<T> parser)
        {
            List<T> data = await parser.ParseShop();
            return data;
        }

        //updates DB with new data
        public void UpdateItemListFile(List<T> data)
        {
            _dataService.WriteData(data);
        }

        //calls shop parser for a spcecific item category and returns parsed item list
        public async Task<List<T>> GetItemCategoryListFromWebAsync()
        {
            switch (_itemCategory)
            {
                case ItemCategory.Laptop:

                    List<Task<List<Computer>>> laptopTasks = new List<Task<List<Computer>>>();

                    laptopTasks.Add(Task.Run(() => new SenukaiComputerParser().ParseShop()));
                    laptopTasks.Add(Task.Run(() => new AvitelaComputerParser().ParseShop()));
                    laptopTasks.Add(Task.Run(() => new PiguComputerParser().ParseShop()));
                    
                    var laptopData = await Task.WhenAll(laptopTasks);
                    return laptopData.Cast<T>().ToList();

                case ItemCategory.Smartphone:

                    List<Task<List<Smartphone>>> smartphoneTasks = new List<Task<List<Smartphone>>>();

                    smartphoneTasks.Add(Task.Run(() => new SenukaiSmartphoneParser().ParseShop()));
                    smartphoneTasks.Add(Task.Run(() => new AvitelaSmartphoneParser().ParseShop()));
                    smartphoneTasks.Add(Task.Run(() => new PiguSmartphoneParser().ParseShop()));

                    var smartphoneData = await Task.WhenAll(smartphoneTasks);
                    return smartphoneData.Cast<T>().ToList();


                case ItemCategory.DesktopComputer:

                    return null;

                default:
                    return null;
            }
        }
    }
}   
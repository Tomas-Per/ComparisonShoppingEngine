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

                    var time = DateTime.Now;

                    var data = new List<Computer>();
 

                    List<Task> tasks = new List<Task>();

                    tasks.Add(Task.Run(() => new SenukaiComputerParser().ParseWindow("https://www.senukai.lt/p/lenovo-ideapad-3-15ada-81w1005jpb-pl/eya2?cat=5ei&index=2")));
                    tasks.Add(Task.Run(() => new AvitelaComputerParser().ParseWindow("https://avitela.lt/kompiuterine-technika/nesiojamieji-kompiuteriai/nesiojamieji-kompiuteriai-zaidimams/nitro-5-an515-54-acer-15-6-fhd-i5-9300h-8gb-1tb-128gb-ssd-geforce-1650-4gb-win10h-en-black-ne-komp")));
                    tasks.Add(Task.Run(() => new PiguComputerParser().ParseWindow("https://pigu.lt/lt/kompiuteriai/nesiojami-kompiuteriai/nesiojamas-kompiuteris-hp-17-by3053cl?id=34110896")));

                    await Task.WhenAll(tasks);

                    Console.WriteLine(DateTime.Now - time);

                   return data.Cast<T>().ToList();

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
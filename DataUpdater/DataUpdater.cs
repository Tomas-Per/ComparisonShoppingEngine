using WebParser.ComputerParsers;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using PathLibrary;
using WebParser;
using DataContent;
using static ItemLibrary.Categories;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace DataUpdater
{
    public class DataUpdater<T> where T : Item
    {
        private HttpClient _httpClient;

        private ItemCategory _itemCategory { get; set; }

        public DataUpdater(ItemCategory itemCategory)
        {
            _itemCategory = itemCategory;
            _httpClient = new HttpClient();
        }

        //calls shop parser and returns parsed item list
        public async Task<List<T>> GetItemListFromWeb(IParser<T> parser)
        {
            List<T> data = await parser.ParseShop();
            return data;
        }

        //updates DB with new data
        public async Task UpdateItemListFile(List<T> data)
        {
            
            //switch
            if (typeof(Computer) == typeof(T))
            {
                var url = "https://localhost:44315/api/Computers";
                var json = JsonConvert.SerializeObject(data);
                var postData = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, postData); ;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }

            }
            else if (typeof(Smartphone) == typeof(T))
            {
                var url = "https://localhost:44315/api/Smartphones";
                var json = JsonConvert.SerializeObject(data);
                var postData = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, postData); ;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            }
        }

        //calls shop parser for a spcecific item category and returns parsed item list
        public async Task<List<T>> GetItemCategoryListFromWebAsync()
        {
            switch (_itemCategory)
            {
                case ItemCategory.Laptop:

                    List<Task<List<Computer>>> laptopTasks = new List<Task<List<Computer>>>();

                    //laptopTasks.Add(Task.Run(() => new SenukaiComputerParser().ParseShop()));
                    laptopTasks.Add(Task.Run(() => new AvitelaComputerParser().ParseShop()));
                    laptopTasks.Add(Task.Run(() => new PiguComputerParser().ParseShop()));
                    
                    var laptopData = await Task.WhenAll(laptopTasks);

                    List<Computer> results = new List<Computer>();
                    results = laptopData[0].Concat(laptopData[1]).ToList();

                    return results.Cast<T>().ToList();
                    //return laptopData.ToList().Cast<T>().ToList();




                    //this ItemCategory still needs to be tested
                case ItemCategory.Smartphone:

                    List<Task<List<Smartphone>>> smartphoneTasks = new List<Task<List<Smartphone>>>();

                    //smartphoneTasks.Add(Task.Run(() => new SenukaiSmartphoneParser().ParseShop()));
                    //smartphoneTasks.Add(Task.Run(() => new AvitelaSmartphoneParser().ParseShop()));
                    //smartphoneTasks.Add(Task.Run(() => new PiguSmartphoneParser().ParseShop()));

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
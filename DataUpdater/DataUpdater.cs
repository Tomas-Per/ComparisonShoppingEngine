using ItemLibrary;
using System.Collections.Generic;
using WebParser;
using static ItemLibrary.Categories;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using WebParser.SmartphoneParsers;
using WebParser.LaptopParsers;

namespace DataUpdater
{
    public class DataUpdater<T> where T : Item
    {
        private HttpClient _httpClient;

        public DataUpdater()
        {
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
        public async Task<List<T>> GetItemCategoryListFromWebAsync(ItemCategory itemCategory)
        {
            switch (itemCategory)
            {
                case ItemCategory.Laptop:

                    List<Task<List<Computer>>> laptopTasks = new List<Task<List<Computer>>>();

                    laptopTasks.Add(Task.Run(() => new SenukaiLaptopParser().ParseShop()));
                    laptopTasks.Add(Task.Run(() => new AvitelaLaptopParser().ParseShop()));
                    laptopTasks.Add(Task.Run(() => new PiguLaptoprParser().ParseShop()));
                    
                    var laptopData = await Task.WhenAll(laptopTasks);

                    List<Computer> computerResults = new List<Computer>();
                    laptopData.ToList().ForEach(elem => elem.ForEach(item => computerResults.Add(item)));

                    return computerResults.Cast<T>().ToList();


                case ItemCategory.Smartphone:

                    List<Task<List<Smartphone>>> smartphoneTasks = new List<Task<List<Smartphone>>>();

                    smartphoneTasks.Add(Task.Run(() => new AvitelaSmartphoneParser().ParseShop()));
                    smartphoneTasks.Add(Task.Run(() => new PiguSmartphoneParser().ParseShop()));
                    smartphoneTasks.Add(Task.Run(() => new SenukaiSmartphoneParser().ParseShop()));
                    
                    var smartphoneData = await Task.WhenAll(smartphoneTasks);


                    List<Smartphone> smartphoneResults = new List<Smartphone>();
                    smartphoneData.ToList().ForEach(elem => elem.ForEach(item => smartphoneResults.Add(item)));

                    return smartphoneResults.Cast<T>().ToList();


                case ItemCategory.DesktopComputer:

                    return null;

                default:
                    return null;
            }
        }
    }
}   
using WebParser.ComputerParsers;
using ModelLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using PathLibrary;
using WebParser;
using DataContent;
using static ModelLibrary.Categories;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using WebParser.SmartphoneParsers;
using DataContent.DAL.Helpers;

using DataContent.DAL.Access;

namespace DataUpdater
{
    public class DataUpdater<T> where T : Item
    {
        private ComputerAccess _computerAccess;
        private SmartphoneAccess _smartphoneAccess;

        public DataUpdater()
        {
            _computerAccess = new ComputerAccess();
            _smartphoneAccess = new SmartphoneAccess();
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
                await _computerAccess.PostComputers(data.Cast<Computer>().ToList());
            }
            else if (typeof(Smartphone) == typeof(T))
            {
                await _smartphoneAccess.PostSmartphones(data.Cast<Smartphone>().ToList());
            }
        }

        //calls shop parser for a spcecific item category and returns parsed item list
        public async Task<List<T>> GetItemCategoryListFromWebAsync(ItemCategory itemCategory)
        {
            switch (itemCategory)
            {
                case ItemCategory.Laptop:

                    List<Task<List<Computer>>> laptopTasks = new List<Task<List<Computer>>>();

                    laptopTasks.Add(Task.Run(() => new SenukaiComputerParser(itemCategory).ParseShop()));
                    laptopTasks.Add(Task.Run(() => new AvitelaComputerParser(itemCategory).ParseShop()));
                    laptopTasks.Add(Task.Run(() => new PiguComputerParser(itemCategory).ParseShop()));
                    
                    var laptopData = await Task.WhenAll(laptopTasks);

                    List<Computer> laptopResults = new List<Computer>();
                    laptopData.ToList().ForEach(elem => elem.ForEach(item => laptopResults.Add(item)));

                    return laptopResults.Cast<T>().ToList();


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

                    List<Task<List<Computer>>> desktopPCTasks = new List<Task<List<Computer>>>();

                    desktopPCTasks.Add(Task.Run(() => new SenukaiComputerParser(itemCategory).ParseShop()));
                    desktopPCTasks.Add(Task.Run(() => new AvitelaComputerParser(itemCategory).ParseShop()));
                    desktopPCTasks.Add(Task.Run(() => new PiguComputerParser(itemCategory).ParseShop()));

                    var desktopPCData = await Task.WhenAll(desktopPCTasks);

                    List<Computer> desktopPCResults = new List<Computer>();
                    desktopPCData.ToList().ForEach(elem => elem.ForEach(item => desktopPCResults.Add(item)));

                    return desktopPCResults.Cast<T>().ToList();

                default:
                    return null;
            }
        }
    }
}   
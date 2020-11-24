﻿using WebParser.ComputerParsers;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using System.Collections.Generic;
using PathLibrary;
using WebParser;
using DataContent.ReadingDB.Services;
using DataContent;
using static ItemLibrary.Categories;
using System.Linq;

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
        public List<T> GetItemListFromWeb(IParser<T> parser)
        {
            List<T> data = parser.ParseShop();
            return data;
        }

        //updates DB with new data
        public void UpdateItemListFile(List<T> data)
        {
            _dataService.WriteData(data);
        }

        //calls shop parser for a spcecific item category and returns parsed item list
        public List<T> GetItemCategoryListFromWeb()
        {
            //List<T> data = new List<T>();

            switch (_itemCategory)
            {
                case ItemCategory.Laptop:
                    var data = new List<Computer>();
                    var item1 = new SenukaiComputerParser().ParseWindow("https://www.senukai.lt/p/lenovo-ideapad-3-15ada-81w1005jpb-pl/eya2?cat=5ei&index=2");
                    var item2 = new AvitelaComputerParser().ParseWindow("https://avitela.lt/kompiuterine-technika/nesiojamieji-kompiuteriai/nesiojami-kompiuteriai/nesiojamasis-kompiuteris-acer-aspire-a514-53-54z4-silver-i5-1035g1-8gb-256gb-ssd-win10");
                    var item3 = new PiguComputerParser().ParseWindow("https://pigu.lt/lt/kompiuteriai/nesiojami-kompiuteriai/nesiojamas-kompiuteris-hp-17-by3053cl?id=34110896");

                    data.Add(item1);
                    

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
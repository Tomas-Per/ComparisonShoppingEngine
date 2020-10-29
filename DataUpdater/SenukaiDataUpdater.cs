using ShopParser;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DataContent.ReadingCSV.Services;
using DataContent.ReadingCSV;

namespace DataUpdater
{
    public class SenukaiDataUpdater : IDataUpdater
    {
        public void update()
        {
            IParser<Computer> parser = new SenukaiParser();

            var data = parser.ParseShop();

            var path = MainPath.GetMainPath() + @"\Data\SenukaiData.csv";

            new LaptopServiceCSV().WriteCSVFile(path, data);
        }
    }
}


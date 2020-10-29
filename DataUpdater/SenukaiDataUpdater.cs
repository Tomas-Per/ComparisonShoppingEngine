using ShopParser;
using ItemLibrary;
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


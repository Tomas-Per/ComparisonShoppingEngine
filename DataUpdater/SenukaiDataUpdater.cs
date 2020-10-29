using ShopParser;
using ItemLibrary;
using DataContent.ReadingCSV.Services;
using DataContent.ReadingCSV;

namespace DataUpdater
{
    public class SenukaiDataUpdater : IDataUpdater
    {

        //starts parsing senukai.lt laptops and writes parsed data to a file
        public void update()
        {
            IParser<Computer> parser = new SenukaiParser();

            var data = parser.ParseShop();

            var path = MainPath.GetMainPath() + @"\Data\SenukaiData.csv";

            new LaptopServiceCSV().WriteCSVFile(path, data);
        }
    }
}


using System;
using System.IO;

namespace PathLibrary
{
    public class MainPath
    {
        //returns project's main path
        public static string GetMainPath()
        {
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            while(!_filePath.EndsWith("ComparisonShoppingEngine"))
            {
                _filePath = Directory.GetParent(_filePath).FullName;
                if(!_filePath.Contains("ComparisonShoppingEngine"))
                {
                    throw new Exception("Couldn't reach main path") ;
                }
            }
            return _filePath;
        }
        public static string GetComputerPath()              // should be added and avitela path
        {
            return GetMainPath() + @"\Data\senukai.csv";
        }
        public static string GetBrandPath()
        {
            return GetMainPath() + @"\Data\brands.csv";
        }
        public static string GetProcessorPath()
        {
            return GetMainPath() + @"\Data\processorFilters.csv";
        }
        public static string GetShopParserPath()
        {
            return GetMainPath() + @"\ShopParser\bin\Debug\net5.0\chromedriver.exe";
        }
    }
}

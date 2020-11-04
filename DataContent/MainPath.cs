using System.IO;
using ExceptionsLibrary;

namespace DataContent.ReadingCSV
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
                    throw new InnerCustomException("Couldn't reach main path", null) ;
                }
            }
            return _filePath;
        }
        public static string GetComputerPath()
        {
            return GetMainPath() + @"\Data\senukai.csv";
        }
    }
}

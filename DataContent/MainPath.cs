using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExceptionsLibrary;

namespace DataContent.ReadingCSV
{
    public class MainPath
    {
        public static string GetMainPath()
        {
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            while(!_filePath.EndsWith("ComparisonShoppingEngine"))
            {
                _filePath = Directory.GetParent(_filePath).FullName;
                if(!_filePath.Contains("ComparisonShoppingEngine"))
                {
                    throw new DataCustomException("Couldn't reach main path", null);
                }
            }
            return _filePath;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExceptionsLibrary;

namespace DataContent.ReadingCSV
{
    class MainPath
    {
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
    }
}

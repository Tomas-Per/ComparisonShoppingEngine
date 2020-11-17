using ItemLibrary.DataContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContent.ReadingCSV.Services
{
    class ComputerDataService
    {
        static void Main()
        {
            using var db = new ComputerContext();
            Console.WriteLine("Done");

        }
    }
}

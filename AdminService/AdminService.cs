using DataContent.ReadingCSV.Services;
using DataUpdater;
using ExceptionsLogging;
using WebParser.ComputerParsers;
using System;
using System.Collections.Generic;
using System.IO;
using PathLibrary;
using WebParser.ComponentsParser;
using DataContent.ReadingDB.Services;
using ItemLibrary;
using static ItemLibrary.Categories;
using System.Threading.Tasks;

namespace AdminService
{
    //Exe class which is for admins only. this class controls data updates
    public class AdminService
    {
        public static string _helpMessage = "1 - parse Laptops from shops" +
                                            "\n5 - Update Processor in database" +
                                            "\n0 - close program";

        public static async Task Main(string[] args)
        {
            string command;


            do
            {
                command = Console.ReadLine();

                switch (command)
                {
                    case "!help":
                        Console.WriteLine(_helpMessage);
                        break;

                    case "0":
                        break;

                    case "1":
                        try
                        {
                            var updater = new DataUpdater<Computer>(new ComputerDataService(), ItemCategory.Laptop);
                            var results = await updater.GetItemCategoryListFromWebAsync();
                            updater.UpdateItemListFile(results);
                        }
                        catch (Exception ex)
                        {
                            ExceptionLogger.Log(ex);
                        }

                        Console.WriteLine("Shop Parsed");
                        break;

                    case "5":
                        Console.WriteLine("Type processor ID");
                        var service = new ProcessorDataService();
                        var processor = service.GetDataByID(Int32.Parse(Console.ReadLine()));
                        Console.WriteLine("Write Name, Model, Cache, Cores");
                        var specs = Console.ReadLine().Split(',');
                        processor.Name = specs[0];
                        processor.Model = specs[1];
                        processor.Cache = Int32.Parse(specs[2]);
                        processor.MinCores = Int32.Parse(specs[3]);
                        service.UpdateData(processor);
                        Console.WriteLine("Updated");
                        break;

                    default:
                        Console.WriteLine("Wrong input, use !help to learn about commands");
                        break;
                        
                }
            } while (command != "0");

            Environment.Exit(0);
        }

    }
}

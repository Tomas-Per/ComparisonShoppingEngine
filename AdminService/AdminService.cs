using ExceptionsLogging;
using System;
using System.Collections.Generic;
using System.IO;
using PathLibrary;
using WebParser.ComponentsParser;
using ModelLibrary;
using static ModelLibrary.Categories;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;

namespace AdminService
{
    //Exe class which is for admins only. this class controls data updates
    public class AdminService
    {
        public static string _helpMessage = "1 - parse Laptops from shops" +
                                            "\n2 - parse Smartphones from shops" +
                                            "\n3 - parse Desktop Computers from shops" +
                                            "\n5 - Update Processor in database" +
                                            "\n0 - close program";

        public static async Task Main(string[] args)
        {
            string command;

            do
            {
                command = Console.ReadLine();

                var updater = new DataUpdater.DataUpdater();
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
                            var results = await updater.GetItemCategoryListFromWebAsync<Computer>(ItemCategory.Laptop);
                            await updater.UpdateItemListFile(results);
                            Console.WriteLine("Shop Parsed");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something wrong happened. Check Logs");
                            ExceptionLogger.Log(ex);
                        }
                        break;


                    case "2":
                        try
                        {
                            var results = await updater.GetItemCategoryListFromWebAsync<Smartphone>(ItemCategory.Smartphone);
                            await updater.UpdateItemListFile(results);
                            Console.WriteLine("Shop Parsed");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something wrong happened. Check Logs");
                            ExceptionLogger.Log(ex);
                        }
                        break;

                    case "3":
                        try
                        {
                            var results = await updater.GetItemCategoryListFromWebAsync<Computer>(ItemCategory.DesktopComputer);
                            await updater.UpdateItemListFile(results);
                            Console.WriteLine("Shop Parsed");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something wrong happened. Check Logs");
                            ExceptionLogger.Log(ex);
                        }
                        break;

                    case "5":
                        Processor processor = new Processor();

                        Console.WriteLine("Type processor ID");
                        processor.Id = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Write Name, Model, Cache, Cores");
                        var specs = Console.ReadLine().Split(',');
                        processor.Name = specs[0];
                        processor.Model = specs[1];
                        processor.Cache = Int32.Parse(specs[2]);
                        processor.MinCores = Int32.Parse(specs[3]);
          
                        if(await updater.UpdateProcessor(processor))
                        {
                            Console.WriteLine("Updated");
                        }
                        else Console.WriteLine("something wrong happened");   
                        
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

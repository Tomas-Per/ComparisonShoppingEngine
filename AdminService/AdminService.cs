﻿using DataContent.ReadingCSV.Services;
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
using WebParser.SmartphoneParsers;
using static ItemLibrary.Categories;
using System.Threading.Tasks;

namespace AdminService
{
    //Exe class which is for admins only. this class controls data updates
    public class AdminService
    {
        public static string _helpMessage = "1 - parse Laptops from Senukai" +
                                            "5 - Update Processor in database" +
                                            "\n0 - close program";

        public static async Task Main(string[] args)
        {
            string command;

            var updaterr = new DataUpdater<Computer>(new ComputerDataService(), ItemCategory.Laptop);

            await updaterr.GetItemCategoryListFromWebAsync();


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
                        //var updater = new DataUpdater<Computer>(new ComputerDataService(), ItemCategory.Laptop);
                        //updater.UpdateItemListFile(updater.GetItemListFromWeb(new SenukaiComputerParser()));
                        Console.WriteLine("Shop Parsed");
                        break;

                    case "5":
                        Console.WriteLine("Type processor ID");
                        var service = new ProcessorDataService();
                        var processor = service.GetProccesorByID(Int32.Parse(Console.ReadLine()));
                        Console.WriteLine("Write Name, Model, Cache, Cores");
                        var specs = Console.ReadLine().Split(',');
                        processor.Name = specs[0];
                        processor.Model = specs[1];
                        processor.Cache = Int32.Parse(specs[2]);
                        processor.MinCores = Int32.Parse(specs[3]);
                        service.UpdateProcessor(processor);
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

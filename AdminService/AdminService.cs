using DataUpdater;
using ShopParser;
using System;
using System.IO;

namespace AdminService
{
    //Exe class which is for admins only. this class controls data updates
    public class AdminService
    {
        public static string _helpMessage = "1 - parse Laptops from Senukai" +
                                            "\n0 - close program";

        public static void Main(string[] args)
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
                        var updater = new ComputerDataUpdater(new SenukaiParser());
                        updater.UpdateItemListFile(updater.GetItemListFromWeb());
                        Console.WriteLine("Shop Parsed");
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

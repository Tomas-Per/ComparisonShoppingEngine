using DataUpdater;
using ExceptionsLogging;
using System;
using ItemLibrary;
using static ItemLibrary.Categories;
using System.Threading.Tasks;


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
                            var updater = new DataUpdater<Computer>();
                            var results = await updater.GetItemCategoryListFromWebAsync(ItemCategory.Laptop);
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
                            var updater = new DataUpdater<Smartphone>();
                            var results = await updater.GetItemCategoryListFromWebAsync(ItemCategory.Smartphone);
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
                            var updater = new DataUpdater<Computer>();
                            var results = await updater.GetItemCategoryListFromWebAsync(ItemCategory.DesktopComputer);
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
                        //Console.WriteLine("Type processor ID");
                        //var service = new ProcessorDataService();
                        //var processor = service.GetDataByID(Int32.Parse(Console.ReadLine()));
                        //Console.WriteLine("Write Name, Model, Cache, Cores");
                        //var specs = Console.ReadLine().Split(',');
                        //processor.Name = specs[0];
                        //processor.Model = specs[1];
                        //processor.Cache = Int32.Parse(specs[2]);
                        //processor.MinCores = Int32.Parse(specs[3]);
                        //service.UpdateData(processor);
                        //Console.WriteLine("Updated");
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

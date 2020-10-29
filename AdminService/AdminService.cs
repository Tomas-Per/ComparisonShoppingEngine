using DataUpdater;
using System;

namespace AdminService
{
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

                    case "1":
                        
                        new SenukaiDataUpdater().update();
                        Console.WriteLine("Senukai Parsed");
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

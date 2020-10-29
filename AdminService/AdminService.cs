
using DataUpdater;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

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

                if (command == "!help")
                {
                    Console.WriteLine(_helpMessage);
                }

                else if (command == "1")
                {
                    new SenukaiDataUpdater().update();
                    Console.WriteLine("Senukai Parsed");
                }

                else if (command == "0")
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Wrong input, use !help to learn about commands");
                }

            } while (true);

            Environment.Exit(0);
        }

    }
}

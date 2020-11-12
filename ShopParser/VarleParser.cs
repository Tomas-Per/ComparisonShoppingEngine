using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using static ItemLibrary.Categories;

namespace ShopParser
{
    public class VarleParser : IParser<Computer>
    {
        private readonly string _url = "https://www.varle.lt/nesiojami-kompiuteriai/nesiojami-kompiuteriai/?p=1";
        private IWebDriver _driver;
        private string _currentWIndowURL;

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();

            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_url);

            string nextPage;

            try
            {
                nextPage = _driver.FindElement(By.XPath("//a[contains(@class, 'next')]")).GetAttribute("href");
            }

            catch (Exception)
            {
                nextPage = _currentWIndowURL;
            }

            var elements = _driver.FindElements(By.ClassName("title"));
            var links = new List<String>();

            foreach (var elem in elements)
            {
                links.Add(elem.GetAttribute("href"));
            }

            foreach (var link in links)
            {
                _driver.Navigate().GoToUrl(link);

                Computer computer = new Computer { ItemURL = link, ItemCategory = ItemCategory.Computer, ComputerCategory = ComputerCategory.Laptop};
                ParseWindow(computer);

                _driver.Navigate().Back();
            }




            _driver.Navigate().GoToUrl(nextPage);
            _driver.Close();

            return data;
        }

        public Computer ParseWindow(Computer computer)
        {
            computer.Name = _driver.FindElement(By.ClassName("title")).Text;
            computer.Price = _driver.FindElement(By.XPath("//meta[@itemprop='price']")).GetAttribute("content").ParseDouble();

            var table = _driver.FindElements(By.ClassName("spec-line"));

            Console.WriteLine("-------------------------");

            foreach (var item in table)
            {
                var title = item.FindElement(By.ClassName("title")).Text;
                var text = item.FindElement(By.ClassName("text")).Text;

                if (title.Contains("Gamintojas"))
                {
                    computer.ManufacturerName = text;
                    Console.WriteLine("Manufacturer");
                    Console.WriteLine(computer.ManufacturerName);
                }

                else if (title.Contains("Ekrano raiška"))
                {
                    computer.Resolution = text;
                    Console.WriteLine("resoliution");
                    Console.WriteLine(computer.Resolution);
                }

                else if (title.Contains("Procesoriaus šeima"))
                {
                    computer.ProcessorName = text;
                    Console.WriteLine("procesor");
                    Console.WriteLine(computer.ProcessorName);
                }

                //else if (title.Contains("Operatyvioji atmintis	"))
                //{
                //    computer.RAM_type = text;
                //    Console.WriteLine("ram type");
                //    Console.WriteLine(computer.RAM_type);
                //}

                //else if (table[i].Text.Contains("Maksimali vidinė atmintis"))
                //{
                //    computer.RAM = table[i + 1].Text.ParseInt();
                //    Console.WriteLine("ram");
                //    Console.WriteLine(computer.RAM);
                //}

                else if (title.Contains("Bendra saugyklos talpa"))
                {
                    computer.StorageCapacity = text.ParseInt();
                    Console.WriteLine("storage");
                    Console.WriteLine(computer.StorageCapacity);
                }

                else if (title.Contains("Vaizdo plokštė"))
                {
                    computer.GraphicsCardName = text;
                    Console.WriteLine("card name ");
                    Console.WriteLine(computer.GraphicsCardName);
                }
            }
            return computer;
        }
    }
}

using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Parsing;
using static ItemLibrary.Categories;

namespace ShopParser
{
    public class AvitelaParser : IParser<Computer>
    {
        private readonly string _url = "https://avitela.lt/kompiuterine-technika/nesiojamieji-kompiuteriai/nesiojami-kompiuteriai?page=1";
        private IWebDriver _driver;

        //parses laptops from avitela.lt and returns results in a List<Computer>
        //this method parses first 5 pages (18 laptops in every page), because later pages are outdated 
        public List<Computer> ParseShop()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver(options);

            List<Computer> data = new List<Computer>();

            List<string> links = new List<string>();

            string nextPage = _url;

            _driver.Navigate().GoToUrl(_url);

            for (int i = 1; i <= 5; i++)
            {
                nextPage = _url.Remove(_url.Length - 1, 1) + i;
                _driver.Navigate().GoToUrl(nextPage);

                var names = _driver.FindElements(By.CssSelector("div.right > div.name > a"));

                foreach (var name in names)
                {
                    var elem = name.GetAttribute("onclick");    
                    var stringStart =elem.Substring(elem.IndexOf("http"));

                    links.Add(stringStart.Substring(0, stringStart.IndexOf("\'")));
                }

                foreach (var link in links)
                {
                    _driver.Navigate().GoToUrl(link);

                    Computer computer =ParseWindow(link);

                    computer.ItemCategory = ItemCategory.Computer;
                    computer.ComputerCategory = ComputerCategory.Laptop;

                    if (computer.Resolution != null)
                    {
                        data.Add(computer);
                    }
                    _driver.Navigate().Back();
                }
                
            }

            _driver.Close();
            return data;
        }


        //parses laptop window, updates computer fields
        public Computer ParseWindow(string url)
        {

            Computer computer = new Computer();
            computer.Name = _driver.FindElement(By.Id("pname")).Text;
            computer.Price = _driver.FindElement(By.Id("price-old")).Text.ParseDouble();
            computer.ItemURL = url;
            computer.ShopName = "Avitela";

            try
            {
                computer.ImageLink = _driver.FindElement(By.CssSelector("div.product-image-right.product-photo")).
                    FindElement(By.CssSelector("a")).GetAttribute("href");
            }
            catch (Exception)
            {
                computer.ImageLink = "https://ksd-images.lt/display/aikido/store/1e3628060337b388dd4ffbce4f20f608.jpg?h=742&w=816";
            }

            var table = _driver.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Equals("Vaizdo plokštė"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Operat. atminties tipas"))
                {
                    computer.RAM_type = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Operatyvioji atmintis"))
                {
                    computer.RAM = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Vidinė atmintis"))
                {
                    computer.StorageCapacity = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Ekrano raiška"))
                {
                    computer.Resolution = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Procesoriaus tipas"))
                {
                    computer.Processor = new Processor { Name = table[i + 1].Text };
                }

                else if (computer.GraphicsCardName == null && table[i].Text.Contains("Vaizdo plokštės tipas"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }

                else if (computer.Processor.Name == null && table[i].Text.Contains("Procesoriaus modelis"))
                {
                    computer.Processor = new Processor { Name = table[i + 1].Text };
                }

            }
            return computer;
        }

    }
}

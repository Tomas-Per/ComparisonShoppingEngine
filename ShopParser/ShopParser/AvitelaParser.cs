using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using Parsing;
using static ItemLibrary.Categories;
using System.Linq;
using PathLibrary;
using DataContent.ReadingDB.Services;

namespace WebParser.ShopParser
{
    public class AvitelaParser : IParser<Computer>
    {
        private readonly string _url = "https://avitela.lt/kompiuterine-technika/nesiojamieji-kompiuteriai/nesiojami-kompiuteriai?page=1";
        private Lazy<ChromeDriver> _driver;

        public AvitelaParser()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }

        //parses laptops from avitela.lt and returns results in a List<Computer>
        //this method parses first 5 pages (18 laptops in every page), because later pages are outdated 
        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();
            List<string> links = new List<string>();
            string nextPage = _url;

            for (int i = 1; i <= 5; i++)
            {
                nextPage = _url.Remove(_url.Length - 1, 1) + i;
                _driver.Value.Navigate().GoToUrl(nextPage);

                var names = _driver.Value.FindElements(By.CssSelector("div.right > div.name > a"));

                foreach (var name in names)
                {
                    var elem = name.GetAttribute("onclick");    
                    var stringStart =elem.Substring(elem.IndexOf("http"));

                    links.Add(stringStart.Substring(0, stringStart.IndexOf("\'")));
                }

                foreach (var link in links)
                {
                    ((IJavaScriptExecutor)_driver.Value).ExecuteScript("window.open();");
                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.Last());

                    var computer = ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    if (computer.Resolution != null)
                    {
                        computer.ItemCategory = ItemCategory.Computer;
                        data.Add(computer);
                    }
                }
                
            }
            ResetDriver();
            return data;
        }


        //parses laptop window, updates computer fields
        public Computer ParseWindow(string url)
        {

            Computer computer = new Computer();
            computer.Name = _driver.Value.FindElement(By.Id("pname")).Text;
            computer.Price = _driver.Value.FindElement(By.Id("price-old")).Text.ParseDouble();
            computer.ItemURL = url;
            computer.ShopName = "Avitela";

            try
            {
                computer.ImageLink = _driver.Value.FindElement(By.CssSelector("div.product-image-right.product-photo")).
                    FindElement(By.CssSelector("a")).GetAttribute("href");
            }
            catch (Exception)
            {
                computer.ImageLink = "https://ksd-images.lt/display/aikido/store/1e3628060337b388dd4ffbce4f20f608.jpg?h=742&w=816";
            }

            var table = _driver.Value.FindElements(By.TagName("td"));

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
                    computer.Processor = new ProcessorDataService().GetProcessor(table[i + 1].Text);
                }

                else if (computer.GraphicsCardName == null && table[i].Text.Contains("Vaizdo plokštės tipas"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }

                else if (computer.Processor.Name == null && table[i].Text.Contains("Procesoriaus modelis"))
                {
                    computer.Processor = new ProcessorDataService().GetProcessor(table[i + 1].Text);
                }

            }
            ResetDriver();
            return computer;
        }

        private void ResetDriver()
        {
            _driver.Value.Close();
            if (_driver.Value == null)
            {
                var options = new ChromeOptions();
                options.AddArguments("--headless");
                _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
            }
        }

    }
}

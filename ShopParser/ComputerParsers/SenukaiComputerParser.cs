using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using Parsing;
using static ItemLibrary.Categories;
using PathLibrary;
using DataContent.ReadingDB.Services;

namespace WebParser.ComputerParsers
{
    public class SenukaiComputerParser : IParser<Computer>
    {
        private readonly string _url = "https://www.senukai.lt/c/kompiuterine-technika-biuro-prekes/nesiojami-kompiuteriai-ir-priedai/nesiojami-kompiuteriai/5ei?page=1";
        private Lazy<ChromeDriver> _driver;   

        public SenukaiComputerParser()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }

        //parses laptops from senukai.lt and returns results in a List<Computer>
        //this method parses first 5 pages (48laptops in every page), because later pages are
        //outdated and don't have items in stock for a long time
        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();
            List<string> links = new List<string>();

            for (int i = 1; i <= 5; i++)
            {
                var nextPage = _url.Remove(_url.Length - 1, 1) + i;

                _driver.Value.Navigate().GoToUrl(nextPage);

                var elements = _driver.Value.FindElements(By.XPath("//*[@class = 'catalog-taxons-product__name']"));
                foreach (var element in elements)
                {
                    links.Add(element.GetAttribute("href"));
                }

                foreach (var link in links)
                {
                    ((IJavaScriptExecutor)_driver.Value).ExecuteScript("window.open();");
                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.Last());

                    var computer = ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    computer.ItemCategory = ItemCategory.Computer;
                    computer.ComputerCategory = ComputerCategory.Laptop;
                    data.Add(computer);
                }
            }
            ResetDriver();
            return data;
        }


        //parses laptop window, updates computer fields 
        public Computer ParseWindow(string url)
        {
            _driver.Value.Navigate().GoToUrl(url);

            Computer computer = new Computer();

            computer.Name = _driver.Value.FindElement(By.TagName("h1")).Text;
            computer.Price = _driver.Value.FindElement(By.XPath("//span[@class = 'price']")).Text.ParseDouble();
            computer.ItemURL = url;
            computer.ShopName = "Senukai.lt";

            var image = _driver.Value.FindElements(By.ClassName("product-gallery-slider__slide__image"));

            try
            {
                computer.ImageLink = image[0].GetAttribute("src");
            }

            catch (ArgumentOutOfRangeException)
            {
                computer.ImageLink = "https://ksd-images.lt/display/aikido/store/1e3628060337b388dd4ffbce4f20f608.jpg?h=742&w=816";
            }

            var table = _driver.Value.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Prekės ženklas"))
                {
                    computer.ManufacturerName = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Ekrano raiška taškais"))
                {
                    computer.Resolution = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Procesoriaus modelis"))
                {
                    if (table[i + 1].Text.Contains("("))
                    {
                        computer.Processor = new ProcessorDataService().GetProcessor(table[i + 1].Text.Substring(0, table[i + 1].Text.IndexOf("(")));
                    }
                    else
                    {
                        computer.Processor = new ProcessorDataService().GetProcessor(table[i + 1].Text );
                    }
                }

                else if (table[i].Text.Contains("Operatyvioji atmintis (RAM)"))
                {

                    computer.RAM = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Operatyviosios atminties tipas"))
                {
                    computer.RAM_type = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Vaizdo plokštės"))
                {
                    if (table[i].Text.Contains("modelis"))
                    {
                        computer.GraphicsCardName = table[i + 1].Text;
                    }
                    else if (table[i].Text.Contains("serija") && computer.GraphicsCardName == null)
                    {
                        computer.GraphicsCardName = table[i + 1].Text;
                    }

                    else if (table[i].Text.Contains("atmintis"))
                    {
                        computer.GraphicsCardMemory = table[i + 1].Text;
                    }
                }
                else if (table[i].Text.Contains("MMC disko talpa"))
                {
                    computer.StorageCapacity += table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Kietojo disko talpa(HDD)"))
                {

                        computer.StorageCapacity += table[i + 1].Text.ParseInt();
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




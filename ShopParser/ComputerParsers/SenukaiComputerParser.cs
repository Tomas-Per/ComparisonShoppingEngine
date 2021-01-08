using ModelLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using Parsing;
using static ModelLibrary.Categories;
using PathLibrary;
using System.Threading.Tasks;
using System.Net.Http;
using DataContent.DAL.Access;

namespace WebParser.ComputerParsers
{
    public class SenukaiComputerParser : IParser<Computer>
    {
        private string _url;
        private Lazy<ChromeDriver> _driver;
        private ProcessorAccess _processorAccess;
        
        public SenukaiComputerParser(ItemCategory itemCategory)
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
            _processorAccess = new ProcessorAccess();

            switch(itemCategory)
            {
                case ItemCategory.DesktopComputer:
                    _url = "https://www.senukai.lt/c/kompiuterine-technika-biuro-prekes/stacionarus-kompiuteriai-monitoriai-ups/stacionarus-kompiuteriai/c07?page=1";
                    break;
                case ItemCategory.Laptop:
                    _url = "https://www.senukai.lt/c/kompiuterine-technika-biuro-prekes/nesiojami-kompiuteriai-ir-priedai/nesiojami-kompiuteriai/5ei?page=1";
                    break;
                default:
                    _url = "https://www.senukai.lt/c/kompiuterine-technika-biuro-prekes/nesiojami-kompiuteriai-ir-priedai/nesiojami-kompiuteriai/5ei?page=1";
                    break;
            }
        }

        //parses laptops from senukai.lt and returns results in a List<Computer>
        public async Task<List<Computer>> ParseShop()
        {
            List<Computer> data = new List<Computer>();
            List<string> links = new List<string>();

            for (int i = 1; i <= 3; i++)
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

                    var computer = await ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    if (computer.Price == 0)
                    {
                        continue;
                    }
                    data.Add(computer);
                }
            }
            ResetDriver();
            return data;
        }


        //parses laptop window, updates computer fields 
        public async Task<Computer> ParseWindow(string url)
        {
            if(url == null)
            {
                return null;
            }

            _driver.Value.Navigate().GoToUrl(url);

            Computer computer = new Computer();

            computer.Name = _driver.Value.FindElement(By.TagName("h1")).Text;
            try
            {
                computer.Price = _driver.Value.FindElement(By.XPath("//span[@class = 'price']")).Text.ParseDouble();
            }
            catch(Exception)
            {
                computer.Price = 0;
            }
            
            computer.ItemURL = url;
            computer.ShopName = "Senukai.lt";

            var table = _driver.Value.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Prekės ženklas"))
                {
                    computer.ManufacturerName = table[i + 1].Text;
                }
                else if (table[i].Text.Equals("Stacionarūs kompiuteriai"))
                {
                    computer.ItemCategory = ItemCategory.DesktopComputer;
                }
                else if (table[i].Text.Equals("Nešiojami kompiuteriai"))
                {
                    computer.ItemCategory = ItemCategory.Laptop;
                }
                else if (table[i].Text.Contains("Ekrano raiška taškais"))
                {
                    computer.Resolution = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Procesoriaus modelis"))
                {
                    if (table[i + 1].Text.Contains("("))
                    {
                        computer.Processor = await _processorAccess.GetByModelAsync(table[i + 1].Text.Substring(0, table[i + 1].Text.IndexOf("(")));
                    }
                    else
                    {
                        computer.Processor = await _processorAccess.GetByModelAsync(table[i + 1].Text);
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

                else if (table[i].Text.Contains("Vaizdo plokštės modelis"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }           

                else if (table[i].Text.Contains("atmintis"))
                {
                    computer.GraphicsCardMemory = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Kietojo disko talpa (HDD)") ||
                    table[i].Text.Contains("MMC disko talpa"))
                {

                    if (table[i + 1].Text.Contains("TB"))
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt() * 1024;
                    }
                    else
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt();
                    }
                }
                else if(table[i].Text.Contains("SSD disko talpa"))
                {
                    if (table[i + 1].Text.Contains("TB"))
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt() * 1024;
                    }
                    else
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt();
                    }
                }
            }

            var image = _driver.Value.FindElements(By.ClassName("product-gallery-slider__slide__image"));
            try
            {
                computer.ImageLink = image[0].GetAttribute("src");
            }

            catch (ArgumentOutOfRangeException)
            {
                if (computer.ItemCategory == ItemCategory.Laptop)
                {
                    computer.ImageLink = "https://ksd-images.lt/display/aikido/store/1e3628060337b388dd4ffbce4f20f608.jpg?h=742&w=816";
                }
                else
                {
                    computer.ImageLink = "https://ksd-images.lt/display/aikido/store/5b7e344dc88ac39e324764a6cdb9dabb.jpg?h=742&w=816";
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




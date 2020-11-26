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
using System.Threading.Tasks;

namespace WebParser.ComputerParsers
{
    public class AvitelaComputerParser : IParser<Computer>
    {
        private readonly string _url = "https://avitela.lt/kompiuterine-technika/nesiojamieji-kompiuteriai/nesiojami-kompiuteriai?limit=50&page=1";
        private Lazy<ChromeDriver> _driver;

        public AvitelaComputerParser()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();
            List<string> links = new List<string>();
            string nextPage = _url;

            for (int i = 1; i <= 3; i++)
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

                    if (computer == null) continue;

                    if (computer.Resolution != null)
                    {
                        computer.ItemCategory = ItemCategory.Laptop;
                        data.Add(computer);
                    }
                }
                break;
                
            }
            _driver.Value.Close();
            //ResetDriver();
            return data;
        }


        //parses laptop window, updates computer fields
        public Computer ParseWindow(string url)
        {
            _driver.Value.Navigate().GoToUrl(url);

            Computer computer = new Computer();
            try
            {
                computer.Name = _driver.Value.FindElement(By.CssSelector("#pname")).Text;
                computer.Price = _driver.Value.FindElement(By.Id("price-old")).Text.ParseDouble();
            }
            catch(Exception)
            {
                return null;
            }
            computer.ItemURL = url;
            computer.ShopName = "Avitela.lt";

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
                    
                    if (table[i + 1].Text.Contains("TB"))
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt() * 1024;
                    }
                    else
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt();
                    }
                }

                else if (table[i].Text.Contains("Ekrano raiška"))
                {
                    if (table[i + 1].Text.Contains("("))
                    {
                        computer.Resolution = table[i + 1].Text.Substring(0, table[i + 1].Text.IndexOf("("));
                    }
                    else
                    {
                        computer.Resolution = table[i + 1].Text;
                    }
                }
                else if (table[i].Text.Contains("Procesoriaus tipas"))
                {
                    computer.Processor = new ProcessorDataService().GetProcessor(table[i + 1].Text);
                }

                else if (computer.GraphicsCardName == null && table[i].Text.Contains("Vaizdo plokštės tipas"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Procesoriaus modelis"))
                {
                    computer.Processor = new ProcessorDataService().GetProcessor(table[i + 1].Text);
                }

            }
            //ResetDriver();
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

using DataContent.ReadingDB.Services;
using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Parsing;
using PathLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using static ItemLibrary.Categories;

namespace WebParser.ShopParser
{
    public class PiguParser : IParser<Computer>
    {
        private readonly string _url = "https://pigu.lt/lt/kompiuteriai/nesiojami-kompiuteriai?page=1";
        private Lazy<ChromeDriver> _driver;

        public PiguParser ()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }

        //parses laptops from avitela.lt and returns results in a List<Computer>
        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();
            List<String> links = new List<String>();

            for (int i = 1; i <= 3; i++)
            {

                var nextPage = _url.Remove(_url.Length - 1, 1) + i;

                _driver.Value.Navigate().GoToUrl(nextPage);

                var elements = _driver.Value.FindElements(By.ClassName("cover-link"));

                foreach (var item in elements)
                {
                    links.Add(item.GetAttribute("href"));
                }

                foreach (var link in links)
                {
                    ((IJavaScriptExecutor)_driver.Value).ExecuteScript("window.open();");
                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.Last());

                    var computer = ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    computer.ItemCategory = ItemCategory.Computer;

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
            computer.Price = _driver.Value.FindElement(By.XPath("//meta[@itemprop='price']")).GetAttribute("content").ParseDouble();
            computer.ImageLink = _driver.Value.FindElement(By.ClassName("media-items-wrap")).FindElement(By.TagName("img")).GetAttribute("src");
            computer.ItemURL = url;
            computer.ShopName = "Pigu";

            var table = _driver.Value.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Procesorius"))
                {
                    computer.Processor = new ProcessorDataService().GetProcessor(table[i + 1].Text);
                }

                else if (table[i].Text.Contains("Atminties dydis (RAM)"))
                {
                    computer.RAM = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("raiška"))
                {
                    computer.Resolution = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Atminties tipas"))
                {
                    computer.RAM_type = table[i + 1].Text;
                }

                else if (table[i].Text.Equals("Vaizdo plokštė"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Kietasis diskas SSD") || table[i].Text.Contains("Kietasis diskas HDD"))
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

using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using static DataContent.Parsing;
using static ItemLibrary.Categories;

namespace ShopParser
{
    public class SkytechParser : IParser<Computer>
    {

        private readonly string _url = "http://www.skytech.lt/nesiojami-kompiuteriai-nesiojami-kompiuteriai-c-86_165_81.html?sand=1&sort=5a&grp=1&pagesize=500";
        private IWebDriver _driver;
        private readonly string _specificationsTab = "#tabsnav-1";

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();

            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver();         

            _driver.Navigate().GoToUrl(_url);

            var elements = _driver.FindElements(By.ClassName("name"));
            List<String> links = new List<String>();

            foreach (var elem in elements)
            {
                links.Add(elem.FindElement(By.TagName("a")).GetAttribute("href") + _specificationsTab);

            }
            int i = 0;
            foreach (var link in links)
            {
                Console.WriteLine(i++);
                _driver.Navigate().GoToUrl(link);

                Computer computer = new Computer { ItemURL = link, ShopName = "Skytech", ItemCategory = ItemCategory.Computer, ComputerCategory = ComputerCategory.Laptop};
                data.Add(ParseWindow(computer));

                _driver.Navigate().Back();
            }
            _driver.Close();
            return data;
        }

        public Computer ParseWindow(Computer computer)
        {
            string defaultImageLink = "http://www.skytech.lt/images/large/13/2563613.png";

            try
            {
                computer.Name = _driver.FindElement(By.CssSelector("div.product-name.full")).GetAttribute("innerText");
            }
            catch (Exception)
            {
                computer.Name = _driver.FindElement(By.CssSelector("div.product-name.ellipsis.multiline")).Text;
            }
            computer.Price = ParseDouble(_driver.FindElement(By.ClassName("num")).Text);

            try
            {
                computer.ImageLink = _driver.FindElement(By.Id("large-product-image")).GetAttribute("href");
            }
            catch (Exception)
            {
                computer.ImageLink = defaultImageLink;
            }

            ReadOnlyCollection<IWebElement> table;

            try
            {
                table = _driver.FindElements(By.TagName("td"));
            }
            catch (Exception)
            {
                return null;
            }

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Resolution") || (table[i].Text.Contains("Ekranas")))
                {
                    computer.Resolution = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("CPU Family name") || (table[i].Text.Contains("Procesorius")))
                {
                    computer.ProcessorName = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("SSD Capacity") || table[i].Text.Contains("Bendroji laikmenos talpa"))
                {
                    computer.StorageCapacity = ParseInt(table[i + 1].Text);
                }
                else if (table[i].Text.Contains("RAM Type"))
                {
                    computer.RAM_type = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Memory (RAM)"))
                {
                    computer.RAM = ParseInt(table[i + 1].Text);
                }
                else if (table[i].Text.Contains("Graphics memory"))
                {
                    computer.GraphicsCardMemory = table[i + 1].Text;
                }
            }
            return computer;
        }
    }
}

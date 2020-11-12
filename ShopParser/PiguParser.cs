using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Parsing;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using static ItemLibrary.Categories;

namespace ShopParser
{
    public class PiguParser : IParser<Computer>
    {
        private readonly string _url = "https://pigu.lt/lt/kompiuteriai/nesiojami-kompiuteriai?page=1";
        private IWebDriver _driver;

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();
            List<String> links = new List<String>();

            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver();

            for (int i = 1; i <= 3; i++)

            _driver.Navigate().GoToUrl(_url);

            var elements = _driver.FindElements(By.ClassName("cover-link"));

            foreach (var item in elements)
            {
                links.Add(item.GetAttribute("href"));
            }

            foreach (var link in links)
            {
                _driver.Navigate().GoToUrl(link);

                Computer computer = new Computer { ItemURL = link, ShopName = "Pigu", ItemCategory = ItemCategory.Computer, ComputerCategory = ComputerCategory.Laptop };
                data.Add(ParseWindow(computer));

                _driver.Navigate().Back();
            }


            return data;
        }

        public Computer ParseWindow(Computer computer)
        {
            computer.Name = _driver.FindElement(By.TagName("h1")).Text;
            computer.Price = _driver.FindElement(By.XPath("//meta[@itemprop='price']")).GetAttribute("content").ParseDouble();
            computer.ImageLink = _driver.FindElement(By.ClassName("media-items-wrap")).FindElement(By.TagName("img")).GetAttribute("src");


            var table = _driver.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Procesoriaus tipas"))
                {
                    computer.ProcessorName = table[i + 1].Text;
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
                    //this line of code will work when we will update ParseInt
                    //computer.StorageCapacity += table[i + 1].Text.ParseInt(); 
                }

            }

            







            return computer;
        }
    }
}

using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using static DataContent.Parsing;
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

            Console.WriteLine(_driver.FindElement(By.XPath("//meta[@itemprop='price']")).GetAttribute("content"));


            return computer;
        }
    }
}

using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using static ItemLibrary.Categories;

namespace ShopParser
{
    public class PiguParser : IParser<Computer>
    {
        private readonly string _url = "https://pigu.lt/lt/kompiuteriai/nesiojami-kompiuteriai";
        private IWebDriver _driver;

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();

            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver();

            var elements = _driver.FindElements(By.CssSelector("#productBlock28833675 > div"));

            Console.WriteLine(elements.Count);

            foreach (var item in elements)
            {
                Console.WriteLine(item.Text);
            }

            _driver.Navigate().GoToUrl(_url);

            return data;
        }

        public Computer ParseWindow(Computer item)
        {
            throw new NotImplementedException();
        }
    }
}

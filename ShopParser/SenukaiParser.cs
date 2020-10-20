using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopParser
{
    class SenukaiParser : IParser<Computer>
    {
        private IWebDriver _driver;

        public SenukaiParser ()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver();
            //_driver = new ChromeDriver(options);S
        }
        public List<Computer> ParseShop(string url)
        {
            List<Computer> data = new List<Computer>();
            _driver.Navigate().GoToUrl(url);
            //var price = _driver.FindElement(By.Id("catalog-taxons-product-price__item-price"));


            return data;
        }
    }
}

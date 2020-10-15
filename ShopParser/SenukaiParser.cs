using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopParser
{
    class SenukaiParser : IParser
    {
        private IWebDriver _driver;

        public SenukaiParser ()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver();
            //_driver = new ChromeDriver(options);S
        }
        public List<Item> ParseShop(string url)
        {
            List<Item> data = new List<Item>();
            _driver.Navigate().GoToUrl(url);
            //var price = _driver.FindElement(By.Id("catalog-taxons-product-price__item-price"));


            return data;
        }
    }
}

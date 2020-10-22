using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using static DataContent.Parsing;


namespace ShopParser
{
    class SenukaiParser : IParser<Computer>
    {
        private string _url = "https://www.senukai.lt/c/kompiuterine-technika-biuro-prekes/nesiojami-kompiuteriai-ir-priedai/nesiojami-kompiuteriai/5ei";
        private IWebDriver _driver;


        public SenukaiParser ()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new ChromeDriver(options);
        }

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();

            _driver.Navigate().GoToUrl(_url);

            // while exists next button
            var names = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product__name']"));
            var prices = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product-price__item-price']"));

            List<string> pricesList = new List<string>();

            foreach (var price in prices)
            {
                pricesList.Add(price.Text);
            }

            foreach (var name in names) 
            {
                data.Add(new Computer(name.Text, ParseDouble(pricesList.ElementAt(0))));
                pricesList.RemoveAt(0);
            }

            return data;
        }

            
    }
}


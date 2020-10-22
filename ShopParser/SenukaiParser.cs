using ItemLibrary;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
            _driver = new ChromeDriver();
        }


        public static void Main (string[] args)
        {
            var test = new SenukaiParser();
            var tet = test.ParseShop();
        }

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();

            _driver.Navigate().GoToUrl(_url);

            // while exists next button
            var names = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product__name']"));
            var prices = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product-price__item-price']"));

            var pricesList = prices.ToList();

            foreach (var name in names) 
            {
                data.Add(new Computer(name.Text, ParseDouble(pricesList.First)));
                pricesList.RemoveAt(0);
            }

            return data;
        }

            
    }
}


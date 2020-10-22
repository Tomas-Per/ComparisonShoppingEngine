using HtmlAgilityPack;
using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

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

            return data;
        }

            
    }
}


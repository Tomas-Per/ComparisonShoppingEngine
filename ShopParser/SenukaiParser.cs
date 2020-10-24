using ItemLibrary;
using Newtonsoft.Json.Schema;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            _driver = new ChromeDriver();
        }


        public static void Main (string[] args)
        {
            var test = new SenukaiParser();
            List<Computer> testt = test.ParseShop();

            foreach (var item in testt)
            {
                Console.WriteLine(item.Price);
            }


        }

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();

            _driver.Navigate().GoToUrl(_url);

            // while exists next button
            var elements = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product__name']"));

            List<string> links = new List<string>();

            foreach (var element in elements)
            {
                links.Add(element.GetAttribute("href"));
            }

            foreach (var link in links)
            {
                _driver.Navigate().GoToUrl(link);
                var test = parseWindow();
                _driver.Navigate().Back();
                break;
                //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            }

            return data;
        }


        private Computer parseWindow ()
        {

            var name = _driver.FindElement(By.CssSelector("//div[contains ((@class, 'product-righter') and (@class 'google-rich-snippet'))]"));
            var price = _driver.FindElement(By.ClassName("price"));
            Console.WriteLine(name.Text);

            return null;
        }


            
    }
}


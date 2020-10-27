using ItemLibrary;
using Newtonsoft.Json.Schema;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
            var names = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product__name']"));
            var prices = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product-price__item-price']"));
            string link;
            List<string> pricesList = new List<string>();


            foreach (var price in prices)
            {
                pricesList.Add(price.Text);
            }



            foreach (var element in names)
            {
                Computer computer = new Computer { Name = element.Text, Price = ParseDouble(pricesList.ElementAt(0)) };
                pricesList.RemoveAt(0);

                link = element.GetAttribute("href");
                computer.ItemURL = link;

                _driver.Navigate().GoToUrl(link);

                var test = parseWindow(computer);
                _driver.Navigate().Back();

                break;
                //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            }

            return data;
        }


        private Computer parseWindow (Computer computer)
        {
            
            //var id = _driver.FindElement(By.ClassName("product-id"));

            var generalProperties = _driver.FindElements(By.ClassName("attribute-filter-link"));

            computer.ManufacturerName = generalProperties.ElementAt(1).Text;
            computer.ProcessorName = generalProperties.ElementAt(8).Text + generalProperties.ElementAt(9).Text;
            computer.GraphicsCardName = generalProperties.ElementAt(14).Text;
            computer.GraphicsCardMemory = generalProperties.ElementAt(15).Text;
            computer.RAM_type = generalProperties.ElementAt(11).Text;


            Console.WriteLine(generalProperties.ElementAt(11).Text);




            Console.WriteLine("-----------------------");


            return null;
        }

        

    }
}


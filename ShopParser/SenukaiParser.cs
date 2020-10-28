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
        private string _url = "https://www.senukai.lt/c/kompiuterine-technika-biuro-prekes/nesiojami-kompiuteriai-ir-priedai/nesiojami-kompiuteriai/5ei?";
        private IWebDriver _driver;
        private string currentWIndowURL;

        public SenukaiParser ()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new ChromeDriver(options);
            currentWIndowURL = _url;
        }

        public List<Computer> ParseShop()
        {
            List<Computer> data = new List<Computer>();

            _driver.Navigate().GoToUrl(_url);

            for (int i = 0; i < 10; i++)
            {
                var names = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product__name']"));
                var prices = _driver.FindElements(By.XPath("//*[@class = 'catalog-taxons-product-price__item-price']"));

                List<string> links = new List<string>();
                List<string> pricesList = new List<string>();
                List<string> namesList = new List<string>();

                foreach (var price in prices)
                {
                    pricesList.Add(price.Text);
                }

                foreach (var element in names)
                {
                    links.Add(element.GetAttribute("href"));
                    namesList.Add(element.Text);
                }

                foreach (var link in links)
                {
                    Computer computer = new Computer { Name = namesList.ElementAt(0), Price = ParseDouble(pricesList.ElementAt(0)) };

                    namesList.RemoveAt(0);
                    pricesList.RemoveAt(0);

                    computer.ItemURL = link;

                    _driver.Navigate().GoToUrl(link);

                    data.Add(ParseWindow(computer));
                    _driver.Navigate().Back();

                    //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                }

                var nextPage = _driver.FindElement(By.ClassName("next")).GetAttribute("href");

                if (nextPage != currentWIndowURL)
                {
                    _driver.Navigate().GoToUrl(nextPage);
                    currentWIndowURL = nextPage;
                }
            }
            return data;
        }


        private Computer ParseWindow(Computer computer)
        {

            //var id = _driver.FindElement(By.ClassName("product-id"));

            var generalProperties = _driver.FindElements(By.TagName("td"));


            for (int i = 0; i < generalProperties.Count; i++)
            {
                if (generalProperties[i].Text.Contains("Prekės ženklas"))
                {
                    computer.ManufacturerName = generalProperties[i + 1].Text;
                }

                else if (generalProperties[i].Text.Contains("Ekrano raiška taškais"))
                {
                    computer.Resolution = generalProperties[i + 1].Text;
                }

                else if (generalProperties[i].Text.Contains("Procesoriaus klasė"))
                {
                    computer.ProcessorName = generalProperties[i + 1].Text;
                }

                else if (generalProperties[i].Text.Contains("Operatyviosios atmint"))
                {
                    if (generalProperties[i].Text.Contains("tipas"))
                    {
                        computer.RAM_type = generalProperties[i + 1].Text;
                    }

                    else if (generalProperties[i].Text.Contains("(RAM)"))
                    {
                        computer.RAM = ParseInt(generalProperties[i + 1].Text);
                    }  
                }
                else if (generalProperties[i].Text.Contains("Vaizdo plokštės"))
                {
                    if (generalProperties[i].Text.Contains("modelis"))
                    {
                        computer.GraphicsCardName = generalProperties[i + 1].Text;
                    }
                    else if (generalProperties[i].Text.Contains("serija") && computer.GraphicsCardName == null)
                    {
                        computer.GraphicsCardName = generalProperties[i + 1].Text;
                    }

                    else if (generalProperties[i].Text.Contains("atmintis"))
                    {
                        computer.GraphicsCardMemory = generalProperties[i + 1].Text;
                    }
                }
            }
        return computer;
        }

        

    }
}


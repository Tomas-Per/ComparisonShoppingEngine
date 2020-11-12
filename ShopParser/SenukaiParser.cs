using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using static DataContent.Parsing;
using static ItemLibrary.Categories;

namespace ShopParser
{
    public class SenukaiParser : IParser<Computer>
    {
        private readonly string _url = "https://www.senukai.lt/c/kompiuterine-technika-biuro-prekes/nesiojami-kompiuteriai-ir-priedai/nesiojami-kompiuteriai/5ei?";
        private IWebDriver _driver;
        private string _currentWIndowURL;

        public SenukaiParser()
        {
        }

        //parses laptops from senukai.lt and returns results in a List<Computer>
        //this method parses first 10 pages (48laptops in every page), because later pages are
        //outdated and don't have items in stock for a long time
        public List<Computer> ParseShop()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver(options);
            _currentWIndowURL = _url;

            List<Computer> data = new List<Computer>();

            string nextPage;

            _driver.Navigate().GoToUrl(_url);

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    nextPage = _driver.FindElement(By.XPath("//a[contains(@class, 'next')]")).GetAttribute("href");
                }

                catch (Exception)
                {
                    nextPage = _currentWIndowURL;
                }

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
                    Computer computer = new Computer { Name = namesList.ElementAt(0), Price = ParseDouble(pricesList.ElementAt(0)), ItemCategory = ItemCategory.Computer, ComputerCategory = ComputerCategory.Laptop, ShopName = "Senukai" };

                    namesList.RemoveAt(0);
                    pricesList.RemoveAt(0);

                    computer.ItemURL = link;

                    _driver.Navigate().GoToUrl(link);

                    data.Add(ParseWindow(computer));
                    _driver.Navigate().Back();

                    //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                }

                if (nextPage != _currentWIndowURL)
                {
                    _driver.Navigate().GoToUrl(nextPage);
                    _currentWIndowURL = nextPage;
                }
            }

            _driver.Close();
            return data;
        }


        //parses laptop window, updates computer fields 
        public Computer ParseWindow(Computer computer)
        {

            //var id = _driver.FindElement(By.ClassName("product-id"));

            var image = _driver.FindElements(By.ClassName("product-gallery-slider__slide__image"));

            try
            {
                computer.ImageLink = image[0].GetAttribute("src");
            }

            catch (ArgumentOutOfRangeException)
            {
                computer.ImageLink = "https://ksd-images.lt/display/aikido/store/1e3628060337b388dd4ffbce4f20f608.jpg?h=742&w=816";
            }

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

                else if (generalProperties[i].Text.Contains("Operatyvioji atmintis (RAM)"))
                {

                    computer.RAM = ParseInt(generalProperties[i + 1].Text);
                }

                else if (generalProperties[i].Text.Contains("Operatyviosios atminties tipas"))
                {
                    computer.RAM_type = generalProperties[i + 1].Text;
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
                else if (generalProperties[i].Text.Contains("MMC disko talpa"))
                {
                    computer.StorageCapacity += ParseInt(generalProperties[i + 1].Text);
                }

                else if (generalProperties[i].Text.Contains("Kietojo disko talpa(HDD)"))
                {
                    try
                    {
                        computer.StorageCapacity += ParseInt(generalProperties[i + 1].Text);
                    }

                    catch (Exception)
                    {

                    }
                }
                
            }

            return computer;
        }
    }
}




using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using Parsing;
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
        //this method parses first 5 pages (48laptops in every page), because later pages are
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

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    nextPage = _driver.FindElement(By.XPath("//a[contains(@class, 'next')]")).GetAttribute("href");
                }

                catch (Exception)
                {
                    nextPage = _currentWIndowURL;
                }

                List<string> links = new List<string>();

                foreach (var link in links)
                {
                    var computer = ParseWindow(link);

                    computer.ItemCategory = ItemCategory.Computer;
                    computer.ComputerCategory = ComputerCategory.Laptop;
                    data.Add(computer);

                    //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                }

                if (nextPage != _currentWIndowURL)
                {
                    _driver.Navigate().GoToUrl(nextPage);
                    _currentWIndowURL = nextPage;
                }
                else
                {
                    break;
                }
            }
            _driver.Close();
            return data;
        }


        //parses laptop window, updates computer fields 
        public Computer ParseWindow(string url)
        {
            _driver.Navigate().GoToUrl(url);

            Computer computer = new Computer();

            computer.Name = _driver.FindElement(By.TagName("h1")).Text;
            computer.Price = _driver.FindElement(By.XPath("//span[@class = 'price']")).Text.ParseDouble();
            computer.ItemURL = url;
            computer.ShopName = "Senukai.lt";

            var image = _driver.FindElements(By.ClassName("product-gallery-slider__slide__image"));

            try
            {
                computer.ImageLink = image[0].GetAttribute("src");
            }

            catch (ArgumentOutOfRangeException)
            {
                computer.ImageLink = "https://ksd-images.lt/display/aikido/store/1e3628060337b388dd4ffbce4f20f608.jpg?h=742&w=816";
            }

            var table = _driver.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Prekės ženklas"))
                {
                    computer.ManufacturerName = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Ekrano raiška taškais"))
                {
                    computer.Resolution = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Procesoriaus klasė"))
                {
                    computer.Processor = new Processor { Name = table[i + 1].Text };
                }

                else if (table[i].Text.Contains("Operatyvioji atmintis (RAM)"))
                {

                    computer.RAM = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Operatyviosios atminties tipas"))
                {
                    computer.RAM_type = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Vaizdo plokštės"))
                {
                    if (table[i].Text.Contains("modelis"))
                    {
                        computer.GraphicsCardName = table[i + 1].Text;
                    }
                    else if (table[i].Text.Contains("serija") && computer.GraphicsCardName == null)
                    {
                        computer.GraphicsCardName = table[i + 1].Text;
                    }

                    else if (table[i].Text.Contains("atmintis"))
                    {
                        computer.GraphicsCardMemory = table[i + 1].Text;
                    }
                }
                else if (table[i].Text.Contains("MMC disko talpa"))
                {
                    computer.StorageCapacity += table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Kietojo disko talpa(HDD)"))
                {
                    try
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt();
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




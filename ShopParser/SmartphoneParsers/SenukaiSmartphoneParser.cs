using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using Parsing;
using static ItemLibrary.Categories;
using PathLibrary;
using System.Threading.Tasks;

namespace WebParser.SmartphoneParsers
{
    public class SenukaiSmartphoneParser : IParser<Smartphone>
    {
        private readonly string _url = "https://www.senukai.lt/c/telefonai-plansetiniai-kompiuteriai/mobilieji-telefonai/5nt?page=1";
        private Lazy<ChromeDriver> _driver;

        public SenukaiSmartphoneParser()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }

        //parses smartphones from senukai.lt and returns results in a List<Smartphone>
        public async Task<List<Smartphone>> ParseShop()
        {
            List<Smartphone> data = new List<Smartphone>();
            List<string> links = new List<string>();

            for (int i = 1; i <= 5; i++)
            {
                var nextPage = _url.Remove(_url.Length - 1, 1) + i;

                _driver.Value.Navigate().GoToUrl(nextPage);

                var elements = _driver.Value.FindElements(By.XPath("//*[@class = 'catalog-taxons-product__name']"));
                foreach (var element in elements)
                {
                    links.Add(element.GetAttribute("href"));
                }

                foreach (var link in links)
                {
                    ((IJavaScriptExecutor)_driver.Value).ExecuteScript("window.open();");
                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.Last());

                    _driver.Value.Navigate().GoToUrl(link);
                    Smartphone smartphone = await ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    if (smartphone.Price == 0)
                    {
                        continue;
                    }

                    smartphone.ItemCategory = ItemCategory.Smartphone;
                    data.Add(smartphone);
                }
            }
            ResetDriver();
            return data;
        }


     
        //Parses Smartphone from an url
        public async Task<Smartphone> ParseWindow(string url)
        {
            _driver.Value.Navigate().GoToUrl(url);

            Smartphone smartphone = new Smartphone();

            smartphone.Name = _driver.Value.FindElement(By.TagName("h1")).Text;
            try
            {
                smartphone.Price = _driver.Value.FindElement(By.XPath("//span[@class = 'price']")).Text.ParseDouble();
            }
            catch (Exception)
            {
                smartphone.Price = 0;
            }

            smartphone.ItemURL = url;
            smartphone.ShopName = "Senukai.lt";

            var image = _driver.Value.FindElements(By.ClassName("product-gallery-slider__slide__image"));

            try
            {
                smartphone.ImageLink = image[0].GetAttribute("src");
            }

            catch (ArgumentOutOfRangeException)
            {
                smartphone.ImageLink = "https://ksd-images.lt/display/aikido/store/3bb53f9a34e0ed486f44798e1f417a8d.jpg?h=742&w=816";
            }

            var table = _driver.Value.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Prekės ženklas"))
                {
                    smartphone.ManufacturerName = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Ekrano dydis"))
                {
                    smartphone.ScreenDiagonal = table[i + 1].Text.ParseDouble();
                }

                else if (table[i].Text.Contains("Ekrano raiška"))
                {
                    smartphone.Resolution = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Procesoriaus modelis"))
                {
                    smartphone.Processor = table[i + 1].Text.Substring(0, table[i + 1].Text.IndexOf("("));
                }

                else if (table[i].Text.Contains("Atminties talpa"))
                {
                    smartphone.Storage = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Akumuliatoriaus/baterijos talpa"))
                {
                    smartphone.BatteryStorage = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("Galinė kamera"))
                {
                    var values = table[i + 1].Text.Split(',');
                    List<int> cameras = new List<int>();
                    values.ToList().ForEach(item => cameras.Add(item.ParseInt()));
                    smartphone.BackCameraMP = cameras;

                }
                else if (table[i].Text.Contains("Priekinė kamera"))
                {
                    var values = table[i + 1].Text.Split(',');
                    List<int> cameras = new List<int>();
                    values.ToList().ForEach(item => cameras.Add(item.ParseInt()));
                    smartphone.FrontCameraMP = cameras;
                }
                else if (table[i].Text.Contains("Operatyvioji atmintis (RAM)"))
                {
                    smartphone.RAM = table[i + 1].Text.ParseInt();
                }
            }

            ResetDriver();
            return smartphone;
        }

        private void ResetDriver()
        {
            _driver.Value.Close();
            if (_driver.Value == null)
            {
                var options = new ChromeOptions();
                options.AddArguments("--headless");
                _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
            }
        }

    }
}




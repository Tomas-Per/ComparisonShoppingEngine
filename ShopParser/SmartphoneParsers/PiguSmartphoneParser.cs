﻿using DataContent.ReadingDB.Services;
using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Parsing;
using PathLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ItemLibrary.Categories;

namespace WebParser.SmartphoneParsers
{
    public class PiguSmartphoneParser : IParser<Smartphone>
    {
        private readonly string _url = "https://pigu.lt/lt/foto-gsm-mp3/mobilieji-telefonai?page=1";
        private Lazy<ChromeDriver> _driver;

        public PiguSmartphoneParser()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }

        public async Task<List<Smartphone>> ParseShop()
        {
            List<Smartphone> data = new List<Smartphone>();
            List<string> links = new List<string>();

            for (int i = 1; i <= 5; i++)
            {
                var nextPage = _url.Remove(_url.Length - 1, 1) + i;
                _driver.Value.Navigate().GoToUrl(nextPage);

                var elements = _driver.Value.FindElements(By.ClassName("cover-link"));

                foreach (var item in elements)
                {
                    links.Add(item.GetAttribute("href"));
                }

                foreach (var link in links)
                {
                    ((IJavaScriptExecutor)_driver.Value).ExecuteScript("window.open();");
                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.Last());

                    var smartphone = await ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    smartphone.ItemCategory = ItemCategory.Smartphone;
                    data.Add(smartphone);
                }
            }
            ResetDriver();
            return data;
        }

        //parses smartphone window
        public async Task<Smartphone> ParseWindow(string url)
        {
            _driver.Value.Navigate().GoToUrl(url);

            Smartphone smartphone = new Smartphone();

            smartphone.Name = _driver.Value.FindElement(By.TagName("h1")).Text;
            smartphone.Price = _driver.Value.FindElement(By.XPath("//meta[@itemprop='price']")).GetAttribute("content").ParseDouble();
            smartphone.ImageLink = _driver.Value.FindElement(By.ClassName("media-items-wrap")).FindElement(By.TagName("img")).GetAttribute("src");
            smartphone.ItemURL = url;
            smartphone.ShopName = "Pigu.lt";

            var table = _driver.Value.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if(table[i].Text.Contains("Prekės ženklas"))
                {
                    smartphone.ManufacturerName = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Ekrano dydis(coliais)"))
                {
                    smartphone.ScreenDiagonal = table[i + 1].Text.ParseDouble();
                }
                else if (table[i].Text.Contains("Pagrindinė kamera"))
                {
                    var values = table[i + 1].Text.Split('+');
                    List<int> cameras = new List<int>();
                    values.ToList().ForEach(item => cameras.Add(item.ParseInt()));
                    smartphone.BackCameraMP = cameras;

                    smartphone.BackCameraMP.ForEach(item => Console.WriteLine(item));
                }
                else if (table[i].Text.Contains("Priekinė kamera"))
                {
                    var values = table[i + 1].Text.Split('+');
                    List<int> cameras = new List<int>();
                    values.ToList().ForEach(item => cameras.Add(item.ParseInt()));
                    smartphone.BackCameraMP = cameras;
                }
                else if (table[i].Text.Contains("Procesoriaus tipas"))
                {
                    smartphone.Processor = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Vidinė atmintis"))
                {
                    smartphone.Storage = table[i + 1].Text.ParseInt();
                }
                else if (table[i].Text.Contains("Operatyvinė atmintis (RAM)"))
                {
                    smartphone.RAM = table[i + 1].Text.ParseInt();
                    if (table[i + 1].Text.Contains("MB"))
                    {
                        smartphone.RAM = smartphone.RAM / 1024;
                    }
                }
                else if (table[i].Text.Contains("Baterijos talpa"))
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
﻿using ModelLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using Parsing;
using static ModelLibrary.Categories;
using System.Linq;
using PathLibrary;
using System.Threading.Tasks;

namespace WebParser.SmartphoneParsers
{
    public class AvitelaSmartphoneParser : IParser<Smartphone>
    {
        private readonly string _url = "https://avitela.lt/telefonai-ir-laikrodziai/mobilieji-telefonai?page=PAGE_NUMBER&bfilter=tipas[grafinas]";
        private Lazy<ChromeDriver> _driver;

        public AvitelaSmartphoneParser()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }


        public async Task<List<Smartphone>> ParseShop()
        {
            List<Smartphone> data = new List<Smartphone>();
            List<string> links = new List<string>();
            string nextPage = _url;

            for (int i = 0; i < 3; i++)
            {
                nextPage = _url.Replace("PAGE_NUMBER", i.ToString());
                _driver.Value.Navigate().GoToUrl(nextPage);

                var names = _driver.Value.FindElements(By.CssSelector("div.right > div.name > a"));

                foreach (var name in names)
                {
                    var elem = name.GetAttribute("onclick");
                    var stringStart = elem.Substring(elem.IndexOf("http"));
                    links.Add(stringStart.Substring(0, stringStart.IndexOf("\'")));
                }

                foreach (var link in links)
                {
                    ((IJavaScriptExecutor)_driver.Value).ExecuteScript("window.open();");
                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.Last());

                    _driver.Value.Navigate().GoToUrl(link);

                    Smartphone smartphone = await ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    if (smartphone == null) continue;

                    smartphone.ItemCategory = ItemCategory.Smartphone;

                    data.Add(smartphone);
                }
                //break;
            }
            ResetDriver();
            return data;
        }


        //parses smartphone window, updates smartphone fields
        public async Task<Smartphone> ParseWindow(string url)
        {
            if (url == null)
            {
                return null;
            }

            _driver.Value.Navigate().GoToUrl(url);

            Smartphone smartphone = new Smartphone();

            try
            {
                smartphone.Name = _driver.Value.FindElement(By.Id("pname")).Text;
                smartphone.Price = _driver.Value.FindElement(By.Id("price-old")).Text.ParseDouble();
            }
            catch(Exception)
            {
                return null;
            }
            smartphone.ItemURL = url;
            smartphone.ShopName = "Avitela.lt";

            try
            {
                smartphone.ImageLink = _driver.Value.FindElement(By.CssSelector("div.product-image-right.product-photo")).
                    FindElement(By.CssSelector("a")).GetAttribute("href");
            }
            catch (Exception)
            {
                smartphone.ImageLink = "https://avitela.lt/image/cache/catalog/p/75/76/9767/l_10193704_005-600x600.jpg";
            }

            smartphone.ManufacturerName = _driver.Value.FindElement(By.CssSelector("#main > div.breadcrumb.full-width > div.background > div.pattern > div > div > ul > li:nth-child(4) > a")).Text;



            var table = _driver.Value.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Ekrano įstrižainė"))
                {
                    smartphone.ScreenDiagonal = table[i + 1].Text.ParseDouble();
                }
                else if (table[i].Text.Contains("Priekinė kamera"))
                {
                    smartphone.FrontCameras = table[i + 1].Text;
                }
                else if (table[i].Text.Equals("Kamera"))
                {
                    smartphone.BackCameras = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Vidinė atmintis"))
                {
                    smartphone.Storage = table[i + 1].Text.ParseInt();
                }
                else if (table[i].Text.Contains("Procesoriaus modelis"))
                {

                    if (table[i + 1].Text.Contains("("))
                    {
                        smartphone.Processor = table[i + 1].Text.Substring(0, table[i + 1].Text.IndexOf("("));
                    }
                    else
                    {
                        smartphone.Processor = table[i + 1].Text;
                    }                
                }
                else if (table[i].Text.Contains("Baterijos talpa"))
                {
                    smartphone.BatteryStorage = table[i + 1].Text.ParseInt();
                }
                else if (table[i].Text.Contains("Ekrano raiška"))
                {
                    smartphone.Resolution = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Operatyvioji atmintis"))
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

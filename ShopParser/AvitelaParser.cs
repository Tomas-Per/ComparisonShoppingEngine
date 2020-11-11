using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using static DataContent.Parsing;
using static ItemLibrary.Categories;

namespace ShopParser
{
    public class AvitelaParser : IParser<Computer>
    {
        private readonly string _url = "https://avitela.lt/kompiuterine-technika/nesiojamieji-kompiuteriai/nesiojami-kompiuteriai?page=1";
        private IWebDriver _driver;




        public List<Computer> ParseShop()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");

            _driver = new ChromeDriver();

            List<Computer> data = new List<Computer>();

            List<string> links = new List<string>();

            string nextPage = _url;

            _driver.Navigate().GoToUrl(_url);

            for (int i = 1; i < 8; i++)
            {
                var names = _driver.FindElements(By.CssSelector("div.right > div.name > a"));

                foreach (var name in names)
                {
                    var elem = name.GetAttribute("onclick");    
                    var stringStart =elem.Substring(elem.IndexOf("http"));

                    links.Add(stringStart.Substring(0, stringStart.IndexOf("\'")));
                }

                foreach (var link in links)
                {
                    _driver.Navigate().GoToUrl(link);

                    var test = ParseWindow(new Computer {ItemURL = link, ItemCategory = ItemCategory.Computer, ComputerCategory = ComputerCategory.Laptop, ShopName = "Avitela" });


                    _driver.Navigate().Back();
                }
                nextPage = _url.Remove(_url.Length - 1, 1) + i;
                //_driver.Navigate().GoToUrl(nextPage);
                break;


            }

   
            //_driver.Close();
            return data;
        }


        private Computer ParseWindow(Computer computer)
        {
            computer.Name = _driver.FindElement(By.Id("pname")).Text;
            computer.Price = ParseDouble(_driver.FindElement(By.Id("price-old")).Text);

            computer.ImageLink = _driver.FindElement(By.CssSelector("div.product-image-right.product-photo")).
                FindElement(By.CssSelector("a")).GetAttribute("href");

            var table = _driver.FindElements(By.TagName("td"));

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Equals("Vaizdo plokštė"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Operat. atminties tipas"))
                {
                    computer.RAM_type = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Operatyvioji atmintis"))
                {
                    computer.RAM = ParseInt(table[i + 1].Text);
                }

                else if (table[i].Text.Contains("Vidinė atmintis"))
                {
                    computer.StorageCapacity = ParseInt(table[i + 1].Text);
                }

                else if (table[i].Text.Contains("Ekrano raiška"))
                {
                    computer.Resolution = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Procesoriaus tipas"))
                {
                    computer.ProcessorName = table[i + 1].Text;
                }

            }
            return computer;
        }

    }
}

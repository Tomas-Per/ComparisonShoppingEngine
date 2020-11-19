using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Parsing;
using PathLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.ComponentsParser
{
    public class ProcessorParser
    {
        private readonly string _url = "https://www.gpuskin.com/CPU-Database";
        private Lazy<ChromeDriver> _driver;

        public ProcessorParser()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            //_driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath()));
        }

        public Processor ParseProcessor (string processorType)
        {
            _driver.Value.Navigate().GoToUrl(_url);
            _driver.Value.FindElement(By.Name("SearchRecords")).SendKeys(processorType);
            _driver.Value.FindElement(By.Id("searchButton")).Click();


            //var a = _driver.Value.FindElements(By.TagName("a"));

            var button = _driver.Value.FindElements(By.CssSelector("a.btn.btn-default"));

             var link = button[0].GetAttribute("href");

            _driver.Value.Navigate().GoToUrl(link);

            var table = _driver.Value.FindElements(By.TagName("td"));

            Processor processor = new Processor { Name = processorType };

            for (int i = 0; i < table.Count; i++)
            {
                //Console.WriteLine(table[i].GetAttribute("innerHTML"));
                if (table[i].GetAttribute("innerHTML").Contains("Cache L3"))
                {
                    processor.Cache = table[i + 1].Text.ParseInt();
                    Console.WriteLine(processor.Cache);
                }
                else if (table[i].GetAttribute("innerHTML").Contains("Cores"))
                {
                    processor.MinCores = table[i + 1].Text.ParseInt();
                    Console.WriteLine(processor.MinCores);
                }
            }







            return null;
        }
    }
}

using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));

        }

        [Obsolete]
        public Processor ParseProcessor (string model)
        {
            _driver.Value.Navigate().GoToUrl(_url);
   
            try
            {
                _driver.Value.FindElement(By.Name("SearchRecords")).SendKeys(model);
                //DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(_driver.Value);
                //fluentWait.Timeout = TimeSpan.FromSeconds(30);
                //fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
                //fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                //fluentWait.Until(x => x.FindElement(By.Id("searchButton")));


                WebDriverWait wait = new WebDriverWait(_driver.Value, TimeSpan.FromSeconds(10));
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchButton")));
                element.Click();


                //_driver.Value.FindElement(By.Id("searchButton")).Click();
            }
            catch (Exception)
            {
                throw new ProcessorNotFoundException("Could not parse processor");
            }

            try
            {
                var button = _driver.Value.FindElements(By.CssSelector("a.btn.btn-default"));
                var link = button[0].GetAttribute("href");
                _driver.Value.Navigate().GoToUrl(link);
            }
            catch (Exception)
            {
                throw new ProcessorNotFoundException("Processor could not be parsed");
            }

           
            var table = _driver.Value.FindElements(By.TagName("td"));

            Processor processor = new Processor ();

            var processorModel = _driver.Value.FindElement(By.ClassName("text-center"))
                .GetAttribute("innerText").Split(new[] { '\r', '\n' }).FirstOrDefault();

            processor.Model = processorModel;
            processor.SetName(processorModel);

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].GetAttribute("innerHTML").Contains("Cache L3"))
                {
                    processor.Cache = table[i + 1].GetAttribute("innerHTML").ParseInt();
                }
            }
            processor.MinCores = _driver.Value.FindElement(By.CssSelector("#fh5co-programs-section > div > form > div:nth-child(6) > div > table > tbody > tr:nth-child(2) > td:nth-child(2) > div > div:nth-child(2) > div:nth-child(1)"))
                .GetAttribute("innerHTML").ParseInt();

            _driver.Value.Close();
            //ResetDriver();
            return processor;
        }

        private void ResetDriver()
        {
            _driver.Value.Close();
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using static ItemLibrary.Processor;

namespace URLGenerator
{
    public class AmazonURLGenerator
    {
        private static IWebDriver driver;

        public AmazonURLGenerator()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");

            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.amazon.com/");

            var moreElementsButton = driver.FindElement(By.Id("nav-hamburger-menu"));
            moreElementsButton.Click();

            var computerButton = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/ul[1]/li[7]/a"));
            computerButton.Click();

            var computersAndTabletsButton = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/ul[6]/li[5]/a"));
            computersAndTabletsButton.Click();
        }

        public static string getLaptopURL(ProcessorTypes pt)
        {
            driver.Navigate().GoToUrl("https://www.amazon.com/s?i=computers-intl-ship&bbn=16225007011&rh=n%3A16225007011%2Cn%3A13896617011%2Cn%3A565108&dc&qid=1602170202&rnid=13896617011&ref=sr_nr_n_2");

            switch (pt)
            {
                case ProcessorTypes.IntelCoreI5:
                    {

                        var button = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div[1]/div/div[3]/span/div[1]/span/div/div/div[5]/ul[5]/li[2]/span/a/div/label/i"));
                        button.Click();
                        return driver.Url;
                    }


                case ProcessorTypes.IntelCeleron:
                    {
                        var button = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div[1]/div/div[3]/span/div[1]/span/div/div/div[5]/ul[5]/li[3]/span/a/div/label/i"));
                        button.Click();
                        return driver.Url;
                    }

                case ProcessorTypes.IntelCoreI7:
                    {
                        var button = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div[1]/div/div[3]/span/div[1]/span/div/div/div[5]/ul[5]/li[2]/span/a/div/label/i"));
                        button.Click();
                        return driver.Url;
                    }

                case ProcessorTypes.IntelCoreI3:
                    {
                        var button = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div[1]/div/div[3]/span/div[1]/span/div/div/div[5]/ul[5]/li[5]/span/a/div/label/i"));
                        button.Click();
                        return driver.Url;
                    }

                case ProcessorTypes.AMDASeries:
                    {
                        var button = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div[1]/div/div[3]/span/div[1]/span/div/div/div[5]/ul[5]/li[5]/span/a/div/label/i"));
                        button.Click();
                        return driver.Url;
                    }

                default:
                    {
                        return driver.Url;
                    }

            }
        }

    }
}



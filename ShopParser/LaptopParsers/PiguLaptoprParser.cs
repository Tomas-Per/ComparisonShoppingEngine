using ItemLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Parsing;
using PathLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static ItemLibrary.Categories;

namespace WebParser.LaptopParsers
{
    public class PiguLaptoprParser : IParser<Computer>
    {
        private readonly string _url = "https://pigu.lt/lt/kompiuteriai/nesiojami-kompiuteriai?page=1";
        private Lazy<ChromeDriver> _driver;
        private HttpClient _client;
        public PiguLaptoprParser ()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new Lazy<ChromeDriver>(() => new ChromeDriver(MainPath.GetShopParserPath(), options));
            _client = new HttpClient();
        }

        public async Task<List<Computer>> ParseShop()
        {
            List<Computer> data = new List<Computer>();
            List<string> links = new List<string>();

            for (int i = 1; i <= 3; i++)
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

                    var computer = await ParseWindow(link);

                    _driver.Value.SwitchTo().Window(_driver.Value.WindowHandles.First());

                    if (computer == null) continue;

                    computer.ItemCategory = ItemCategory.Laptop;

                    data.Add(computer);
                }
                //break;
            }
            _driver.Value.Close();
            //ResetDriver();
            return data;
        }

        //parses laptop window, updates computer fields
        public async Task<Computer> ParseWindow(string url)
        {
            _driver.Value.Navigate().GoToUrl(url);

            Computer computer = new Computer();

            try
            {
                computer.Name = _driver.Value.FindElement(By.TagName("h1")).Text;
                computer.Price = _driver.Value.FindElement(By.XPath("//meta[@itemprop='price']")).GetAttribute("content").ParseDouble();
                computer.ImageLink = _driver.Value.FindElement(By.ClassName("media-items-wrap")).FindElement(By.TagName("img")).GetAttribute("src");
            }
            catch(Exception)
            {
                return null;
            }
            computer.ItemURL = url;
            computer.ShopName = "Pigu.lt";

            var table = _driver.Value.FindElements(By.TagName("td"));

            //FOR API CALLS
            var apiUrl = "https://localhost:44315/Models/";
            HttpResponseMessage response;
            //---------------------------------------------

            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Text.Contains("Procesorius"))
                {
                    response = await _client.GetAsync(apiUrl + table[i + 1].Text);
                    if (response.IsSuccessStatusCode)
                    {
                        computer.Processor = await response.Content.ReadAsAsync<Processor>();
                    }
                }
                else if (table[i].Text.Contains("Prekės ženklas"))
                {
                    computer.ManufacturerName = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Atminties dydis (RAM)"))
                {
                    computer.RAM = table[i + 1].Text.ParseInt();
                }

                else if (table[i].Text.Contains("raiška"))
                {
                    if (table[i + 1].Text.Contains("("))
                    {
                        computer.Resolution = table[i + 1].Text.Substring(0, table[i + 1].Text.IndexOf("("));
                    }
                    else
                    {
                        computer.Resolution = table[i + 1].Text;
                    }
                }

                else if (table[i].Text.Contains("Atminties tipas"))
                {
                    computer.RAM_type = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Vaizdo plokštė:"))
                {
                    computer.GraphicsCardName = table[i + 1].Text;
                }
                else if (table[i].Text.Contains("Vaizdo plokštės atmintinė"))
                {
                    computer.GraphicsCardMemory = table[i + 1].Text;
                }

                else if (table[i].Text.Contains("Kietasis diskas SSD") || table[i].Text.Contains("Kietasis diskas HDD") 
                    || table[i].Text.Contains("Diskas SSD M.2 PCIe"))
                {
                    if (table[i + 1].Text.Contains("TB"))
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt() * 1024;
                    }
                    else
                    {
                        computer.StorageCapacity += table[i + 1].Text.ParseInt();
                    }
                }

            }
            _driver.Value.Close();
            //ResetDriver();
            return computer;
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

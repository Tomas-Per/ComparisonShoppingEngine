using DataContent.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ModelLibrary;
using Newtonsoft.Json;
using System.Configuration;

namespace DataContent.DAL.Access
{
    public class ComputerAccess
    {
        private string _apiUrl;
        private ApiHelper _apiHelper;

        public ComputerAccess()
        {
            _apiHelper = new ApiHelper();
            _apiHelper.InitializeClient();
            _apiUrl = ConfigurationManager.AppSettings["api"];
        }

        public async Task PostComputers(List<Computer> computers)
        {
            var json = JsonConvert.SerializeObject(computers);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _apiHelper.Client.PostAsync(_apiUrl + "api/Computers", postData); ;
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else throw new HttpRequestException();
        }
    }
}

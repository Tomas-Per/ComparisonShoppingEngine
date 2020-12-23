using DataContent.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ModelLibrary;
using Newtonsoft.Json;

namespace DataContent.DAL.Access
{
    public class ComputerAccess
    {
        private string _apiUrl = "https://localhost:44315/";
        private ApiHelper _apiHelper;

        public ComputerAccess()
        {
            _apiHelper = new ApiHelper();
            _apiHelper.InitializeClient();
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

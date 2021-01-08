using DataContent.DAL.Helpers;
using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;

namespace DataContent.DAL.Access
{
    public class SmartphoneAccess
    {
        private string _apiUrl;
        private ApiHelper _apiHelper;

        public SmartphoneAccess()
        {
            _apiHelper = new ApiHelper();
            _apiHelper.InitializeClient();
            _apiUrl = ConfigurationManager.AppSettings["api"];
            _apiHelper.Client.BaseAddress = new Uri(_apiUrl);
            _apiHelper.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task PostSmartphones(List<Smartphone> smartphones)
        {
            var json = JsonConvert.SerializeObject(smartphones);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _apiHelper.Client.PostAsync(_apiUrl + "api/Smartphones/", postData); ;
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else throw new HttpRequestException();
        }
    }
}

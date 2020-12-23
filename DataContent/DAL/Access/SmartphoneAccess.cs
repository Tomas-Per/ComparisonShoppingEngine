﻿using DataContent.DAL.Helpers;
using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Access
{
    public class SmartphoneAccess
    {
        private string _apiUrl = "https://localhost:44315/";
        private ApiHelper _apiHelper;

        public SmartphoneAccess()
        {
            _apiHelper = new ApiHelper();
            _apiHelper.InitializeClient();
        }

        public async Task PostSmartphones(List<Smartphone> smartphones)
        {
            var json = JsonConvert.SerializeObject(smartphones);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _apiHelper.Client.PostAsync(_apiUrl + "api/Smartphones", postData); ;
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else throw new HttpRequestException();
        }
    }
}

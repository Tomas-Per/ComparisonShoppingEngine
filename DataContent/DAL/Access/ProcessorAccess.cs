using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DataContent.DAL.Helpers;
using System.Configuration;
using System.Net.Http.Headers;

namespace DataContent.DAL.Access
{
    public class ProcessorAccess
    {
        private string _apiUrl;
        private ApiHelper _apiHelper;

        public ProcessorAccess()
        {
            _apiHelper = new ApiHelper();
            _apiHelper.InitializeClient();
            _apiUrl = ConfigurationManager.AppSettings["api"];
            _apiHelper.Client.BaseAddress = new Uri(_apiUrl);
            _apiHelper.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Processor> GetByModelAsync(string model)
        {
            using HttpResponseMessage response = await _apiHelper.Client.GetAsync(_apiUrl + "api/Processors/Models/" + model);
            Processor processor;
            if (response.IsSuccessStatusCode)
            {
                processor = await response.Content.ReadAsAsync<Processor>();
                return processor;
            }
            else throw new HttpRequestException();


        }
    }
}

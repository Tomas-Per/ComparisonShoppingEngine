using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DataContent.DAL.Helpers;

namespace DataContent.DAL.Access
{
    public class ProcessorAccess
    {
        private string apiUrl = "https://localhost:44315/";
        private ApiHelper _apiHelper;

        public ProcessorAccess()
        {
            _apiHelper = new ApiHelper();
            _apiHelper.InitializeClient();
        }

        public async Task<Processor> GetByModelAsync(string model)
        {
            using HttpResponseMessage response = await _apiHelper.Client.GetAsync(apiUrl + "Models/" + model);
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

using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DataContent.DAL.Helpers;

namespace DataContent.DAL.Access
{
    public static class ProcessorAccess
    {
        private static string apiUrl = "https://localhost:44315/";

        public async static Task<Processor> GetByModelAsync(string model)
        {
            using HttpResponseMessage response = await ApiHelper.Client.GetAsync(apiUrl + "Models/" + model);
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

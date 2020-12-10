using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Helpers
{
    public static class ProcessorServiceHelper
    {
        private static HttpClient _client;

        public static void InitializeClient()
        {
            _client = new HttpClient();
        }

    }
}

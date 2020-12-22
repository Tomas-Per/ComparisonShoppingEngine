using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Helpers
{
    public static class ApiHelper
    {
        public static HttpClient Client;

        public static void InitializeClient()
        {
            Client = new HttpClient();
        }

    }
}

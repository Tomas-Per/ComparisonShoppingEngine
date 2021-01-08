using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Helpers
{
    public class ApiHelper
    {
        public HttpClient Client;

        public void InitializeClient()
        {
            Client = new HttpClient();
        }

    }
}

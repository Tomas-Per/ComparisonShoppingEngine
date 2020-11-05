using System;

namespace ItemLibrary
{
    public struct Processor
    {
        public string Name { get; set; }
        public string AmazonLink { get; set; }
        public  string AmazonBin { get; set; }

        public Processor(string name, string link)
        {
            Name = name;
            try
            {
                //constructor parses elements which is needed to generate AmazonURL in URLGenerator project

                AmazonLink = link.Substring(0, link.IndexOf("&dc"));
                string binStart = link.Substring(link.IndexOf("bin%") + 4);
                AmazonBin = "%7C" + binStart.Substring(2);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

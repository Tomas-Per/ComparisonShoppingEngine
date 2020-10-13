namespace ItemLibrary
{
    public struct Processor
    {
        public string Name { get; }
        public string AmazonLink { get; }
        public  string AmazonBin { get; }

        public Processor(string name, string link)
        {
            Name = name;
            AmazonLink = link.Substring(0, link.IndexOf("&dc")) ;
            string binStart = link.Substring(link.IndexOf("bin%") + 4);
            AmazonBin = "%7C" + binStart.Substring(2);
        }
    }
}

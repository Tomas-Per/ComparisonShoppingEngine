using ItemLibrary;
using System.Collections.Generic;
using System.Linq;

namespace URLGenerator
{
    public class AmazonURLGenerator : IURLGenerator
    {
        private string _link = "https://www.amazon.com";

        public string GenerateURL(List<Processor> filters)
        {
            if(!filters.Any())
            {
                return _link;
            }
            _link = filters.ElementAt(0).AmazonLink;
            filters.RemoveAt(0);
            foreach (var filter in filters)
            {
                _link = _link + filter.AmazonBin;
            }
            return _link;
        }

    }
}



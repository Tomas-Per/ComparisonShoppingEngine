using ItemLibrary;
using System.Collections.Generic;


namespace URLGenerator
{
    public interface URLGenerator
    {
        public string GenerateURL(List<Processor> filters);
    }
}

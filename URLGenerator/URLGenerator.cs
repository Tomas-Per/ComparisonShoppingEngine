using ItemLibrary;
using System.Collections.Generic;


namespace URLGenerator
{
    public interface IURLGenerator
    {
        public string GenerateURL(List<Processor> filters);
    }
}

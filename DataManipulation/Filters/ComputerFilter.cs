using Parsing;
using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace DataManipulation.Filters
{
    public class ComputerFilter : Filter<Computer>
    {
        //Filters item list by the processor
        public void FilterByProcessor(string processor, List<Computer> items, FilterList filter)
        {
            filter(items.Where(item => (item.Processor.Name).DeleteSpecialChars() == processor).ToList());
        }
    }
}

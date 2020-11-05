using ItemLibrary;
using System.Collections.Generic;
using System.Linq;

namespace DataManipulation.Filters
{
    public class ComputerFilter : Filter<Computer>
    {
        public ComputerFilter(List<Computer> items) : base(items)
        {

        }
        //Filters item list by the processor and sets the protected field to the filtered list
        public List<Computer> FilterByProcessor(string processor)
        {
            List<Computer> result = _items.Where(item => item.ProcessorName == processor).ToList();
            return result;
        }
    }
}

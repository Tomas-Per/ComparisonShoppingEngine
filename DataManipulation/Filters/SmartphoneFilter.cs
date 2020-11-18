using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using Parsing;

namespace DataManipulation.Filters
{
    public class SmartphoneFilter : Filter<Smartphone>
    {
        public SmartphoneFilter(List<Smartphone> items) : base(items)
        {

        }

        public List<Smartphone> FilterByProcessor(string processor)
        {
            List<Smartphone> result = _items.Where(item => (item.Processor).DeleteSpecialChars() == processor).ToList();
            return result;
        }

        public List<Smartphone> FilterByScreenDiagonal(double diagonal)
        {
            List<Smartphone> result = _items.Where(item => item.ScreenDiagonal == diagonal).ToList();
            return result;
        }

        public List<Smartphone> FilterByRAM(int RAM)
        {
            List<Smartphone> result = _items.Where(item => item.RAM == RAM).ToList();
            return result;
        }
    }
}

using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataManipulation.Filters
{
    class ComputerFilter : Filter<Computer>
    {
        public ComputerFilter(List<Computer> items) : base(items)
        {

        }
    }
}

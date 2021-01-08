using CsvHelper.Configuration;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataContent.ReadingCSV.Mappers
{
    public sealed class FilterMap : ClassMap<FilterSpec>
    {
        public FilterMap()
        {
            Map(x => x.Name).Index(0);
        }
    }
}

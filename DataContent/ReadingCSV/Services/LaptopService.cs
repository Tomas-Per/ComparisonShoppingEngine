using CsvHelper;
using DataContent.ReadingCSV.Mappers;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataContent.ReadingCSV.Services
{
    public class LaptopService
    {
        public List<Computer> ReadCsvFile(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.RegisterClassMap<LaptopMap>();
                    var records = csv.GetRecords<Computer>().ToList();

                    return records;
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not found");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}


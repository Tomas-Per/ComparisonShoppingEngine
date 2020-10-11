using CsvHelper;
using ItemLibrary.ReadingCSV.Mappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ItemLibrary.ReadingCSV.Services
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
            catch (FileNotFoundException e)
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


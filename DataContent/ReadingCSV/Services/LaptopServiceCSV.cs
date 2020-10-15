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
    public class LaptopServiceCSV
    {
        public List<Computer> ReadData(string path)
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
        public void WriteCSVFile(string path, List<Computer> computer)
        {
            using (StreamWriter sw = new StreamWriter(path))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                var headers = new List<String>{"laptop_name", "laptop_url", "laptop_price", "laptop_manufacturer",
                    "laptop_resolution", "laptop_processor_class", "laptop_ram_type", "laptop_ram",
                    "laptop_storage", "laptop_graphic_card", "laptop_graphic_card_memory" };
                foreach(String head in headers)
                {
                    cw.WriteField(head);
                }
                cw.NextRecord();
                foreach (Computer comp in computer)
                {
                    cw.Configuration.RegisterClassMap<LaptopMap>();
                    cw.WriteRecord<Computer>(comp);
                    cw.NextRecord();
                }
            }
        }
    }
}


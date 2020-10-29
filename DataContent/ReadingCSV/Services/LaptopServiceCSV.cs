using CsvHelper;
using DataContent.ReadingCSV.Mappers;
using ItemLibrary;
using ExceptionsLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataContent.ReadingCSV.Services
{
    public class LaptopServiceCSV : IData<Computer>
    {
        //reads Laptop list from CSV file
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
                throw new DataCustomException("File not found", this);
            }
            catch (Exception e)
            {
                throw new DataCustomException("Something's wrong happened:" + e.Message, this);
            }
        }

        //writes Laptop list to CSV file
        public void WriteCSVFile(string path, List<Computer> computer)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                using (CsvWriter cw = new CsvWriter(sw))
                {
                    var headers = new List<String>{"laptop_name", "laptop_url", "laptop_price", "laptop_manufacturer",
                    "laptop_resolution", "laptop_processor_class", "laptop_ram_type", "laptop_ram",
                    "laptop_storage", "laptop_graphic_card", "laptop_graphic_card_memory" };
                    foreach (String head in headers)
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
            catch (FileNotFoundException)
            {
                throw new DataCustomException("File not found", this);
            }
            catch (FileLoadException)
            {
                throw new DataCustomException("File could not be opened", this);
            }
            catch (Exception e)
            {
                throw new DataCustomException("Something's wrong happened:" + e.Message, this);
            }
        }
    }
}


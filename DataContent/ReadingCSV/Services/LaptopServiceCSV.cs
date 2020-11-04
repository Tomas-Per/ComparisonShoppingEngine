﻿using CsvHelper;
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
    public class LaptopServiceCSV : IData<IEnumerable<Computer>>
    {
        public string Path { get; set; }
        private FileMode Filemode { get; set; }
        public LaptopServiceCSV(string path)
        {
            Path = path;
            Filemode = FileMode.Append;
        }
        //reads Laptop list from CSV file
        public IEnumerable<Computer> ReadData()
        {
            try
            {
                using (var reader = new StreamReader(Path))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.RegisterClassMap<LaptopMap>();
                    var records = csv.GetRecords<Computer>();

                    return records.ToList();
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
        public void WriteData(IEnumerable<Computer> computer)
        {
            try
            {
                using (var stream = File.Open(Path, Filemode))
                using (StreamWriter sw = new StreamWriter(stream))
                using (CsvWriter cw = new CsvWriter(sw))
                {
                    var headers = new List<String>{"laptop_name", "laptop_url", "laptop_price", "laptop_manufacturer",
                    "laptop_resolution", "laptop_processor_class", "laptop_ram_type", "laptop_ram",
                    "laptop_storage", "laptop_graphic_card", "laptop_graphic_card_memory", "laptop_image_link" };
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


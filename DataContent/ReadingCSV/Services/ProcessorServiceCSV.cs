using CsvHelper;
using DataContent.ReadingCSV.Mappers;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataContent.ReadingCSV.Services
{
    class ProcessorServiceCSV
    {
        public List<Processor> ReadData(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.RegisterClassMap<ProcessorMap>();
                    var records = csv.GetRecords<Processor>().ToList();

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

        public void WriteCSVFile(string path, List<Processor> processors)
        {
            using (var stream = File.Open(path, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(stream))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                foreach (Processor processor in processors)
                {
                    cw.Configuration.RegisterClassMap<ProcessorMap>();
                    cw.WriteRecord<Processor>(processor);
                    cw.NextRecord();
                }
            }
        }
    }
}

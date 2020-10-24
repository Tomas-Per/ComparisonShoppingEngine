using CsvHelper;
using DataContent.ReadingCSV.Mappers;
using ExceptionsLibrary;
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
                throw new DataCustomException("File not found", this);
            }
            catch (Exception e)
            {
                throw new DataCustomException("Something's wrong happened:" + e.Message, this);
            }
        }

        public void WriteCSVFile(string path, List<Processor> processors)
        {
            try
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

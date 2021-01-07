using CsvHelper;
using DataContent.ReadingCSV.Mappers;
using ExceptionsLogging;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataContent.ReadingCSV.Services
{
    public class ProcessorServiceCSV
    {   private string Path { get; set; }
        private FileMode Filemode { get; set; }
        private ExceptionLogger logger = new ExceptionLogger();
        public ProcessorServiceCSV(string path)
        {
            Path = path;
            Filemode = FileMode.Append;
        }
        //reads Processor list from CSV file
        public IEnumerable<object> ReadData()
        {
            try
            {
                using (var reader = new StreamReader(Path))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.RegisterClassMap<ProcessorMap>();
                    var records = csv.GetRecords<Processor>().ToList();

                    return records.Cast<object>().ToList();
                }
            }
            catch (FileNotFoundException e)
            {
                logger.Log(e);
                throw;
            }
            catch (Exception e)
            {
                logger.Log(e);
                throw new Exception("Something's wrong happened:" + e.Message);
            }
        }

        //writes Processor list to CSV file
        public void WriteData(IEnumerable<object> processors)
        {
            try
            {
                using (var stream = File.Open(Path, Filemode))
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
            catch (FileNotFoundException e)
            {
                logger.Log(e);
                throw;
            }
            catch (FileLoadException e)
            {
                logger.Log(e);
                throw;
            }
            catch (Exception e)
            {
                logger.Log(e);
                throw new Exception("Something's wrong happened:" + e.Message);
            }
        }
    }
}

using CsvHelper;
using DataContent.ReadingCSV.Mappers;
using ExceptionsLogging;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataContent.ReadingCSV.Services
{
    class SmartphoneServiceCSV
    {
        private ExceptionLogger logger = new ExceptionLogger();
        public string Path { get; set; }
        private FileMode Filemode { get; set; }
        public SmartphoneServiceCSV(string path)
        {
            Path = path;
            Filemode = FileMode.Append;
        }
        public IEnumerable<Smartphone> ReadData()
        {
            try
            {
                using (var reader = new StreamReader(Path))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.RegisterClassMap<SmartphoneMap>();
                    var records = csv.GetRecords<Smartphone>();

                    return records.ToList();
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

        public void WriteData(IEnumerable<Smartphone> list)
        {
            try
            {
                using (var stream = File.Open(Path, Filemode))
                using (StreamWriter sw = new StreamWriter(stream))
                using (CsvWriter cw = new CsvWriter(sw))
                {
                   
                    foreach (Smartphone smartphone in list)
                    {
                        cw.Configuration.RegisterClassMap<SmartphoneMap>();
                        cw.WriteRecord<Smartphone>(smartphone);
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

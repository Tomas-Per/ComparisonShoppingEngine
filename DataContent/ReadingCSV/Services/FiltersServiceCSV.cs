using CsvHelper;
using DataContent.ReadingCSV.Mappers;
using ExceptionsLogging;
using ModelLibrary;
using PathLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataContent.ReadingCSV.Services
{
    public class FiltersServiceCSV
    {
        public string Path { get; set; }
        private FileMode Filemode { get; set; }
        public FiltersServiceCSV(string path)
        {
            Path = path;
            Filemode = FileMode.Append;
        }
        public IEnumerable<FilterSpec> ReadData()
        {
            try
            {
                using (var reader = new StreamReader(Path))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.RegisterClassMap<FilterMap>();
                    var records = csv.GetRecords<FilterSpec>();

                    return records.ToList();
                }
            }
            catch (FileNotFoundException e)
            {
                ExceptionLogger.Log(e);
                throw;
            }
            catch (Exception e)
            {
                ExceptionLogger.Log(e);
                throw new Exception("Something's wrong happened:" + e.Message);
            }
        }
        public void WriteData(IEnumerable<FilterSpec> names)
        {
            try
            {
                using (var stream = File.Open(Path, Filemode))
                using (StreamWriter sw = new StreamWriter(stream))
                using (CsvWriter cw = new CsvWriter(sw))
                {
                    foreach (FilterSpec name in names)
                    {
                        cw.Configuration.RegisterClassMap<FilterMap>();
                        cw.WriteField(name);
                        cw.NextRecord();
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                ExceptionLogger.Log(e);
                throw;
            }
            catch (FileLoadException e)
            {
                ExceptionLogger.Log(e);
                throw;
            }
            catch (Exception e)
            {
                ExceptionLogger.Log(e);
                throw new Exception("Something's wrong happened:" + e.Message);
            }
        }
    }
}

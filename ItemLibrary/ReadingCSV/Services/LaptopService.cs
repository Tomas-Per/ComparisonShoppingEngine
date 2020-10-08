using CsvHelper;
using ItemLibrary.ReadingCSV.Mappers;
using System;
using System.Collections.Generic;
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
                using (var reader = new System.IO.StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<LaptopMap>();
                    var records = csv.GetRecords<Computer>().ToList();
                    return records;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}


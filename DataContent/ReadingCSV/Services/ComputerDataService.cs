using ItemLibrary;
using ItemLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContent.ReadingCSV.Services
{
    class ComputerDataService :IData<IEnumerable<Computer>>
    {
        private DbContext _db { get; set; }
        static void Main()
        {
            var a = new Computer() { Name = "Apple1", Price = 16.28, ItemURL = "www.b.com", RAM = 16, ManufacturerName = "Apple",
                                    Processor = new Processor { Name = "Intel" }, StorageCapacity = 256, Resolution = "1980x720" };
         
            List<Computer> _list = new List<Computer>();
            _list.Add(a);
            var serv = new ComputerDataService();
            serv.WriteData(_list);

        }
        public ComputerDataService()
        {
           // _db = new ComputerContext();
        }

        public IEnumerable<Computer> ReadData()
        {
            throw new NotImplementedException();
        }

        public void WriteData(IEnumerable<Computer> list)
        {
            using (_db = new ComputerContext())
            {
                _db.AddRange(list);
                _db.SaveChanges();
            }
        }
    }
}

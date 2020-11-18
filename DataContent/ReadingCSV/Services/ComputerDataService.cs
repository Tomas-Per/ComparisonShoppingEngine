using ItemLibrary;
using ItemLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContent.ReadingCSV.Services
{
    class ComputerDataService :IData<IEnumerable<Computer>>
    {
        private ComputerContext _db { get; set; }
        static void Main()
        {
            var a = new Computer() { Name = "Apple1", Price = 16.28, ItemURL = "www.b.com", RAM = 16, ManufacturerName = "Apple",
                                    Processor = new Processor { Name = "Intel" }, StorageCapacity = 256, Resolution = "1980x720" };
         
            List<Computer> _list = new List<Computer>();
            _list.Add(a);
            var serv = new ComputerDataService();
            serv.WriteData(_list);

        }
        public IEnumerable<Computer> ReadData()
        {
            using (_db = new ComputerContext())
            {
                var computers = _db.Computers.Include(x => x.Processor).ToList();
                return computers;
            }
        }

        public void WriteData(IEnumerable<Computer> list)
        {
            using (_db = new ComputerContext())
            {
                foreach(Computer computer in list)
                {
                    var sameComputer = _db.Computers
                                            .Where(x => x.Name == computer.Name)
                                            .Where(x => x.ShopName == computer.ShopName)
                                            .First();
                    if (sameComputer != null) sameComputer.Price = computer.Price;
                    else _db.Add(computer);
                    
                }
                _db.SaveChanges();
            }
        }
    }
}

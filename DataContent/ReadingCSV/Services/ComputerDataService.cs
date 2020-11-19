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
            var p = new Processor { Name = "Intel Core i5", Model = "Intel Core i5-10400F", Cache = 12, MinCores = 6 };
            var a = new Computer() { Name = "Apple1", Price = 162.28, ItemURL = "www.b.com", RAM = 16, ManufacturerName = "Apple",
                                    Processor = p, StorageCapacity = 256, Resolution = "1980x720" };
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
                foreach (Computer computer in list)
                {
                    var sameComputer = _db.Computers
                                            .Where(x => x.Name == computer.Name
                                            && x.ShopName == computer.ShopName)
                                            .FirstOrDefault();
                    if (sameComputer != null) sameComputer.Price = computer.Price;
                    else
                    {
                        var sameComputers = _db.Computers
                                            .Where(x => x.RAM == computer.RAM
                                            && x.StorageCapacity == computer.StorageCapacity
                                            && x.Resolution.Contains(computer.Resolution)
                                            && computer.Resolution.Contains(x.Resolution))
                                            .ToList();
                        foreach(var sameComp in sameComputers)
                        {
                            if (!sameComp.Equals(computer)) sameComputers.Remove(sameComp);
                        }
                        _db.Add(computer);
                    }
                }
                _db.SaveChanges();
            }
        }
    }
}

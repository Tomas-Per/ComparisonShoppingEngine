using DataManipulation.DataFillers;
using ItemLibrary;
using ItemLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContent.ReadingDB.Services
{
    public class ComputerDataService :IData<IEnumerable<Computer>>
    {
        private ComputerContext _db { get; set; }
       
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
                        var similarComputers = _db.Computers
                                            .Where(x => x.RAM == computer.RAM
                                            && x.StorageCapacity == computer.StorageCapacity
                                            && x.Resolution.Contains(computer.Resolution)
                                            && computer.Resolution.Contains(x.Resolution))
                                            .ToList();
                        var sameComputers = new List<Computer>();
                        foreach(var similarComputer in similarComputers)
                        {
                            if (similarComputer.Equals(computer)) sameComputers.Add(similarComputer);
                        }
                        if(sameComputers.Count()>0)sameComputers = new ComputerFiller().FillComputers(sameComputers);
                        _db.SaveChanges();
                        computer.Processor = _db.Processors.Find(computer.Processor.Id);
                        _db.Add(computer);
                    }
                }
                _db.SaveChanges();
            }
        }
    }
}

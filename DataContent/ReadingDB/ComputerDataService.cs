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
    public class ComputerDataService :IData<Computer>
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
                    if (sameComputer != null)
                    {
                        sameComputer.Price = computer.Price;
                        sameComputer.ModifyDate = DateTime.Now;
                    }
                    else
                    {
                        var similarComputers = _db.Computers.Include(p => p.Processor)
                                            .Where(x => x.RAM == computer.RAM
                                            && x.StorageCapacity == computer.StorageCapacity
                                            && x.Resolution.Contains(computer.Resolution)
                                            && computer.Resolution.Contains(x.Resolution))
                                            .ToList();
                        var sameComputers = new List<Computer>();
                        foreach (var similarComputer in similarComputers)
                        {
                            if (similarComputer.Equals(computer)) sameComputers.Add(similarComputer);
                        }
                        if (sameComputers.Count() > 0)
                        {
                            sameComputers = new ComputerFiller().FillComputers(sameComputers);
                            computer.ItemCode = sameComputers[0].Id;
                        }
                        _db.SaveChanges();
                        computer.Processor = _db.Processors.Find(computer.Processor.Id);
                        _db.Add(computer);
                    }
                }
                _db.SaveChanges();
            }
        }
        public Computer GetDataByID(int id)
        {
            using (_db = new ComputerContext())
            {
                var computer = _db.Computers.Include(x => x.Id == id).FirstOrDefault();
                return computer;
            }
        }
        public void UpdateData(Computer computer)
        {
            using (_db = new ComputerContext())
            {
                var computerInDB = _db.Computers.Where(x => x.Id == computer.Id).FirstOrDefault();
                if (computerInDB != null)
                {
                    computerInDB.Name = computer.Name;
                    computerInDB.ManufacturerName = computer.ManufacturerName;
                    computerInDB.Price = computer.Price;
                    computerInDB.ItemURL = computer.ItemURL;
                    computerInDB.ShopName = computer.ShopName;
                    computerInDB.ImageLink = computer.ImageLink;
                    computerInDB.ItemCategory = computer.ItemCategory;
                    computerInDB.Processor = computer.Processor;
                    computerInDB.GraphicsCardName = computer.GraphicsCardName;
                    computerInDB.GraphicsCardMemory = computer.GraphicsCardMemory;
                    computerInDB.RAM = computer.RAM;
                    computerInDB.RAM_type = computer.RAM_type;
                    computerInDB.Resolution = computer.Resolution;
                    computerInDB.ItemCode = computer.ItemCode;
                    computerInDB.ModifyDate = DateTime.Now;
                }
                _db.SaveChanges();
            }
        }
        public void DeleteData(int id)
        {
            using(_db = new ComputerContext())
            {
                var computer = GetDataByID(id);
                _db.Computers.Remove(computer);
                _db.SaveChanges();
            }
        }
    }
}

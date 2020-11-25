using DataContent.DAL.Interfaces;
using DataManipulation.DataFillers;
using ItemLibrary;
using ItemLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Repositories
{
    class ComputerRepository : IComputerRepository
    {
        private readonly ComputerContext _context;
        
        public ComputerRepository(ComputerContext context)
        {
            _context = context;
        }

        public async Task<List<Computer>> AddComputersAsync(List<Computer> list)
        {
            foreach (Computer computer in list)
            {
                //check for the computer in DB
                var sameComputer = _context.Computers
                                        .Where(x => x.Name == computer.Name
                                        && x.ShopName == computer.ShopName)
                                        .FirstOrDefault();

                //change price and mofidy date if the computer is found in DB
                if (sameComputer != null)
                {
                    sameComputer.Price = computer.Price;
                    sameComputer.ModifyDate = computer.ModifyDate;
                }

                else
                {
                    computer.ItemCode = _context.Computers.Count();

                    //extract simiilar computer from DB
                    var similarComputers = _context.Computers.Include(p => p.Processor)
                                        .Where(x => x.RAM == computer.RAM
                                        && x.StorageCapacity == computer.StorageCapacity
                                        && (x.Resolution.Contains(computer.Resolution) || computer.Resolution.Contains(x.Resolution)))
                                        .ToList();

                    //check and get list of equal computers, but in other shops
                    var sameComputers = new List<Computer>();
                    foreach (var similarComputer in similarComputers)
                    {
                        if (similarComputer.Equals(computer)) sameComputers.Add(similarComputer);
                    }

                    //if equal computers found, fill all possible information
                    if (sameComputers.Count() > 0)
                    {
                        sameComputer = new ComputerFiller().FillComputers(sameComputers);
                        foreach (var sameComp in sameComputers)
                        {
                            sameComp.ManufacturerName = sameComputer.ManufacturerName;
                            sameComp.GraphicsCardName = sameComputer.GraphicsCardName;
                            sameComp.GraphicsCardMemory = sameComputer.GraphicsCardMemory;
                            sameComp.RAM_type = sameComputer.RAM_type;
                        }
                        computer.ItemCode = sameComputers[0].Id;
                    }

                    //update equal computers in DB
                    await _context.SaveChangesAsync();

                    //add new computer with existing Processor to DB
                    computer.Processor = _context.Processors.Find(computer.Processor.Id);
                    _context.Add(computer);
                }
            }
            await _context.SaveChangesAsync();
            return list;
        }

        public async Task<Computer> DeleteComputerAsync(int id)
        {
            var computer = await _context.Computers.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();
            return computer;
        }

        public async Task<List<Computer>> GetAllComputersAsync()
        {
            var computers = await _context.Computers.Include(x => x.Processor).ToListAsync();
            return computers;
        }

        public async Task<Computer> GetComputerByIdAsync(int id)
        {
            var computer = await _context.Computers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return computer;
        }

        public async Task<Computer> UpdateComputerAsync(int id, Computer computer)
        {
            var computerInDB = _context.Computers.Where(x => x.Id == computer.Id).FirstOrDefault();
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
            await _context.SaveChangesAsync();
            return computerInDB;
        }
    }
}

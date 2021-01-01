using DataContent.DAL.Interfaces;
using DataManipulation.DataFillers;
using ModelLibrary;
using ModelLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailSender;
using static ModelLibrary.Categories;

namespace DataContent.DAL.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly ComputerContext _context;
        private readonly UserContext _userContext;
        
        public ComputerRepository(ComputerContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
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
                    //notify users that price became lower
                    if(sameComputer.Price > computer.Price)
                    {   
                        var userNotifier = new UserNotifier();
                        var usersToNotify = await userNotifier.GetUsersForNotification(sameComputer);
                        userNotifier.NotifyUsersWhenPriceDropped(computer, sameComputer.Price, usersToNotify);
                    }

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
                    try
                    {
                        computer.Processor = _context.Processors.Find(computer.Processor.Id);
                    }
                    catch(NullReferenceException)
                    {
                    }
                    _context.Add(computer);
                    _userContext.Add(computer);
                }
            }
            await _context.SaveChangesAsync();
            await _userContext.SaveChangesAsync();
            return list;
        }

        public async Task<Computer> DeleteComputerAsync(int id)
        {
            var computer = await _context.Computers.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Computers.Remove(computer);
            var item = await _userContext.Computers.Where(x => x.Id == id && x.ItemCategory == computer.ItemCategory).FirstOrDefaultAsync();
            _userContext.Computers.Remove(item);
            await _context.SaveChangesAsync();
            return computer;
        }

        public async Task<List<Computer>> GetAllComputersAsync(ItemCategory category, int page)
        {     
            if(page > 0)
            {
                var skip = (page - 1) * 20;
                var computers = await _context.Computers.Where(x => x.ItemCategory == category).OrderBy(x => x.Id).Skip(skip).Take(20).Include(x => x.Processor).ToListAsync();
                return computers;
            }
            else
            {
                var computers = await _context.Computers.Where(x => x.ItemCategory == category).Include(x => x.Processor).ToListAsync();
                return computers;
            }

        }

        public async Task<Computer> GetComputerByIdAsync(int id)
        {
            var computer = await _context.Computers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return computer;
        }

        public async Task<Computer> UpdateComputerAsync(Computer computer)
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

        public async Task<List<Computer>> FindSimilarAsync(Computer computer)
        {
            var computers = await _context.Computers.Where(x => x.ItemCategory == computer.ItemCategory).Include(x => x.Processor).Cast<Item>().ToListAsync();
            return computer.FindSimilar(computers).Cast<Computer>().ToList();
        }
    }
}

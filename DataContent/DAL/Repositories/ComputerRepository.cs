using DataContent.DAL.Interfaces;
using ItemLibrary;
using ItemLibrary.DataContexts;
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

        public Task<Computer> CreateComputerAsync(Computer computer)
        {
            throw new NotImplementedException();
        }

        public Task<Computer> DeleteComputerAsync(Computer computer)
        {
            throw new NotImplementedException();
        }

        public Task<List<Computer>> GetAllComputersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Computer> GetComputerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Computer> UpdateComputerAsync(int id, Computer computer)
        {
            throw new NotImplementedException();
        }
    }
}

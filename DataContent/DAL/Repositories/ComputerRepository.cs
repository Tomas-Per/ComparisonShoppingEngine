using DataContent.DAL.Interfaces;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Repositories
{
    class ComputerRepository : IComputerRepository
    {
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

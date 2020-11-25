using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Interfaces
{
    public interface IComputerRepository
    {
        Task<List<Computer>> GetAllComputersAsync();
        Task<Computer> GetComputerByIdAsync(int id);
        Task<Computer> UpdateComputerAsync(int id, Computer computer);
        Task<List<Computer>> AddComputersAsync(List<Computer> computers);
        Task<Computer> DeleteComputerAsync(int id);

    }
}

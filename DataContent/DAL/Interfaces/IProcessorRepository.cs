using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Interfaces
{
    public interface IProcessorRepository
    {
        Task<List<Processor>> GetAllProcessorsAsync();
        Task<Processor> GetProcessorByIdAsync(int id);
        Task<Processor> GetProcessorByNameAsync(string model);
        Task<Processor> UpdateProcessorAsync(Processor processor);
        Task<List<Processor>> AddProcessorsAsync(List<Processor> processors);
        Task<Processor> DeleteProcessorAsync(int id);
    }
}

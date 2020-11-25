using DataContent.DAL.Interfaces;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Repositories
{
    public class ProcessorRepository : IProcessorRepository
    {
        public Task<List<Processor>> AddProcessorsAsync(List<Processor> processors)
        {
            throw new NotImplementedException();
        }

        public Task<Processor> DeleteProcessorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Processor>> GetAllProcessorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Processor> GetProcessorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Processor> UpdateProcessorAsync(Processor processors)
        {
            throw new NotImplementedException();
        }
    }
}

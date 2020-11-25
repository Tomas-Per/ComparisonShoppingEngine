using DataContent.DAL.Interfaces;
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
    public class ProcessorRepository : IProcessorRepository
    {
        private readonly ComputerContext _context;

        public ProcessorRepository(ComputerContext context)
        {
            _context = context;
        }

        public Task<List<Processor>> AddProcessorsAsync(List<Processor> processors)
        {
            throw new NotImplementedException();
        }

        public Task<Processor> DeleteProcessorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Processor>> GetAllProcessorsAsync()
        {
            var processors = await _context.Processors.ToListAsync();
            return processors;
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

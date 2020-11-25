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

        public async Task<List<Processor>> AddProcessorsAsync(List<Processor> processors)
        {
            _context.AddRange(processors);
            await _context.SaveChangesAsync();
            return processors;
        }

        public async Task<Processor> DeleteProcessorAsync(int id)
        {
            var processor = await _context.Processors.Where(p => p.Id == id).FirstOrDefaultAsync();
            _context.Processors.Remove(processor);
            await _context.SaveChangesAsync();
            return processor;
        }

        public async Task<List<Processor>> GetAllProcessorsAsync()
        {
            var processors = await _context.Processors.ToListAsync();
            return processors;
        }

        public async Task<Processor> GetProcessorByIdAsync(int id)
        {
            var processor = await _context.Processors.Where(x => x.Id == id).FirstOrDefaultAsync();
            return processor;
        }

        public Task<Processor> GetProcessorByNameAsync(string model)
        {
            throw new NotImplementedException();
        }

        public async Task<Processor> UpdateProcessorAsync(Processor processor)
        {
            var processorInDB = _context.Processors.Where(x => x.Id == processor.Id).FirstOrDefault();
            if (processorInDB != null)
            {
                processorInDB.Name = processor.Name;
                processorInDB.Model = processor.Model;
                processorInDB.Cache = processor.Cache;
                processorInDB.MinCores = processor.MinCores;
            }
            await _context.SaveChangesAsync();
            return processor;
        }
    }
}

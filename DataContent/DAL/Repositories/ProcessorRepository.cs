using DataContent.DAL.Interfaces;
using ExceptionsLogging;
using ModelLibrary;
using ModelLibrary.DataContexts;
using ModelLibrary.Exceptions;
using Microsoft.EntityFrameworkCore;
using Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.ComponentsParser;

namespace DataContent.DAL.Repositories
{
    public class ProcessorRepository : IProcessorRepository
    {
        private readonly ComputerContext _context;
        private ExceptionLogger logger = new ExceptionLogger();

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

        public async Task<Processor> GetProcessorByNameAsync(string model)
        {
            if (model.Contains("Processor")) model = model.Substring(0, model.IndexOf("Processor"));
            model = model.DeleteSpecialChars();

            //search for processor in DB
            var processor = _context.Processors
                            .Where(x => x.Model.Contains(model)
                            || model.Contains(x.Model)).FirstOrDefault();

            if (processor != null) return processor;
            else
            {
                //try parse processor from Gpuskin database site
                try
                {
                    processor = new ProcessorParser().ParseProcessor(model);
                    _context.Add(processor);
                    await _context.SaveChangesAsync();
                }
                catch (ProcessorNotFoundException ex)
                {
                    //add new processor but throw exception, so it could be logged
                    processor = new Processor { Model = model };
                    try
                    {
                        processor.SetName(model);
                    }
                    catch (ProcessorInvalidNameException e)
                    {
                        logger.LogProcessorParsingException(processor, e);
                    }

                    _context.Add(processor);
                    await _context.SaveChangesAsync();
                    processor = _context.Processors.Where(x => x.Model == model).FirstOrDefault();

                    logger.LogProcessorParsingException(processor, ex);
                }
                return processor;
            }
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

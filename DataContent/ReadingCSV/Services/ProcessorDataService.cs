﻿using ExceptionsLogging;
using ItemLibrary;
using ItemLibrary.DataContexts;
using Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.ComponentsParser;

namespace DataContent.ReadingCSV.Services
{
    public class ProcessorDataService : IData<IEnumerable<Processor>>
    {
        private ComputerContext _db { get; set; }
        public IEnumerable<Processor> ReadData()
        {
            using (_db = new ComputerContext())
            {
                var processors = _db.Processors.ToList();
                return processors;
            }
        }

        public void WriteData(IEnumerable<Processor> list)
        {
            using (_db = new ComputerContext())
            {
                _db.AddRange(list);
                _db.SaveChanges();
            }
        }
        public Processor GetProcessor(string processorModel)
        {
            processorModel.DeleteSpecialChars();
            if (processorModel.Length > 3)
            {
                using (_db = new ComputerContext())
                {
                    var processor = _db.Processors
                                    .Where(x => x.Model.Contains(processorModel)
                                    || processorModel.Contains(x.Model)).FirstOrDefault();
                    if (processor != null) return processor;
                    else
                    {
                        try
                        {
                            processor = new ProcessorParser().ParseProcessor(processorModel);
                        }
                        catch (ProcessorNotFoundException)
                        {
                            processor = new Processor { Model = processorModel };
                            processor.SetName(processorModel);
                            ExceptionLogger.LogProcessorParsingException(processor);
                        }
                        return processor;
                    }
                }
            }
            else return null;
        }
    }
}

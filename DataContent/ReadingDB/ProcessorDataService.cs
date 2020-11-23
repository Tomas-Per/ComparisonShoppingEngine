using ExceptionsLogging;
using ItemLibrary;
using ItemLibrary.DataContexts;
using Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.ComponentsParser;

namespace DataContent.ReadingDB.Services
{
    public class ProcessorDataService : IDataComponent<Processor>
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
        public Processor GetDataByID(int id)
        {
            using (_db = new ComputerContext())
            {
                var processorInDB = _db.Processors.Where(x => x.Id == id).FirstOrDefault();
                return processorInDB;
            }
        }
        public void UpdateData(Processor processor)
        {
            using(_db = new ComputerContext())
            {
                var processorInDB = _db.Processors.Where(x => x.Id == processor.Id).FirstOrDefault();
                if (processorInDB != null)
                {
                    processorInDB.Name = processor.Name;
                    processorInDB.Model = processor.Model;
                    processorInDB.Cache = processor.Cache;
                    processorInDB.MinCores = processor.MinCores;
                }
                _db.SaveChanges();
            }
        }
        public void DeleteData(int id)
        {
            using(_db = new ComputerContext()){
                var processor = _db.Processors.Where(p => p.Id == id).FirstOrDefault();
                _db.Processors.Remove(processor);
                _db.SaveChanges();
            }
        }
        public Processor GetProcessor(string processorModel)
        {
            //Deletes all trademarks and etc. Deletes sufix Processor
            if (processorModel.Contains("Processor")) processorModel = processorModel.Substring(0, processorModel.IndexOf("Processor"));
            processorModel = processorModel.DeleteSpecialChars();

            using(_db = new ComputerContext())
            {

                //search for processor in DB
                var processor = _db.Processors
                                .Where(x => x.Model.Contains(processorModel)
                                || processorModel.Contains(x.Model)).FirstOrDefault();

                if(processor!= null) return processor;
                else
                {
                    //try parse processor from Gpuskin database site
                    try
                    {
                        processor = new ProcessorParser().ParseProcessor(processorModel);
                        _db.Add(processor);
                        _db.SaveChanges();
                    }
                    catch(ProcessorNotFoundException)
                    {
                        //add new processor but throw exception, so it could be logged
                        processor = new Processor { Model = processorModel };
                        processor.SetName(processorModel);
                        using(_db = new ComputerContext())
                        {
                            _db.Add(processor);
                            _db.SaveChanges();
                            processor = _db.Processors.Where(x => x.Model == processorModel).FirstOrDefault();
                        }
                        ExceptionLogger.LogProcessorParsingException(processor);
                    }
                    return processor;
                }
            }
        }
    }
}

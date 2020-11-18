using ItemLibrary;
using ItemLibrary.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using(_db = new ComputerContext())
            {
                var processor = _db.Processors
                                .Where(x => x.Model.Contains(processorModel)
                                || processorModel.Contains(x.Model)).First();
                if(processor!= null) return processor;
                else
                {
                    //call processor parser
                    return processor;
                }
            }
        }
    }
}

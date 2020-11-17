using CsvHelper.Configuration;

namespace DataContent.ReadingCSV.Mappers
{
    public sealed class ProcessorMap : ClassMap<ItemLibrary.Processor>
    {
        public ProcessorMap()
        {
            Map(x => x.Name).Index(0).Name("Name");
       

        }
    }
}

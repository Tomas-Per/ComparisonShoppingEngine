using CsvHelper.Configuration;

namespace DataContent.ReadingCSV.Mappers
{
    public sealed class ProcessorMap : ClassMap<ModelLibrary.Processor>
    {
        public ProcessorMap()
        {
            Map(x => x.Name).Index(0).Name("Name");
       

        }
    }
}

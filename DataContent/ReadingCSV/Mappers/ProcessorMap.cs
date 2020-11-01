using CsvHelper.Configuration;

namespace DataContent.ReadingCSV.Mappers
{
    public sealed class ProcessorMap : ClassMap<ItemLibrary.Processor>
    {
        public ProcessorMap()
        {
            Map(x => x.Name).Index(0).Name("Name");
            Map(x => x.AmazonLink).Index(1).Name("AmazonLink");
            Map(x => x.AmazonBin).Index(2).Name("AmazonBin");

        }
    }
}

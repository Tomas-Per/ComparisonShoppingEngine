using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Security.Cryptography.X509Certificates;

namespace DataContent.ReadingCSV.Mappers
{
    public sealed class LaptopMap: ClassMap<ItemLibrary.Computer>
    {
        public LaptopMap()
        {
            Map(x => x.Name).Index(0).Name("laptop_name");
            Map(x => x.ItemURL).Index(1).Name("laptop_url");
            Map(x => x.Price).Index(2).Name("laptop_price").TypeConverter<DoubleConverter>();
            Map(x => x.ManufacturerName).Index(3).Name("laptop_manufacturer");
            Map(x => x.Resolution).Index(4).Name("laptop_resolution");
            Map(x => x.ProcessorName).Index(5).Name("laptop_processor_class");
            Map(x => x.RAM_type).Index(6).Name("laptop_ram_type");
            Map(x => x.RAM).Index(7).Name("laptop_ram").TypeConverter<Int32Converter>();
            Map(x => x.StorageCapacity).Index(8).Name("laptop_storage").TypeConverter<Int32Converter>();
            Map(x => x.GraphicsCardName).Index(9).Name("laptop_graphic_card");
            Map(x => x.GraphicsCardMemory).Index(10).Name("laptop_graphic_card_memory");
            Map(x => x.ImageLink).Index(11).Name("laptop_image_link");

        }
    }
}

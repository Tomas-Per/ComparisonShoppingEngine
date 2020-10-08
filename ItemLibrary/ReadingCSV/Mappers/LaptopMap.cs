using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLibrary.ReadingCSV.Mappers
{
    public sealed class LaptopMap: ClassMap<Computer>
    {
        public LaptopMap()
        {
            Map(x => x.Name).Name("laptop_name");
            Map(x => x.ItemURL).Name("laptop_url");
            Map(x => x.Price).Name("laptop_price");
            Map(x => x.ManufacturerName).Name("laptop_manufacturer");
            Map(x => x.Resolution).Name("laptop_resolution");
            Map(x => x.ProcessorName).Name("laptop_processor_class");
            Map(x => x.RAM_type).Name("laptop_ram_type");
            Map(x => x.RAM).Name("laptop_ram");
            Map(x => x.StorageCapacity).Name("laptop_storage");
            Map(x => x.GraphicsCardName).Name("laptop_graphic_card");
            Map(x => x.GraphicsCardMemory).Name("laptop_graphic_card_memory");
            
        }
    }
}

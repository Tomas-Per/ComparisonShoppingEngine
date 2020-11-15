using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContent.ReadingCSV.Mappers
{
    public sealed class SmartphoneMap :ClassMap<Smartphone>
    {
        public SmartphoneMap()
        {
            Map(x => x.Name).Index(0);
            Map(x => x.ItemURL).Index(1);
            Map(x => x.Price).Index(2).TypeConverter<DoubleConverter>();
            Map(x => x.ManufacturerName).Index(3);
            Map(x => x.ScreenDiagonal).Index(4).TypeConverter<DoubleConverter>();
            Map(x => x.Processor).Index(5);
            Map(x => x.RAM).Index(7).TypeConverter<Int32Converter>();
            Map(x => x.Storage).Index(8).TypeConverter<Int32Converter>();
            Map(x => x.DisplaySize).Index(9);
            Map(x => x.BatteryStorage).Index(10).TypeConverter<Int32Converter>();
            Map(x => x.ImageLink).Index(11);
            Map(x => x.BackCameraMP).Index(12);
            Map(x => x.FrontCameraMP).Index(13);

        }
    }
}

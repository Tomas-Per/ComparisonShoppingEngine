using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLibrary
{
    public class Computer : Item
    {
        public string ProcessorName { get; set; }
        public double ProcessorFrequency { get; set; }
        public string GraphicsCardName { get; set; }
        public int GraphicsCardMemory { get; set; }
        public int RAM { get; set; }
        public string RAM_type { get; set; }
        public string Resolution { get; set; }
        public int StorageCapacity { get; set; }


        //builder pattern??
        public Computer(ulong Code, ulong Barcode, int price, string name, string Manufacturer,
            string ProcName, double ProcFreq, string GPUname, int GPUmem, int ram, string RAMtype,
            string resol, int storage)
        {

            //base class
            ItemCode = Code;
            ItemBarcode = Barcode;
            Price = price;
            Name = name;
            ManufacturerName = Manufacturer;

            //derived class
            ProcessorName = ProcName;
            ProcessorFrequency = ProcFreq;
            GraphicsCardName = GPUname;
            GraphicsCardMemory = GPUmem;
            RAM = ram;
            RAM_type = RAMtype;
            Resolution = resol;
            StorageCapacity = storage;
        }


        //irgi testavimui
        public override void PrintOut()
        {
            Console.WriteLine(ItemCode + " " + ItemBarcode + " " + Price + " " + Name + " " + ManufacturerName);
            Console.WriteLine(ProcessorName + " " + ProcessorFrequency + " " + RAM + " " + RAM_type + " " + StorageCapacity);
            Console.WriteLine(GraphicsCardName + " " + GraphicsCardMemory + " " + Resolution);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLibrary
{
    public class Computer : Item
    {
        public string ProcessorName { get; set; }
        public string GraphicsCardName { get; set; }
        public int GraphicsCardMemory { get; set; }
        public int RAM { get; set; }
        public string RAM_type { get; set; }
        public string Resolution { get; set; }
        public int StorageCapacity { get; set; }


        //builder pattern??
        public Computer(ulong code, double price, string name, string manufacturer, string itemURL,
            string procName, double procFreq, string gpuName, int gpuMem, int ram, string ramType,
            string resol, int storage)
        {

            //base class
            ItemCode = code;
            Price = price;
            Name = name;
            ManufacturerName = manufacturer;
            ItemURL = itemURL;

            //derived class
            ProcessorName = procName;
            GraphicsCardName = gpuName;
            GraphicsCardMemory = gpuMem;
            RAM = ram;
            RAM_type = ramType;
            Resolution = resol;
            StorageCapacity = storage;
        }


        //irgi testavimui
        public override void PrintOut()
        {
            Console.WriteLine(ItemCode + " " + " " + Price + " " + Name + " " + ManufacturerName);
            Console.WriteLine(ProcessorName + " " + ProcessorFrequency + " " + RAM + " " + RAM_type + " " + StorageCapacity);
            Console.WriteLine(GraphicsCardName + " " + GraphicsCardMemory + " " + Resolution);
        }
    }
}

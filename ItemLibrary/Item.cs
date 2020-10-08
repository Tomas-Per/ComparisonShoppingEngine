using System;

namespace ItemLibrary
{
    public abstract class Item
    {
        public ulong ItemCode { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string ItemURL { get; set; }


        //cia testavimui
        public virtual void PrintOut()
        {
            Console.WriteLine(ItemCode + " "  + " " + Price + " " + Name + " " + ManufacturerName);
        }

    }
}

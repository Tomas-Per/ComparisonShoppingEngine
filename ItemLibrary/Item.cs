using System;

namespace ItemLibrary
{
    public abstract class Item
    {
        public ulong ItemCode { get; set; }
        public ulong ItemBarcode { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }



        //cia testavimui
        public virtual void PrintOut()
        {
            Console.WriteLine(ItemCode + " " + ItemBarcode + " " + Price + " " + Name + " " + ManufacturerName);
        }

    }
}

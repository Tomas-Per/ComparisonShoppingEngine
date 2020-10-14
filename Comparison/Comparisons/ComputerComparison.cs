using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comparison.Comparisons
{
    class ComputerComparison : ItemComparison
    {
        protected int StorageWeight { get; set; }
        protected int RamWeight { get; set; }
        public ComputerComparison(int priceWeight, int storageWeight, int ramWeight)
                                    : base(priceWeight)
        {
            StorageWeight = storageWeight;
            RamWeight = ramWeight;
            TotalWeight += StorageWeight + RamWeight;
        }
        public (double, double) StorageComparison (int mainStorage, int comparingStorage)
        {
            return SpecComparison(Convert.ToDouble(mainStorage), Convert.ToDouble(comparingStorage), StorageWeight);
        }
        public (double, double) RamComparison (int mainRAM, int comparingRAM)
        {
            return SpecComparison(Convert.ToDouble(mainRAM), Convert.ToDouble(comparingRAM), RamWeight);
        }
        //might delete this if not needed in the future:
        public void ObjectComparison(Computer mainItem, Computer comparingItem)
        {
            PriceComparison(mainItem.Price, comparingItem.Price);
            StorageComparison(mainItem.StorageCapacity, comparingItem.StorageCapacity);
            RamComparison(mainItem.RAM, comparingItem.RAM);
        }
    }
}

using ItemLibrary;
using System;

namespace Comparison.Comparisons
{
    class ComputerComparison : ItemComparison<Computer>
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

        //Compares storage by given interval
        public (double, double) StorageComparison (int mainStorage, int comparingStorage)
        {
            return SpecComparison(Convert.ToDouble(mainStorage), Convert.ToDouble(comparingStorage), StorageWeight);
        }

        //Compares RAM by given interval
        public (double, double) RamComparison (int mainRAM, int comparingRAM)
        {
            return SpecComparison(Convert.ToDouble(mainRAM), Convert.ToDouble(comparingRAM), RamWeight);
        }

        //Compares two given computers by preferences
        public override (double,double) SumAllRankings(Computer mainItem, Computer comparingItem)
        {
            PriceComparison(mainItem.Price, comparingItem.Price);
            StorageComparison(mainItem.StorageCapacity, comparingItem.StorageCapacity);
            RamComparison(mainItem.RAM, comparingItem.RAM);

            return ItemRanking;
        }
    }
}

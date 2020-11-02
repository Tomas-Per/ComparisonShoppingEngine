using ItemLibrary;
using System;

namespace Comparison.Comparisons
{
    public class ComputerComparison : ItemComparison<Computer>
    {
        public int StorageWeight { get; set; }
        public int RamWeight { get; set; }
        public ComputerComparison(int priceWeight, int storageWeight, int ramWeight)
                                    : base(priceWeight)
        {
            StorageWeight = storageWeight;
            RamWeight = ramWeight;
            TotalWeight += StorageWeight + RamWeight;
        }

        //Compares items storage by given items storage
        public (double, double) StorageComparison (int mainStorage, int comparingStorage)
        {
            return SpecComparison(Convert.ToDouble(mainStorage), Convert.ToDouble(comparingStorage), StorageWeight);
        }

        //Compares items RAMs by given items RAMs
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

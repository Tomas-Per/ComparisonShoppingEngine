using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comparison.Comparisons
{
    class ComputerComparison : ItemComparison<Computer>
    {
        protected int StorageWeight { get; set; }
        protected int RamWeight { get; set; }
        protected (double, double) StorageRanking { get; set; }
        protected (double, double) RamRanking { get; set; }
        public ComputerComparison(int priceWeight, int storageWeight, int ramWeight)
                                    : base(priceWeight)
        {
            UpdateWeights(priceWeight, storageWeight, ramWeight);
        }
        public void StorageComparison (int mainStorage, int comparingStorage)
        {
            StorageRanking = SpecComparison(Convert.ToDouble(mainStorage), Convert.ToDouble(comparingStorage), StorageWeight);
            ItemRanking = (ItemRanking.Item1 + StorageRanking.Item1, ItemRanking.Item2 + StorageRanking.Item2);
        }
        public void RamComparison(int mainRAM, int comparingRAM)
        {
            RamRanking = SpecComparison(Convert.ToDouble(mainRAM), Convert.ToDouble(comparingRAM), RamWeight);
            ItemRanking = (ItemRanking.Item1 + StorageRanking.Item1, ItemRanking.Item2 + StorageRanking.Item2);
        }
        public override void UpdateRatings(Computer mainItem, Computer comparingItem)
        {
            ItemRanking = (0, 0);
            PriceComparison(mainItem.Price, comparingItem.Price);
            StorageComparison(mainItem.StorageCapacity, comparingItem.StorageCapacity);
            RamComparison(mainItem.RAM, comparingItem.RAM);
        }
        public void UpdateWeights(int priceWeight, int storageWeight, int ramWeight)
        {
            UpdateWeights(priceWeight);
            StorageWeight = storageWeight;
            RamWeight = ramWeight;
            TotalWeight += StorageWeight + RamWeight;
        }
        public (double, double) getStorageRankings()
        {
            return StorageRanking;
        }
        public (double, double) getRamRankings()
        {
            return RamRanking;
        }
    }
}

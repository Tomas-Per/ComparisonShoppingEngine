using ItemLibrary;
using System;
using System.Collections.Generic;

namespace Comparison.Comparisons
{
    public class ComputerComparison : ItemComparison<Computer>
    {
        public int StorageWeight { get; protected set; }
        public int RamWeight { get; protected set; }

        public ComputerComparison(int priceWeight, int storageWeight, int ramWeight)
                                    : base(priceWeight)
        {
            UpdateWeights(priceWeight, storageWeight, ramWeight);
        }
        //Compares items storage by given items storage
        public (double, double) StorageComparison (int mainStorage, int comparingStorage)
        {
            var storageRanking = SpecComparison(Convert.ToDouble(mainStorage), Convert.ToDouble(comparingStorage), StorageWeight);
            ItemRanking = (ItemRanking.Item1 + storageRanking.Item1, ItemRanking.Item2 + storageRanking.Item2);
            return storageRanking;
        }
        //Compares items RAMs by given items RAMs
        public (double, double) RamComparison(int mainRAM, int comparingRAM)
        {
            var ramRanking = SpecComparison(Convert.ToDouble(mainRAM), Convert.ToDouble(comparingRAM), RamWeight);
            ItemRanking = (ItemRanking.Item1 + ramRanking.Item1, ItemRanking.Item2 + ramRanking.Item2);
            return ramRanking;
        }
        public void UpdateRatings(Computer mainItem, Computer comparingItem, Action<(double, double)> priceRanking,
                                  Action<(double, double)> storageRanking, Action<(double, double)> ramRanking,
                                                                             Action<(double, double)> itemRanking)
        //Compares two given computers by preferences
        {
            ItemRanking = (0, 0);
            priceRanking(PriceComparison(mainItem.Price, comparingItem.Price));
            storageRanking(StorageComparison(mainItem.StorageCapacity, comparingItem.StorageCapacity));
            ramRanking(RamComparison(mainItem.RAM, comparingItem.RAM));
            itemRanking(ItemRanking);
        }
        //Updates weights if new were given
        public void UpdateWeights(int priceWeight, int storageWeight, int ramWeight)
        {
            UpdateWeights(priceWeight);
            StorageWeight = storageWeight;
            RamWeight = ramWeight;
            TotalWeight += StorageWeight + RamWeight;
        }
        public static void Main()
        {
            var a = new Computer { Price = 15, RAM = 16, StorageCapacity = 256};
            var b = new Computer { Price = 10, RAM = 8, StorageCapacity = 128 };

            var comparison = new ComputerComparison(5, 5, 5);
            comparison.UpdateRatings(a, b, (c) => Console.WriteLine(c), (c) => Console.WriteLine(c), (c) => Console.WriteLine(c),
                                            (c) => Console.WriteLine(c)) ;
        }
    }
}

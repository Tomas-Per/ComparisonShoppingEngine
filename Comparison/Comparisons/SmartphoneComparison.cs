using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;

namespace Comparison.Comparisons
{
    class SmartphoneComparison : ItemComparison<Smartphone>
    {
        public int RamWeight { get; protected set; }
        public int StorageWeight { get; protected set; }
        public int CameraWeight { get; protected set; }

        public SmartphoneComparison (int priceWeight, int ramWeight, int storageWeight, int cameraWeight)
                                        : base(priceWeight)
        {
            UpdateWeights(priceWeight, ramWeight, storageWeight, cameraWeight);
        }

        //Compares items by RAM amount
        public (double, double) RamComparison(int mainRAM, int comparingRAM)
        {
            var ramRanking = SpecComparison(Convert.ToDouble(mainRAM), Convert.ToDouble(comparingRAM), RamWeight);
            ItemRanking = (ItemRanking.Item1 + ramRanking.Item1, ItemRanking.Item2 + ramRanking.Item2);
            return ramRanking;
        }

        //Compares items by storage amount
        public (double, double) StorageComparison(int mainStorage, int comparingStorage)
        {
            var storageRanking = SpecComparison(Convert.ToDouble(mainStorage), Convert.ToDouble(comparingStorage), StorageWeight);
            ItemRanking = (ItemRanking.Item1 + storageRanking.Item1, ItemRanking.Item2 + storageRanking.Item2);
            return storageRanking;
        }

        //Compares items by maximum camera megapixel amount
        public (double, double) CameraComparison(List<int> mainCamera, List<int> comparingCamera)
        {
            var cameraRanking = SpecComparison(Convert.ToDouble(mainCamera.Max()), Convert.ToDouble(comparingCamera.Max()), CameraWeight);
            ItemRanking = (ItemRanking.Item1 + cameraRanking.Item1, ItemRanking.Item2 + cameraRanking.Item2);
            return cameraRanking;
        }

        //Compares two smartphones by preference weights
        public void UpdateRatings(Smartphone mainItem, Smartphone comparingItem, Action<(double, double)> priceRanking,
                                  Action<(double, double)> storageRanking, Action<(double, double)> ramRanking,
                                  Action<(double, double)> cameraRanking,  Action<(double, double)> itemRanking)
        {
            ItemRanking = (0, 0);
            priceRanking(PriceComparison(mainItem.Price, comparingItem.Price));
            ramRanking(RamComparison(mainItem.RAM, comparingItem.RAM));
            storageRanking(StorageComparison(mainItem.Storage, comparingItem.Storage));
            cameraRanking(CameraComparison(mainItem.BackCameraMP, comparingItem.BackCameraMP));
            itemRanking(ItemRanking);
        }

        //Updates weights if new were given
        public void UpdateWeights(int priceWeight, int ramWeight, int storageWeight, int cameraWeight)
        {
            UpdateWeights(priceWeight);
            RamWeight = ramWeight;
            StorageWeight = storageWeight;
            CameraWeight = cameraWeight;
            TotalWeight += RamWeight + StorageWeight + CameraWeight;
        }
    }
}

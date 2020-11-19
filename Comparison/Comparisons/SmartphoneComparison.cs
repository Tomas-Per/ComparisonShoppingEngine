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
        protected (double, double) RamRanking { get; set; }
        protected (double, double) StorageRanking { get; set; }
        protected (double, double) CameraRanking { get; set; }

        public SmartphoneComparison (int priceWeight, int ramWeight, int storageWeight, int cameraWeight)
                                        : base(priceWeight)
        {
            UpdateWeights(priceWeight, ramWeight, storageWeight, cameraWeight);
        }

        //Compares items by RAM amount
        public void RamComparison(int mainRAM, int comparingRAM)
        {
            RamRanking = SpecComparison(Convert.ToDouble(mainRAM), Convert.ToDouble(comparingRAM), RamWeight);
            ItemRanking = (ItemRanking.Item1 + RamRanking.Item1, ItemRanking.Item2 + RamRanking.Item2);
        }

        //Compares items by storage amount
        public void StorageComparison(int mainStorage, int comparingStorage)
        {
            StorageRanking = SpecComparison(Convert.ToDouble(mainStorage), Convert.ToDouble(comparingStorage), StorageWeight);
            ItemRanking = (ItemRanking.Item1 + StorageRanking.Item1, ItemRanking.Item2 + StorageRanking.Item2);
        }

        //Compares items by maximum camera megapixel amount
        public void CameraComparison(List<int> mainCamera, List<int> comparingCamera)
        {
            CameraRanking = SpecComparison(Convert.ToDouble(mainCamera.Max()), Convert.ToDouble(comparingCamera.Max()), CameraWeight);
            ItemRanking = (ItemRanking.Item1 + RamRanking.Item1, ItemRanking.Item2 + RamRanking.Item2);
        }

        //Compares two smartphones by preference weights
        public override void UpdateRatings(Smartphone mainItem, Smartphone comparingItem)
        {
            ItemRanking = (0, 0);
            PriceComparison(mainItem.Price, comparingItem.Price);
            RamComparison(mainItem.RAM, comparingItem.RAM);
            StorageComparison(mainItem.Storage, comparingItem.Storage);
            CameraComparison(mainItem.BackCameraMP, comparingItem.BackCameraMP);
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

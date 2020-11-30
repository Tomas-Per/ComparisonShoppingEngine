using ItemLibrary;
using System;
using System.Collections.Generic;

namespace Comparison
{
    public class ItemComparison<T> where T : Item
    {
        public int PriceWeight { get; protected set; }
        protected (double, double) PriceRanking { get; set; }
        protected (double, double) ItemRanking {get; set;}
        protected int TotalWeight { get; set; }
        protected (double, double) SpecRanking { get; set; }

        public ItemComparison(int priceWeight)
        {
            UpdateWeights(priceWeight);
            ItemRanking = (0,0);
        }

        //Compares items by given weights and returns proportion
        protected (double, double) SpecComparison(double mainSpec, double comparingSpec, int specWeight)
        {
            double _sum = mainSpec + comparingSpec;
            double _mainRating = (((double)specWeight / TotalWeight) * 100 * mainSpec) / _sum;
            double _comparingRating = (((double)specWeight / TotalWeight) * 100 * comparingSpec) / _sum;
            return (_mainRating, _comparingRating);
        }


        //Compares price by given items prices
        protected (double, double) PriceComparison(double mainPrice, double comparingPrice)
        {
            var priceRanking = SpecComparison(mainPrice, comparingPrice, PriceWeight);
            ItemRanking = (ItemRanking.Item1 + priceRanking.Item2, ItemRanking.Item2 + priceRanking.Item1);
            return priceRanking;
        }
        public virtual void UpdateRatings(T mainItem, T comparingItem, Action<(double, double)> priceRanking,
                                  Action<(double, double)> storageRanking, Action<(double, double)> ramRanking,
                                    Action<(double, double)> itemRanking)
        {
            ItemRanking = (0, 0);
            priceRanking(PriceComparison(mainItem.Price, comparingItem.Price));
            //cameraRanking(CameraComparison(mainItem.BackCameraMP, comparingItem.BackCameraMP));
            itemRanking(ItemRanking);
        }

        //Updates weights if new were given
        public void UpdateWeights(int priceWeight)
        {
            PriceWeight = priceWeight;
            TotalWeight = priceWeight;
        }
        
    }
}

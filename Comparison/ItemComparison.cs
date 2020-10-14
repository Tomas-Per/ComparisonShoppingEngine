using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Comparison
{
    class ItemComparison
    {
        protected int PriceWeight { get; set; }
        public double MainItemRanking {get; set;}
        public double ComparingItemRanking { get; set; }
        public int TotalWeight { get; set; }

        public ItemComparison(int priceWeight)
        {
            PriceWeight = priceWeight;
            MainItemRanking = 0;
            ComparingItemRanking = 0;
            TotalWeight = PriceWeight;
        }
        public (double, double) SpecComparison(double mainSpec, double comparingSpec, int specWeight)
        {
            double _sum = mainSpec + comparingSpec;
            double _mainRating = ((specWeight / TotalWeight) * 100 * mainSpec) / _sum;
            double _comparingRating = ((specWeight / TotalWeight) * 100 * comparingSpec) / _sum;
            MainItemRanking += _mainRating;
            ComparingItemRanking += _comparingRating;
            return (_mainRating, _comparingRating);
        }

        public (double, double) PriceComparison(double mainPrice, double comparingPrice)
        {
            return SpecComparison(mainPrice, comparingPrice, PriceWeight);
        }
        //might delete this if not needed in the future:
        public void ObjectComparison(Item mainItem, Item comparingItem)
        {
            PriceComparison(mainItem.Price, comparingItem.Price);
        }
    }
}

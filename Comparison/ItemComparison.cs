using ItemLibrary;

namespace Comparison
{
    class ItemComparison<T> where T : Item
    { 
        protected int PriceWeight { get; set; }
        protected (double, double) ItemRanking {get; set;}
        protected int TotalWeight { get; set; }

        public ItemComparison(int priceWeight)
        {
            PriceWeight = priceWeight;
            ItemRanking = (0,0);
            TotalWeight = PriceWeight;
        }
        protected (double, double) SpecComparison(double mainSpec, double comparingSpec, int specWeight)
        {
            double _sum = mainSpec + comparingSpec;
            double _mainRating = ((specWeight / TotalWeight) * 100 * mainSpec) / _sum;
            double _comparingRating = ((specWeight / TotalWeight) * 100 * comparingSpec) / _sum;
            ItemRanking = (ItemRanking.Item1 + _mainRating, ItemRanking.Item2 + _comparingRating);
            return (_mainRating, _comparingRating);
        }

        public (double, double) PriceComparison(double mainPrice, double comparingPrice)
        {
            return SpecComparison(mainPrice, comparingPrice, PriceWeight);
        }
        public virtual (double, double) SumAllRankings(T mainItem, T comparingItem)
        {
            PriceComparison(mainItem.Price, comparingItem.Price);
            return ItemRanking;
        }
    }
}

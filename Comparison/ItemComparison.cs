using ItemLibrary;

namespace Comparison
{
    public class ItemComparison<T> where T : Item
    { 
        public int PriceWeight { get; protected set; }
        protected (double, double) PriceRanking { get; set; }
        protected (double, double) ItemRanking {get; set;}
        protected int TotalWeight { get; set; }

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
        protected void PriceComparison(double mainPrice, double comparingPrice)
        {
            PriceRanking = SpecComparison(mainPrice, comparingPrice, PriceWeight);
            ItemRanking = (ItemRanking.Item1 + PriceRanking.Item2, ItemRanking.Item2 + PriceRanking.Item1);
        }
        //Compares 2 given items
        public virtual void UpdateRatings(T mainItem, T comparingItem)
        {
            ItemRanking = (0,0);
            PriceComparison(mainItem.Price, comparingItem.Price);
        }
        //Updates weights if new were given
        public void UpdateWeights(int priceWeight)
        {
            PriceWeight = priceWeight;
            TotalWeight = priceWeight;
        }
        public (double, double) GetPriceRankings()
        {
            return PriceRanking;
        }
        public (double, double) GetItemRankings()
        {
            return ItemRanking;
        }
    }
}

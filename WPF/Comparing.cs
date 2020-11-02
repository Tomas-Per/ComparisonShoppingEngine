using Comparison.Comparisons;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace WPF
{
    public partial class MainWindow
    {
        (double first, double second) ItemRating;

        private Computer ComparingItem1 = null;
        private Computer ComparingItem2 = null;

        private ComputerComparison Comparison = new ComputerComparison(5, 5, 5);

        private void PriceWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(ComparingItem1 != null && ComparingItem2 != null)
            {
                Comparison.PriceWeight = (int)PriceWeightSlider.Value;
                ItemRating = Comparison.SumAllRankings(ComparingItem1, ComparingItem2);
                ComparisonProductRating1.Text = (ItemRating.first).ToString();
                ComparisonProductRating2.Text = (ItemRating.second).ToString();
            }
            

        }

        private void StorageWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ComparingItem1 != null && ComparingItem2 != null)
            {
                Comparison.StorageWeight = (int)StorageWeightSlider.Value;
                ItemRating = Comparison.SumAllRankings(ComparingItem1, ComparingItem2);
                ComparisonProductRating1.Text = (ItemRating.first).ToString();
                ComparisonProductRating2.Text = (ItemRating.second).ToString();
            }

        }

        private void RAMWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ComparingItem1 != null && ComparingItem2 != null)
            {
                Comparison.RamWeight = (int)RAMWeightSlider.Value;
                ItemRating = Comparison.SumAllRankings(ComparingItem1, ComparingItem2);
                ComparisonProductRating1.Text = (ItemRating.first).ToString();
                ComparisonProductRating2.Text = (ItemRating.second).ToString();
            }


        }
    }
}

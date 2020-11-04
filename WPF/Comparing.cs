﻿using Comparison.Comparisons;
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

        private Computer ComparingItem1;
        private Computer ComparingItem2;

        private ComputerComparison _Comparison = new ComputerComparison(5,5,5);

        private void DisplayRatings()
        {
            ComparisonProductRating1.Text = (ItemRating.first).ToString("F2");
            ComparisonProductRating2.Text = (ItemRating.second).ToString("F2");
        }
        private void PriceWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(ComparingItem1 != null && ComparingItem2 != null)
            {
                _Comparison.UpdateWeights((int)PriceWeightSlider.Value, _Comparison.StorageWeight, _Comparison.RamWeight);
                _Comparison.UpdateRatings(ComparingItem1, ComparingItem2);
                ItemRating = _Comparison.GetItemRankings();
                DisplayRatings();
            }
     
        }

        private void StorageWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ComparingItem1 != null && ComparingItem2 != null)
            {
                _Comparison.UpdateWeights(_Comparison.PriceWeight, (int)StorageWeightSlider.Value, _Comparison.RamWeight);
                _Comparison.UpdateRatings(ComparingItem1, ComparingItem2);
                ItemRating = _Comparison.GetItemRankings();
                DisplayRatings();
            }

        }

        private void RAMWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ComparingItem1 != null && ComparingItem2 != null)
            {
                _Comparison.UpdateWeights(_Comparison.PriceWeight, _Comparison.StorageWeight, (int)RAMWeightSlider.Value);
                _Comparison.UpdateRatings(ComparingItem1, ComparingItem2);
                ItemRating = _Comparison.GetItemRankings();
                DisplayRatings();
            }


        }
    }
}
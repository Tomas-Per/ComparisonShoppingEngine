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
        private (double first, double second) _itemRating;

        private Computer _comparingItem1;
        private Computer _comparingItem2;

        private ComputerComparison _comparison = new ComputerComparison(5,5,5);

        private void DisplayRatings()
        {
            _comparison.UpdateRatings(_comparingItem1, _comparingItem2);
            _itemRating = _comparison.GetItemRankings();
            
            if(Double.IsNaN(_itemRating.first) || Double.IsNaN(_itemRating.second))
            {
                ComparisonProductRating1.Text = "No preferences selected";
                ComparisonProductRating2.Text = "No preferences selected";
            }
            else
            {
                ComparisonProductRating1.Text = (_itemRating.first).ToString("F2");
                ComparisonProductRating2.Text = (_itemRating.second).ToString("F2");
            }
            
        }
        private void UpdateComparison()
        {
            if(_comparingItem1 != null && _comparingItem2 != null)
            {
                DisplayRatings();
            }
        }
        private void PriceWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(_comparingItem1 != null && _comparingItem2 != null)
            {
                _comparison.UpdateWeights((int)PriceWeightSlider.Value, _comparison.StorageWeight, _comparison.RamWeight);
                DisplayRatings();
            }
     
        }

        private void StorageWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_comparingItem1 != null && _comparingItem2 != null)
            {
                _comparison.UpdateWeights(_comparison.PriceWeight, (int)StorageWeightSlider.Value, _comparison.RamWeight);
                DisplayRatings();
            }

        }

        private void RAMWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_comparingItem1 != null && _comparingItem2 != null)
            {
                _comparison.UpdateWeights(_comparison.PriceWeight, _comparison.StorageWeight, (int)RAMWeightSlider.Value);
                DisplayRatings();
            }


        }
    }
}

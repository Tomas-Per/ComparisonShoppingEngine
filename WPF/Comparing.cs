using Comparison;
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

        private Computer _comparingItem1;
        private Computer _comparingItem2;
        private Smartphone _comparing2Item1;
        private Smartphone _comparing2Item2;

        private ComputerComparison _comparison = new ComputerComparison(5,5,5);
        private SmartphoneComparison _comparison2 = new SmartphoneComparison(5, 5, 5, 0);

        private void DisplayRatings()
        {
            if (Type == typeof(Computer))
            {
                _comparison.UpdateRatings(_comparingItem1, _comparingItem2,
                                    (ranking) => { }, (ranking) => { }, (ranking) => { },
                                            (itemRanking) =>
                                            {
                                                if (Double.IsNaN(itemRanking.Item1) || Double.IsNaN(itemRanking.Item2))
                                                {
                                                    ComparisonProductRating1.Text = "No preferences selected";
                                                    ComparisonProductRating2.Text = "No preferences selected";
                                                }
                                                else
                                                {
                                                    ComparisonProductRating1.Text = (itemRanking.Item1).ToString("F2");
                                                    ComparisonProductRating2.Text = (itemRanking.Item2).ToString("F2");
                                                }
                                            });
            }
            else if(Type == typeof(Smartphone))
            {
                _comparison2.UpdateRatings(_comparing2Item1, _comparing2Item2,
                                   (ranking) => { }, (ranking) => { }, (ranking) => { },
                                           (itemRanking) =>
                                           {
                                               if (Double.IsNaN(itemRanking.Item1) || Double.IsNaN(itemRanking.Item2))
                                               {
                                                   ComparisonProductRating1.Text = "No preferences selected";
                                                   ComparisonProductRating2.Text = "No preferences selected";
                                               }
                                               else
                                               {
                                                   ComparisonProductRating1.Text = (itemRanking.Item1).ToString("F2");
                                                   ComparisonProductRating2.Text = (itemRanking.Item2).ToString("F2");
                                               }
                                           });
            }
            
        }
        private void UpdateComparison()
        {
            if((_comparingItem1 != null && _comparingItem2 != null) || (_comparing2Item1 != null && _comparing2Item2!=null))
            {
                DisplayRatings();
            }
        }
        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if((_comparingItem1 != null && _comparingItem2 != null) || (_comparing2Item1 != null && _comparing2Item2 != null))
            {
                _comparison.UpdateWeights((int)Slider1.Value, _comparison.StorageWeight, _comparison.RamWeight);
                _comparison2.UpdateWeights(_comparison.PriceWeight, _comparison.StorageWeight, (int)Slider3.Value, 0);
                DisplayRatings();
            }
     
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if((_comparingItem1 != null && _comparingItem2 != null) || (_comparing2Item1 != null && _comparing2Item2 != null))
            {
                _comparison.UpdateWeights(_comparison.PriceWeight, (int)Slider2.Value, _comparison.RamWeight);
                _comparison2.UpdateWeights(_comparison.PriceWeight, _comparison.StorageWeight, (int)Slider3.Value, 0);
                DisplayRatings();
            }

        }

        private void Slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if ((_comparingItem1 != null && _comparingItem2 != null) || (_comparing2Item1 != null && _comparing2Item2 != null))
            {
                _comparison.UpdateWeights(_comparison.PriceWeight, _comparison.StorageWeight, (int)Slider3.Value);
                _comparison2.UpdateWeights(_comparison.PriceWeight, _comparison.StorageWeight, (int)Slider3.Value, 0);
                DisplayRatings();
            }
        }
        private void ResetComparison()
        {
            ComparisonProductName1.Text = String.Empty;
            ComparisonProductPrice1.Text = String.Empty;
            ComparisonProductBrand1.Text = String.Empty;
            ComparisonProductProcessor1.Text = String.Empty;
            ComparisonProductRAM1.Text = String.Empty;
            ComparisonProductName2.Text = String.Empty;
            ComparisonProductPrice2.Text = String.Empty;
            ComparisonProductBrand2.Text = String.Empty;
            ComparisonProductProcessor2.Text = String.Empty;
            ComparisonProductRAM2.Text = String.Empty;
            ComparisonProductRating1.Text = String.Empty;
            ComparisonProductRating2.Text = String.Empty;
            _comparingItem1 = null;
            _comparingItem2 = null;
            _comparing2Item1 = null;
            _comparing2Item2 = null;
            ComparisonProduct1Custom1.Text = String.Empty;
            ComparisonProduct1Custom2.Text = String.Empty;
            ComparisonProduct1Custom3.Text = String.Empty;
            ComparisonProduct2Custom1.Text = String.Empty;
            ComparisonProduct2Custom2.Text = String.Empty;
            ComparisonProduct2Custom3.Text = String.Empty;

        }
    }
}

using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class MainWindow
    {
        //these two lists are temporary
        private List<string> Brands = new List<string>() { "Asus", "Dell", "Apple", "Lenovo", "Acer", "Huawei" };
        private List<string> Processors = new List<string>() { "Intel Core i3", "Intel Core i5", "Intel Core i7", "IntelCeleron", "Intel Atom" };
        private List<CheckBox> ProcessorsCheckBoxes = new List<CheckBox>();
        private List<CheckBox> BrandsCheckBoxes = new List<CheckBox>();

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            List<Computer> List1 = new List<Computer>();
            int MaxRange = (int)PriceSlider.Value;
            var List = OriginalList;
            bool isThereCheckedBox = false;
            if (MaxRange != 0)
            {

                //Filtering by price range
                List = _filter.FilterByPrice(0, MaxRange);

                //updating the list inside the class so we can filter out the list 
                //*which already has been filtered by price
                //
                _filter.UpdateList(List);
            }
            //checking every checkbox and if is checked, we use filter
            foreach (var checkBox in BrandsCheckBoxes)
            {
                if ((bool)checkBox.IsChecked)
                {
                    List1.AddRange(_filter.FilterByManufacturer(checkBox.Name));
                    isThereCheckedBox = true;
                }
            }
            foreach (var checkBox in ProcessorsCheckBoxes)
            {
                if ((bool)checkBox.IsChecked)
                {
                    List1.AddRange(_filter.FilterByProcessor(checkBox.Content.ToString()));
                    isThereCheckedBox = true;
                }
            }
            //we check, if there wasn't any checked checkboxes
            if (isThereCheckedBox == false) List1 = List;
            ItemsListBox.ItemsSource = List1;
            _filter.UpdateList(OriginalList);

            ListNameTextBlock.Text = "Filtered List";
        }

        private void DisableFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsListBox.ItemsSource = OriginalList;

            //Setting all checkboxes to be unchecked
            foreach (var checkbox in BrandsCheckBoxes)
            {
                checkbox.IsChecked = false;
            }
            foreach (var checkbox in ProcessorsCheckBoxes)
            {
                checkbox.IsChecked = false;
            }
            ListNameTextBlock.Text = "All Computers";

        }
    }
}

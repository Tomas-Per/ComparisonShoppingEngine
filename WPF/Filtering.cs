using DataManipulation.Filters;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF
{
    public partial class MainWindow
    {

        private ComputerFilter _filter;
        //these two lists are temporary
        private List<string> Brands = new List<string>() { "Asus", "Dell", "Apple", "Lenovo", "Acer", "Huawei" };
        private List<string> Processors = new List<string>() { "Intel Core i3", "Intel Core i5", "Intel Core i7", "IntelCeleron", "Intel Atom" };
        private List<CheckBox> ProcessorsCheckBoxes = new List<CheckBox>();
        private List<CheckBox> BrandsCheckBoxes = new List<CheckBox>();

        private void FilterList()
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

        private void DisableFilters()
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
        private void CreateFilterCheckbox()
        {
            DynamicFilterCheckBox(Brands, BrandsCheckBoxes, BrandColumn1, BrandColumn2, BrandColumn3, BrandColumn4);
            DynamicFilterCheckBox(Processors, ProcessorsCheckBoxes, ProcessorColumn1, ProcessorColumn2, ProcessorColumn3, ProcessorColumn4);
        }
        private void DynamicFilterCheckBox(List<String> filterSpecs,List<CheckBox> checkBoxes,
                                            StackPanel column1, StackPanel column2, StackPanel column3, StackPanel column4)
        {
            int _column = 1;
            int _cycleCount = 1;
            foreach (var filterSpec in filterSpecs)
            {
                CheckBox _checkbox = new CheckBox()
                {
                    Content = filterSpec,
                    Name = filterSpec.Replace(" ", ""),
                    Style =this.Resources["FilterCheckbox"] as Style
                };
                checkBoxes.Add(_checkbox);
                switch (_column)
                {
                    case 1:
                        column1.Children.Add(_checkbox);
                        _column = 2;
                        break;
                    case 2:
                        column2.Children.Add(_checkbox);
                        _column = 1;
                        break;
                    case 3:
                        column3.Children.Add(_checkbox);
                        _column = 4;
                        break;
                    case 4:
                        column4.Children.Add(_checkbox);
                        _column = 3;
                        break;
                }
                _cycleCount++;
                if (_cycleCount == 7) _column = 3;
            }
        }
    }
}

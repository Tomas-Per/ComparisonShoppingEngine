using DataContent.ReadingCSV.Services;
using DataManipulation.Filters;
using ItemLibrary;
using PathLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Parsing;

namespace WPF
{
    public partial class MainWindow
    {

        private ComputerFilter _filter;
        //these two lists are temporary
        private List<FilterSpec> _brands;
        private List<FilterSpec> _processors;
        private List<CheckBox> _processorsCheckBoxes = new List<CheckBox>();
        private List<CheckBox> _brandsCheckBoxes = new List<CheckBox>();

        private void FilterList()
        {
            List<Computer> List1 = new List<Computer>();
            int MaxRange = (int)PriceSlider.Value;
            var List = OriginalList.Cast<Computer>().ToList();
            bool isThereCheckedBox = false;
            if (MaxRange != 0)
            {

                //Filtering by price range
                _filter.FilterByPrice(0, MaxRange, List, (list) => {List = list;});

            }
            //checking every checkbox and if is checked, we use filter
            foreach (var checkBox in _brandsCheckBoxes)
            {
                if ((bool)checkBox.IsChecked)
                {
                    _filter.FilterByManufacturer(checkBox.Content.ToString(), List,  (l) => { List1.AddRange(l); });
                    isThereCheckedBox = true;
                }
            }
            foreach (var checkBox in _processorsCheckBoxes)
            {
                if ((bool)checkBox.IsChecked)
                {
                    _filter.FilterByProcessor(checkBox.Content.ToString(), List, (l) => { List1.AddRange(l); });
                    isThereCheckedBox = true;
                }
            }
            //we check, if there wasn't any checked checkboxes
            if (isThereCheckedBox == false) List1 = List;
            ItemsListBox.ItemsSource = List1;

            ListNameTextBlock.Text = "Filtered List";
        }

        private void DisableFilters()
        {
            ItemsListBox.ItemsSource = OriginalList;

            //Setting all checkboxes to be unchecked
            foreach (var checkbox in _brandsCheckBoxes)
            {
                checkbox.IsChecked = false;
            }
            foreach (var checkbox in _processorsCheckBoxes)
            {
                checkbox.IsChecked = false;
            }
            ListNameTextBlock.Text = "All Computers";

        }
        private void CreateFilterCheckbox()
        {
            //reades filter specs from data and add to checkboxes
            var _filterService = new FiltersServiceCSV(MainPath.GetBrandPath());
            _brands = _filterService.ReadData().ToList();
            _filterService = new FiltersServiceCSV(MainPath.GetProcessorPath());
            _processors = _filterService.ReadData().ToList();
            //adds checkboxes
            DynamicFilterCheckBox(_brands, _brandsCheckBoxes, BrandColumn1, BrandColumn2, BrandColumn3, BrandColumn4);
            DynamicFilterCheckBox(_processors, _processorsCheckBoxes, ProcessorColumn1, ProcessorColumn2, ProcessorColumn3, ProcessorColumn4);
        }
        private void DynamicFilterCheckBox(List<FilterSpec> filterSpecs,List<CheckBox> checkBoxes,
                                            StackPanel column1, StackPanel column2, StackPanel column3, StackPanel column4)
        {
            int _column = 1;
            int _cycleCount = 1;
            //creates checkbox for every filter
            foreach (var filterSpec in filterSpecs)
            {
                CheckBox _checkbox = new CheckBox()
                {
                    Content = filterSpec.Name.ToString().DeleteSpecialChars(),
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
                //add items to invisible columns
                if (_cycleCount == 7) _column = 3;
            }
        }
    }
}

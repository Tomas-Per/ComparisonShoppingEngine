using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using DataContent.ReadingCSV.Services;
using ItemLibrary;
using DataContent;
using System.IO;
using System.Diagnostics;
using DataManipulation;
using DataContent.ReadingCSV;
using System.Xml;
using System.Linq.Expressions;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Filter<Item> _filter;
        private Sorter _sorter;
        private List<Item> OriginalList = new List<Item>();
        private List<string> brands = new List<string>() { "Asus", "Dell", "Apple", "Lenovo", "Acer", "Huawei" };
        private List<string> processors = new List<string>() { "Intel Core i3", "Intel Core i5", "Intel Core i7", "IntelCeleron", "Intel Atom" };
        private List<CheckBox> processorsCheckBoxes = new List<CheckBox>();
        private List<CheckBox> brandsCheckBoxes = new List<CheckBox>();
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation =
                WindowStartupLocation.CenterScreen;
        }

        private void PriceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PriceSlider.Value != 0)
            {
                txtSliderValue.Text = "Price up to: " + '€' + PriceSlider.Value.ToString();
            }
            else txtSliderValue.Text = null;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DynamicBrandCheckBox();
            dynamicProcessorCheckBox();
            string _filePath = MainPath.GetMainPath() + @"\Data\senukai.csv";

            var _laptopService = new LaptopServiceCSV();

            //Here We are calling function to read CSV file
            var resultData = _laptopService.ReadData(_filePath);
            ItemsListBox.ItemsSource = resultData;
            
            OriginalList = resultData.Cast<Item>().ToList();
            _filter = new Filter<Item>(OriginalList);
            _sorter = new Sorter(OriginalList);
        }

        private void dynamicProcessorCheckBox()
        {
            int column = 1;
            int cycleCount = 1;
            foreach (var processor in processors)
            {
                CheckBox checkbox = new CheckBox()
                {
                    Content = processor,
                    Name = processor.Replace(" ", ""),
                    FontFamily = new FontFamily("Candara Light"),
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    Foreground = Brushes.White
                };
                brandsCheckBoxes.Add(checkbox);
                switch (column)
                {
                    case 1:
                        ProcessorColumn1.Children.Add(checkbox);
                        column = 2;
                        break;
                    case 2:
                        ProcessorColumn2.Children.Add(checkbox);
                        column = 1;
                        break;
                    case 3:
                        ProcessorColumn3.Children.Add(checkbox);
                        column = 4;
                        break;
                    case 4:
                        ProcessorColumn4.Children.Add(checkbox);
                        column = 3;
                        break;
                }
                cycleCount++;
                if (cycleCount == 7) column = 3;
            }
        }

        private void DynamicBrandCheckBox()
        {
            int column = 1;
            int cycleCount = 1;
            foreach(var brand in brands)
            {
                CheckBox checkbox = new CheckBox()
                {
                    Content = brand,
                    Name = brand,
                    FontFamily = new FontFamily("Candara Light"),
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    Foreground = Brushes.White
                };
                brandsCheckBoxes.Add(checkbox);
                switch(column)
                {
                    case 1:
                        BrandColumn1.Children.Add(checkbox);
                        column = 2;
                        break;
                    case 2:
                        BrandColumn2.Children.Add(checkbox);
                        column = 1;
                        break;
                    case 3:
                        BrandColumn3.Children.Add(checkbox);
                        column = 4;
                        break;
                    case 4:
                        BrandColumn4.Children.Add(checkbox);
                        column = 3;
                        break;
                }
                cycleCount++;
                if (cycleCount == 7) column = 3;
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            List<Item> List1 = new List<Item>();
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
            foreach(CheckBox checkBox in brandsCheckBoxes)
            {
                if ((bool)checkBox.IsChecked) List1.AddRange(_filter.FilterByManufacturer(checkBox.Name));
                isThereCheckedBox = true;
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
            foreach(var checkbox in brandsCheckBoxes)
            {
                checkbox.IsChecked = false;
            }
            foreach(var checkbox in processorsCheckBoxes)
            {
                checkbox.IsChecked = false;
            }    
            ListNameTextBlock.Text = "All Computers";
            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (ItemsListBox.SelectedIndex == -1) return;

            Computer item = (sender as ListBox).SelectedItem as Computer;

            //Setting textboxes
            ProductName.Text = item.Name;
            ProductPrice.Text = "Price: " + '€' + (item.Price).ToString();
            ProductBrand.Text = "Brand: " + item.ManufacturerName;
            ProductProcessor.Text = "Processor: " + item.ProcessorName;
            ProductRAM.Text = "RAM: " + (item.RAM).ToString() + "GB " + item.RAM_type;
            ProductGraphicsCard.Text = "Graphics Card: " + item.GraphicsCardName + ' ' + item.GraphicsCardMemory;
            ProductResolution.Text = "Resolution: " + item.Resolution;
            ProductStorage.Text = "Storage Capacity: " + (item.StorageCapacity).ToString() + "GB";
            BuyHere.Text = "Buy here";
            SimilarProducts.Text = "Similar Products";
            CompareButton.Visibility = Visibility.Visible;

            Uri uri = new Uri(item.ItemURL);
            BuyHereHyper.NavigateUri = uri;

            List<Item> SimilarItems = item.FindSimilar(OriginalList);
            SimilarItemsListBox.ItemsSource = SimilarItems;

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Hyperlink hl = (Hyperlink)sender;
            string navigateUri = hl.NavigateUri.ToString();
            
            var StartInfo = new ProcessStartInfo
            {
                FileName = navigateUri,
                UseShellExecute = true
            };
            Process.Start(StartInfo);
            
            e.Handled = true;
        }

        private void SimilarListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (SimilarItemsListBox.SelectedIndex == -1) return;

            //Setting textboxes
            Computer item = (sender as ListBox).SelectedItem as Computer;
            ProductName.Text = item.Name;
            ProductPrice.Text = '€' + (item.Price).ToString();
            ProductBrand.Text = item.ManufacturerName;
            ProductProcessor.Text = item.ProcessorName;
            ProductRAM.Text = (item.RAM).ToString() + "GB " + item.RAM_type;
            ProductGraphicsCard.Text = item.GraphicsCardName + ' ' + item.GraphicsCardMemory;
            ProductResolution.Text = item.Resolution;
            ProductStorage.Text = (item.StorageCapacity).ToString() + "GB";
            BuyHere.Text = "Buy here";
            SimilarProducts.Text = "Similar Products";

            Uri uri = new Uri(item.ItemURL);
            BuyHereHyper.NavigateUri = uri;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string SearchTerm = SearchedItemText.Text;
            if(String.IsNullOrEmpty(SearchedItemText.Text))
            {
                ItemsListBox.ItemsSource = OriginalList;
            }
            else
            {
                List<Item> List = ItemsListBox.ItemsSource.Cast<Item>().ToList();
                List<Item> result = List.Where(x => x.Name.Contains(SearchTerm)).ToList();
                ItemsListBox.ItemsSource = result;
            }
            
        }

        private void SearchedItemText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComparisonOff_Click(object sender, RoutedEventArgs e)
        {
            ItemInfoStackPanel.Visibility = Visibility.Visible;
            ListStackPanel.Visibility = Visibility.Visible;
            ComparisonGrid.Visibility = Visibility.Collapsed;
        }

        private void CompareButton_Click(object sender, RoutedEventArgs e)
        {
            ComparisonProductName1.Text = ProductName.Text;
            ComparisonProductPrice1.Text = ProductPrice.Text;
            ComparisonProductBrand1.Text = ProductBrand.Text;
            ComparisonProductProcessor1.Text = ProductProcessor.Text;
            ComparisonProductRAM1.Text = ProductRAM.Text;
            ComparisonProductGraphicsCard1.Text = ProductGraphicsCard.Text;
            ComparisonProductResolution1.Text = ProductResolution.Text;
            ComparisonProductStorage1.Text = ProductStorage.Text;

            ComparisonGrid.Visibility = Visibility.Visible;
            ItemInfoStackPanel.Visibility = Visibility.Collapsed;
            ListStackPanel.Visibility = Visibility.Collapsed;
        }

        private void AZSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByNameAsc();

            ItemsListBox.ItemsSource = items;
        }

        private void ZASortButton_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByNameDesc();

            ItemsListBox.ItemsSource = items;
        }

        private void PriceAscSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByPriceAsc();

            ItemsListBox.ItemsSource = items;
        }

        private void PriceDescSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByPriceDesc();

            ItemsListBox.ItemsSource = items;
        }

    }
}

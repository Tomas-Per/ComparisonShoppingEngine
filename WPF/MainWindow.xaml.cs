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
using DataManipulation.Filters;
using System.Xml;
using System.Linq.Expressions;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Computer> OriginalList = new List<Computer>();

        private List<CheckBox> ProcessorsCheckBoxes = new List<CheckBox>();
        private List<CheckBox> BrandsCheckBoxes = new List<CheckBox>();

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
            DynamicProcessorCheckBox();
            string _filePath = MainPath.GetMainPath() + @"\Data\senukai.csv";

            var _laptopService = new LaptopServiceCSV(MainPath.GetComputerPath());

            //Here We are calling function to read CSV file
            var resultData = _laptopService.ReadData();
            ItemsListBox.ItemsSource = resultData;
            
            OriginalList = resultData.Cast<Computer>().ToList();
            _filter = new ComputerFilter(OriginalList);
            _sorter = new Sorter(OriginalList.Cast<Item>().ToList());

        }

        private void DynamicProcessorCheckBox()
        {
            int column = 1;
            int cycleCount = 1;
            foreach (var processor in Processors)
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
                BrandsCheckBoxes.Add(checkbox);
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
        //Will create a new method to not repeat the code again like here
        private void DynamicBrandCheckBox()
        {
            int column = 1;
            int cycleCount = 1;
            foreach(var brand in Brands)
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
                BrandsCheckBoxes.Add(checkbox);
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
            FilterList();
        }

        private void DisableFilterButton_Click(object sender, RoutedEventArgs e)
        {
            DisableFilters();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (ItemsListBox.SelectedIndex == -1) return;

            Computer item = (sender as ListBox).SelectedItem as Computer;

            DisplayItem(item);

            List<Item> SimilarItems = item.FindSimilar(OriginalList.Cast<Item>().ToList());
            SimilarItemsListBox.ItemsSource = SimilarItems;
            

        }

        private void DisplayItem(Computer item)
        {
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

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("https://ksd-images.lt/display/aikido/store/1e3628060337b388dd4ffbce4f20f608.jpg?h=2000&w=2000");
            bi.EndInit();
            image1.Source = bi;

            Uri uri = new Uri(item.ItemURL);
            BuyHereHyper.NavigateUri = uri;
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

            DisplayItem(item);

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

        private void RemoveButton1_Click(object sender, RoutedEventArgs e)
        {
            ComparisonProductName1.Text = null;
            ComparisonProductPrice1.Text = null;
            ComparisonProductBrand1.Text = null;
            ComparisonProductProcessor1.Text = null;
            ComparisonProductRAM1.Text = null;
            ComparisonProductGraphicsCard1.Text = null;
            ComparisonProductResolution1.Text = null;
            ComparisonProductStorage1.Text = null;
            ComparisonProductRating1.Text = null;

            ComparingItem1 = null;
        }

        private void RemoveButton2_Click(object sender, RoutedEventArgs e)
        {
            ComparisonProductName2.Text = null;
            ComparisonProductPrice2.Text = null;
            ComparisonProductBrand2.Text = null;
            ComparisonProductProcessor2.Text = null;
            ComparisonProductRAM2.Text = null;
            ComparisonProductGraphicsCard2.Text = null;
            ComparisonProductResolution2.Text = null;
            ComparisonProductStorage2.Text = null;
            ComparisonProductRating2.Text = null;

            ComparingItem2 = null;
        }

        private void CompareButton_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(ComparisonProductName1.Text))
            {

                ComparisonProductName1.Text = ProductName.Text;
                ComparisonProductPrice1.Text = ProductPrice.Text;
                ComparisonProductBrand1.Text = ProductBrand.Text;
                ComparisonProductProcessor1.Text = ProductProcessor.Text;
                ComparisonProductRAM1.Text = ProductRAM.Text;
                ComparisonProductGraphicsCard1.Text = ProductGraphicsCard.Text;
                ComparisonProductResolution1.Text = ProductResolution.Text;
                ComparisonProductStorage1.Text = ProductStorage.Text;

                ComparingItem1 = (Computer)ItemsListBox.SelectedItem;

            }
            else 
            {

                ComparisonProductName2.Text = ProductName.Text;
                ComparisonProductPrice2.Text = ProductPrice.Text;
                ComparisonProductBrand2.Text = ProductBrand.Text;
                ComparisonProductProcessor2.Text = ProductProcessor.Text;
                ComparisonProductRAM2.Text = ProductRAM.Text;
                ComparisonProductGraphicsCard2.Text = ProductGraphicsCard.Text;
                ComparisonProductResolution2.Text = ProductResolution.Text;
                ComparisonProductStorage2.Text = ProductStorage.Text;
                
                ComparingItem2 = (Computer)ItemsListBox.SelectedItem;

            }
            UpdateComparison();
            ComparisonGrid.Visibility = Visibility.Visible;
            ItemInfoStackPanel.Visibility = Visibility.Collapsed;
            ListStackPanel.Visibility = Visibility.Collapsed;
        }

        private void AZSortButton_Click(object sender, RoutedEventArgs e)
        {
            AZSort();
        }

        private void ZASortButton_Click(object sender, RoutedEventArgs e)
        {
            ZASort();
        }

        private void PriceAscSortButton_Click(object sender, RoutedEventArgs e)
        {
            PriceAscSort();
        }

        private void PriceDescSortButton_Click(object sender, RoutedEventArgs e)
        {
            PriceDescSort();
        }

    }
}

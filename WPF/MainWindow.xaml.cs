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
using PathLibrary;
using ItemLibrary.DataContexts;
using System.Net.Http;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Item> OriginalList = new List<Item>();
        private static HttpClient client = new HttpClient();

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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateFilterCheckbox();
            OriginalList = (await GetAPIAsync<Computer>("http://localhost:53882/api/Computers")).Cast<Item>().ToList();
            ItemsListBox.ItemsSource = OriginalList;
            _filter = new ComputerFilter();
            _sorter = new Sorter(OriginalList.Cast<Item>().ToList());
        }

        private static async Task<List<T>> GetAPIAsync<T>(string path) where T:Item
        {
            List<T> objects = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if(response.IsSuccessStatusCode)
            {
                objects = await response.Content.ReadAsAsync<List<T>>();
            }
            return objects;
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
            ProductPrice.Text = '€' + (item.Price).ToString();
            ProductBrand.Text = item.ManufacturerName;
            ProductProcessor.Text = item.Processor.Name;
            ProductRAM.Text = (item.RAM).ToString() + "GB " + item.RAM_type;
            ProductGraphicsCard.Text = item.GraphicsCardName + ' ' + item.GraphicsCardMemory;
            ProductResolution.Text = item.Resolution;
            ProductStorage.Text = (item.StorageCapacity).ToString() + "GB";
            InfoStackPanelSecond.Children.Add(new TextBlock() { Text = "BUM BUM" });
            SimilarProducts.Text = "Similar Products";
            BuyHereButton.Visibility = Visibility.Visible;
            CompareButton.Visibility = Visibility.Visible;
            InfoStackPanelSecond.Visibility = Visibility.Visible;
            InfoStackPanelFirst.Visibility = Visibility.Visible;
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(item.ImageLink);
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

        private void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            Hyperlink hl = BuyHereHyper;
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

            _comparingItem1 = null;
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

            _comparingItem2 = null;
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

                _comparingItem1 = (Computer)ItemsListBox.SelectedItem;

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

                _comparingItem2 = (Computer)ItemsListBox.SelectedItem;

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

        private void MenuOpenButton_Click(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Visible;
            FilterGrid.Visibility = Visibility.Collapsed;
            FilterButtonGrid.Visibility = Visibility.Visible;
            MenuButtonGrid.Visibility = Visibility.Collapsed;
        }

        private void FilterOpenButton_Click(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            FilterGrid.Visibility = Visibility.Visible;
            MenuButtonGrid.Visibility = Visibility.Visible;
            FilterButtonGrid.Visibility = Visibility.Collapsed;
        }

        private void CategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            ItemInfoStackPanel.Visibility = Visibility.Collapsed;
            ListStackPanel.Visibility = Visibility.Collapsed;
            ComparisonGrid.Visibility = Visibility.Collapsed;
            CategoriesMenuGrid.Visibility = Visibility.Visible;
        }

        private void SmartphoneCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LaptopCategory_Click(object sender, RoutedEventArgs e)
        {
            ItemInfoStackPanel.Visibility = Visibility.Visible;
            ListStackPanel.Visibility = Visibility.Visible;
            ComparisonGrid.Visibility = Visibility.Collapsed;
            CategoriesMenuGrid.Visibility = Visibility.Collapsed;
        }
    }
}

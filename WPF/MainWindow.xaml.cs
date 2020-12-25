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
using ModelLibrary;
using DataContent;
using System.IO;
using System.Diagnostics;
using DataManipulation;
using DataContent.ReadingCSV;
using DataManipulation.Filters;
using System.Xml;
using System.Linq.Expressions;
using PathLibrary;
using ModelLibrary.DataContexts;
using System.Net.Http;
using System.Configuration;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Type Type = null;
        private SolidColorBrush brush = Brushes.Black;
        private List<Grid> CustomTextBlocks = new List<Grid>();
        private List<TextBlock> CustomTexts = new List<TextBlock>();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserSettings.Default.Theme == "Dark") brush = Brushes.White;
            CreateFilterCheckbox();
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
            if (Type == typeof(Computer))
            {
                Computer item = (sender as ListBox).SelectedItem as Computer;

                DisplayItem(item);

                List<Item> SimilarItems = item.FindSimilar(OriginalList.Cast<Item>().ToList());
                SimilarItemsListBox.ItemsSource = SimilarItems;
            }
            else if(Type == typeof(Smartphone))
            {
                Smartphone item = (sender as ListBox).SelectedItem as Smartphone;

                DisplayItem(item);

                List<Item> SimilarItems = item.FindSimilar(OriginalList.Cast<Item>().ToList());
                SimilarItemsListBox.ItemsSource = SimilarItems;
            }
            

        }

        private void DisplayItem(Computer item)
        {
            //Setting textboxes
            foreach (var grid in CustomTextBlocks) { InfoStackPanelFirst.Children.Remove(grid); };
            CustomTexts.Clear();
            CustomTextBlocks = new List<Grid>();
            ProductName.Text = item.Name;
            ProductPrice.Text = '€' + (item.Price).ToString();
            ProductBrand.Text = item.ManufacturerName;
            ProductProcessor.Text = item.Processor.Name;
            ProductRAM.Text = (item.RAM).ToString() + "GB " + item.RAM_type;
            AddTextblock(InfoStackPanelFirst, "Graphics card" ,item.GraphicsCardName + ' ' + item.GraphicsCardMemory);
            AddTextblock(InfoStackPanelFirst,"Resolution", item.Resolution);
            AddTextblock(InfoStackPanelFirst,"Storage", item.StorageCapacity.ToString() + "GB");
            SimilarProducts.Text = "Similar Products";
            BuyHereButton.Visibility = Visibility.Visible;
            CompareButton.Visibility = Visibility.Visible;
            InfoStackPanelFirst.Visibility = Visibility.Visible;
            var bi = new BitmapImage();
            try
            {
                
                bi.BeginInit();
                bi.UriSource = new Uri(item.ImageLink);
                bi.EndInit();
                image1.Source = bi;
                Uri uri = new Uri(item.ItemURL);
                BuyHereHyper.NavigateUri = uri;
            }
            catch(UriFormatException ex)
            {
                
            }
            
        }
        private void AddTextblock(StackPanel panel, string name, string value)
        {
            var grid = new Grid() { Name = name.Replace(" ", "")};
            var text = new TextBlock()
            {
                Text = name + ":",
                FontFamily = new FontFamily("Candara Light"),
                Foreground = brush,
                FontSize = 14,
                Margin = new Thickness(10, 0, 10, 0)
            };
            CustomTexts.Add(text);
            grid.Children.Add(text);
            text = new TextBlock()
            {
                Text = value,
                FontFamily = new FontFamily("Candara Light"),
                Foreground = brush,
                FontSize = 14,
                Margin = new Thickness(100, 0, 10, 0)
            };
            CustomTexts.Add(text);
            grid.Children.Add(text);
            CustomTextBlocks.Add(grid);
            panel.Children.Add(grid);
        }
        private void DisplayItem(Smartphone item)
        {
            //Setting textboxes
            foreach (var grid in CustomTextBlocks) { InfoStackPanelFirst.Children.Remove(grid); };
            CustomTexts.Clear();
            CustomTextBlocks = new List<Grid>();
            ProductName.Text = item.Name;
            ProductPrice.Text = '€' + (item.Price).ToString();
            ProductBrand.Text = item.ManufacturerName;
            ProductProcessor.Text = item.Processor;
            AddTextblock(InfoStackPanelFirst, "Screen", item.ScreenDiagonal.ToString());
            AddTextblock(InfoStackPanelFirst, "RAM", item.RAM + "GB");
            AddTextblock(InfoStackPanelFirst, "Battery", item.BatteryStorage.ToString() + "AMh"); 
            SimilarProducts.Text = "Similar Products";
            BuyHereButton.Visibility = Visibility.Visible;
            CompareButton.Visibility = Visibility.Visible;
            
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

            if (Type == typeof(Computer))
            {
                Computer item = (sender as ListBox).SelectedItem as Computer;

                DisplayItem(item);

                List<Item> SimilarItems = item.FindSimilar(OriginalList.Cast<Item>().ToList());
                SimilarItemsListBox.ItemsSource = SimilarItems;
            }
            else if (Type == typeof(Smartphone))
            {
                Smartphone item = (sender as ListBox).SelectedItem as Smartphone;

                DisplayItem(item);
            }
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
            ComparisonProductRating1.Text = null;
            ComparisonProduct1Custom1.Text = null;
            ComparisonProduct1Custom2.Text = null;
            ComparisonProduct1Custom3.Text = null;

            _comparingItem1 = null;
            _comparing2Item1 = null;
        }

        private void RemoveButton2_Click(object sender, RoutedEventArgs e)
        {
            ComparisonProductName2.Text = null;
            ComparisonProductPrice2.Text = null;
            ComparisonProductBrand2.Text = null;
            ComparisonProductProcessor2.Text = null;
            ComparisonProductRAM2.Text = null;
            ComparisonProductRating2.Text = null;
            ComparisonProduct2Custom1.Text = null;
            ComparisonProduct2Custom2.Text = null;
            ComparisonProduct2Custom3.Text = null;

            _comparingItem2 = null;
            _comparing2Item2 = null;
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


                if (Type == typeof(Computer))
                {
                    _comparingItem1 = (Computer)ItemsListBox.SelectedItem;
                    ComparisonProduct1Custom1.Text = _comparingItem1.GraphicsCardName;
                    ComparisonProduct1Custom2.Text = _comparingItem1.Resolution;
                    ComparisonProduct1Custom3.Text = _comparingItem1.StorageCapacity.ToString();
                }
                else if (Type == typeof(Smartphone))
                {
                    _comparing2Item1 = (Smartphone)ItemsListBox.SelectedItem;
                    ComparisonProduct1Custom1.Text = _comparing2Item1.ScreenDiagonal.ToString();
                    ComparisonProduct1Custom2.Text = _comparing2Item1.Resolution;
                    ComparisonProduct1Custom3.Text = _comparing2Item1.BatteryStorage.ToString();
                }
            }
            else 
            {

                ComparisonProductName2.Text = ProductName.Text;
                ComparisonProductPrice2.Text = ProductPrice.Text;
                ComparisonProductBrand2.Text = ProductBrand.Text;
                ComparisonProductProcessor2.Text = ProductProcessor.Text;
                ComparisonProductRAM2.Text = ProductRAM.Text;


                if (Type == typeof(Computer))
                {
                    _comparingItem2 = (Computer)ItemsListBox.SelectedItem;
                    ComparisonProduct2Custom1.Text = _comparingItem2.GraphicsCardName;
                    ComparisonProduct2Custom2.Text = _comparingItem2.Resolution;
                    ComparisonProduct2Custom3.Text = _comparingItem2.StorageCapacity.ToString();
                }
                else if (Type == typeof(Smartphone))
                {
                    _comparing2Item2 = (Smartphone)ItemsListBox.SelectedItem;
                    ComparisonProduct2Custom1.Text = _comparing2Item2.ScreenDiagonal.ToString();
                    ComparisonProduct2Custom2.Text = _comparing2Item2.Resolution;
                    ComparisonProduct2Custom3.Text = _comparing2Item2.BatteryStorage.ToString();
                }

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

        private async void SmartphoneCategory_Click(object sender, RoutedEventArgs e)
        {
            ItemInfoStackPanel.Visibility = Visibility.Visible;
            ListStackPanel.Visibility = Visibility.Visible;
            ComparisonGrid.Visibility = Visibility.Collapsed;
            CategoriesMenuGrid.Visibility = Visibility.Collapsed;
            OriginalList = (await GetAPIAsync<Smartphone>(ConfigurationManager.AppSettings.Get("smartphoneKey"))).Cast<Item>().ToList();
            ItemsListBox.ItemsSource = OriginalList;
            Type = typeof(Smartphone);
            Slidet1Text.Text = "Price";
            Slidet2Text.Text = "Storage";
            Slidet3Text.Text = "RAM";
            Custom1.Text = "Screen";
            Custom2.Text = "Resolution";
            Custom3.Text = "Battery";
            ResetComparison();
        }

        private async void LaptopCategory_Click(object sender, RoutedEventArgs e)
        {
            ItemInfoStackPanel.Visibility = Visibility.Visible;
            ListStackPanel.Visibility = Visibility.Visible;
            ComparisonGrid.Visibility = Visibility.Collapsed;
            CategoriesMenuGrid.Visibility = Visibility.Collapsed;
            OriginalList = (await GetAPIAsync<Computer>(ConfigurationManager.AppSettings.Get("computerKey"))).Cast<Item>().ToList();
            ItemsListBox.ItemsSource = OriginalList;
            Type = typeof(Computer);
            Slidet1Text.Text = "Price";
            Slidet2Text.Text = "Storage";
            Slidet3Text.Text = "RAM";
            Custom1.Text = "Graphic Card";
            Custom2.Text = "Resolution";
            Custom3.Text = "Storage";
            ResetComparison();
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserSettings.Default.Theme == "Light")
            {
                UserSettings.Default.PrimaryColor = "#80DEEA";
                UserSettings.Default.SecondaryColor = "#2A2828";
                UserSettings.Default.ThirdColor = "#2A2828";
                UserSettings.Default.AdditionalColor = "#E5A1DB";
                UserSettings.Default.BackgroundColor = "#312F2F";
                UserSettings.Default.FontColor = "#FFFFFF";
                UserSettings.Default.Theme = "Dark";
                UserSettings.Default.Save();
                brush = Brushes.White;
                foreach(var text in CustomTexts) { text.Foreground = brush; }
            }
            else
            {
                UserSettings.Default.PrimaryColor = "#FFFFFF";
                UserSettings.Default.SecondaryColor = "#80DEEA";
                UserSettings.Default.ThirdColor = "#E5A1DB";
                UserSettings.Default.AdditionalColor = "#FFFFFF";
                UserSettings.Default.BackgroundColor = "#FFFFFF";
                UserSettings.Default.FontColor = "#000000";
                UserSettings.Default.Theme = "Light";
                UserSettings.Default.Save();
                brush = Brushes.Black;
                foreach (var text in CustomTexts) { text.Foreground = brush; }
            }
        }
    }
}

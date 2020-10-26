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
using DataContent
using System.IO;
using DataManipulation;
using DataContent.ReadingCSV;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Filter _filter;
        private Sorter _sorter;
        private List<Item> OriginalList = new List<Item>();

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation =
                WindowStartupLocation.CenterScreen;
        }

        private void PriceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSliderValue.Text = "Price up to: " + '€' + PriceSlider.Value.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            string _filePath = MainPath.GetMainPath() + @"\Data\senukai.csv";

            var _laptopService = new LaptopServiceCSV();

            //Here We are calling function to read CSV file
            var resultData = _laptopService.ReadData(_filePath);
            ItemsListBox.ItemsSource = resultData;
            
            OriginalList = resultData.Cast<Item>().ToList();
            _filter = new Filter(ItemsListBox.Items.Cast<Item>().ToList());
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            
            int MaxRange = (int)PriceSlider.Value;
            if(MaxRange != 0)
            {

                //Filtering by price range
                var List = _filter.FilterByPrice(0, MaxRange);

                /*updating the list inside the class so we can filter out the list 
                 *which already has been filtered by price
                */
                _filter.UpdateList(List);

            }

            List<Item> List1 = new List<Item>();

            //Filtering by manufacturer
            if ((bool)AsusCheckBox.IsChecked)
            {
                List1.AddRange(_filter.FilterByManufacturer("Asus"));
            }
            if ((bool)LenovoCheckBox.IsChecked)
            {
                List1.AddRange(_filter.FilterByManufacturer("Lenovo"));
            }
            if ((bool)AppleCheckBox.IsChecked)
            {
                List1.AddRange(_filter.FilterByManufacturer("Apple"));
            }
            if ((bool)HuaweiCheckBox.IsChecked)
            {
                List1.AddRange(_filter.FilterByManufacturer("Huawei"));
            }
            if ((bool)AcerCheckBox.IsChecked)
            {
                List1.AddRange(_filter.FilterByManufacturer("Acer"));
            }
            if ((bool)HPCheckBox.IsChecked)
            {
                List1.AddRange(_filter.FilterByManufacturer("HP"));
            }
 
            ItemsListBox.ItemsSource = List1;
            _filter.UpdateList(OriginalList);
        }

        private void DisableFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsListBox.ItemsSource = OriginalList;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (ItemsListBox.SelectedIndex == -1) return;

            Computer item = (sender as ListBox).SelectedItem as Computer;
            ProductName.Text = item.Name;
            ProductPrice.Text = '€' + (item.Price).ToString();
            ProductBrand.Text = item.ManufacturerName;
            ProductProcessor.Text = item.ProcessorName;
            ProductRAM.Text = (item.RAM).ToString() + "GB " + item.RAM_type;
            ProductGraphicsCard.Text = item.GraphicsCardName + ' ' + item.GraphicsCardMemory;
            ProductResolution.Text = item.Resolution;
            ProductStorage.Text = (item.StorageCapacity).ToString() + "GB";

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

        /*
        private void FilterMenuClose_Click(object sender, RoutedEventArgs e)
        {
            FilterMenuOpen.Visibility = Visibility.Visible;
            FilterMenuClose.Visibility = Visibility.Collapsed;
        }

        private void FilterMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            FilterMenuClose.Visibility = Visibility.Visible;
            FilterMenuOpen.Visibility = Visibility.Collapsed;
        } */
    }
}

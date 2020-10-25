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
using System.IO;


namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation =
                WindowStartupLocation.CenterScreen;
        }

        //Computer test = new Computer();
        private void PriceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSliderValue.Text = "Price up to: " + PriceSlider.Value.ToString() + "$";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            for (int i = 0; i < 4; i++)
            {
                _filePath = Directory.GetParent(_filePath).FullName;
            }
            _filePath += @"\Data\senukai.csv";

            var _laptopService = new LaptopServiceCSV();

            //Here We are calling function to read CSV file
            var resultData = _laptopService.ReadData(_filePath);
            ItemsListBox.ItemsSource = resultData;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (ItemsListBox.SelectedIndex == -1) return;

            Computer item = (sender as ListBox).SelectedItem as Computer;
            ProductName.Text = item.Name;
            ProductPrice.Text = '€' + (item.Price).ToString();
            ProductBrand.Text = item.ManufacturerName;
            ProductProcessor.Text = item.ProcessorName;

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

﻿using MaterialDesignThemes.Wpf;
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


public class Computer {
    public string Name { get; set; }
}

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
        }

        Computer test = new Computer();
        private void PriceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSliderValue.Text = "Price up to: " + PriceSlider.Value.ToString() + "$";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var computers = new List<Computer>();
            computers.Add(new Computer() { Name = "Apple - MacBook Air 13.3\"" });
            computers.Add(new Computer() { Name = "Asus - 14\" ZenBook Duo Touch Laptop" });
            computers.Add(new Computer() { Name = "Lenovo - IdeaPad 15.6\" Laptop"});

            ItemsListView.ItemsSource = computers;
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

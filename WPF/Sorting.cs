using DataManipulation;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF
{
    public partial class MainWindow
    {
        private Sorter _sorter;

        private void AZSort()
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByNameAsc();

            ItemsListBox.ItemsSource = items;
        }

        private void ZASort()
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByNameDesc();

            ItemsListBox.ItemsSource = items;
        }

        private void PriceAscSort()
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByPriceAsc();

            ItemsListBox.ItemsSource = items;
        }

        private void PriceDescSort()
        {
            List<Item> items = new List<Item>();
            items = ItemsListBox.ItemsSource.Cast<Item>().ToList();

            _sorter.UpdateList(items);
            items = _sorter.SortByPriceDesc();

            ItemsListBox.ItemsSource = items;
        }

    }
}

﻿using System;
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
using System.Collections;
using UserClient.StoreService;

namespace UserClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CustomerDTO customer { get; set; }
        public MainWindow(CustomerDTO user)
        {
            customer = user;
            InitializeComponent();
            UpdateView();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void UpdateView()
        {

            RefreshStore(null,null);
            RefreshUser();
            RefreshInventory();
            Saldo.Content = ""+customer.Saldo;
            Saldo.UpdateLayout();
        }
        private void RefreshUser()
        {
            StoreServiceClient proxy = new StoreServiceClient();
            customer = proxy.Authenticate(customer.Name, customer.Password);
        }
        private void RefreshInventory()
        {
            StoreServiceClient proxy = new StoreServiceClient();
            OrderDTO[] orders = proxy.GetCustomerOrders(customer.Name);
            Dictionary<string,int> products = new Dictionary<string,int>();
            foreach (OrderDTO order in orders)
            {
                foreach (OrderEntryDTO entry in order.entries)
                {
                    if (products.ContainsKey(entry.ProductName))
                    {
                        products[entry.ProductName] += entry.Amount;
                    }
                    else
                    {
                        products.Add(entry.ProductName, entry.Amount);
                    }
                }
            }
            Inventory.Items.Clear();
            foreach (KeyValuePair<string, int> entry in products)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = entry.Key + " , " + entry.Value;
                Inventory.Items.Add(item);
            }
        }

        private void BuyProduct(object sender, RoutedEventArgs e)
        {
            ListBoxItem selected = (ListBoxItem)Stock.SelectedValue;
            StoreServiceClient proxy = new StoreServiceClient();
            Dictionary<string,int> orderRows = new Dictionary<string,int>();
            orderRows.Add(selected.Tag.ToString(), 1);
            try
            {
                proxy.PlaceOrder(customer.Name, orderRows);
                UpdateView();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "title", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshStore(object sender, RoutedEventArgs e)
        {
            StoreServiceClient proxy = new StoreServiceClient();
            ProductDTO[] products = null;
            try
            {
                products = proxy.GetProductsInStock();
                Stock.Items.Clear();
                foreach (ProductDTO product in products)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = String.Format("{0} - Prijs: {1} - Voorraad: {2}",product.Name, product.Price, product.Stock);
                    item.Tag = product.Name;
                    Stock.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "title", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

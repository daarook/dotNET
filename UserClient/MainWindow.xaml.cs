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
using System.Collections;
using UserClient.ProductService;
using UserClient.CustomerService;
using UserClient.OrderService;

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
            CustomerServiceClient proxy = new CustomerServiceClient();
            customer = proxy.Authenticate(customer.Name, customer.Password);
        }
        private void RefreshInventory()
        {

        }

        private void BuyProduct(object sender, RoutedEventArgs e)
        {
            ListBoxItem selected = (ListBoxItem)Stock.SelectedValue;
            OrderServiceClient proxy = new OrderServiceClient();
            Dictionary<string,int> orderRows = new Dictionary<string,int>();
            orderRows.Add(selected.Tag.ToString(), 1);
            try
            {
                proxy.PlaceOrder(customer.Name, orderRows);
                UpdateView();
            }
            catch (Exception ex)
            {

            }
        }

        private void RefreshStore(object sender, RoutedEventArgs e)
        {
            ProductServiceClient proxy = new ProductServiceClient();
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

            }
        }
    }
}

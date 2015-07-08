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

namespace UserClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CustomerDTO customer { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void BuyProduct(object sender, RoutedEventArgs e)
        {

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
                    item.Content = "test";
                    Stock.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

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
using System.Windows.Shapes;
using UserClient.CustomerService;

namespace UserClient
{
    /// <summary>
    /// Interaction logic for LogInAndRegister.xaml
    /// </summary>
    public partial class LogInAndRegister : Window
    {
        public LogInAndRegister()
        {
            InitializeComponent();
        }

        private void RegisterUser(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AuthenticateUser(object sender, RoutedEventArgs e)
        {
            //get info from textbox
            string username = "";
            string password = "";

            //verify the input


            //call service
            CustomerServiceClient proxy = new CustomerServiceClient();

            Customer user = proxy.Authenticate(username, password);

            if (user != null) //success from authenticate
            {
                Close();
                MainWindow w = new MainWindow();
                w.customer = user;
            }
        }
    }
}

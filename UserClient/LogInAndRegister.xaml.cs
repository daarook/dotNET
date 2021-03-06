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
using System.Windows.Shapes;
using UserClient.StoreService;
using System.ServiceModel;

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
            //get input from textbox
            string username = RegisterUsername.Text;

            if (!String.IsNullOrEmpty(username)) // check if input is not null or empty
            {
                string message;
                //call service
                using (StoreServiceClient proxy = new StoreServiceClient())
                {
                    message = proxy.Register(username);
                }
                RegisterLabel.Content = message;

            }
            else
            {
                RegisterLabel.Content = "Vul a.u.b een waarde in.";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AuthenticateUser(object sender, RoutedEventArgs e)
        {
            //get info from textbox
            string username = LoginUsername.Text;
            string password = LoginPassword.Text;

            //verify the input

            bool validated = (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password));

            if (validated)
            {
                //call service
                StoreServiceClient proxy = new StoreServiceClient();

                CustomerDTO user = null;
                try
                {
                    user = proxy.Authenticate(username, password);
                    
                    MainWindow w = new MainWindow(user);
                    w.Show();

                    Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "title", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                    
            }
            else
            {
                LoginLabel.Content = "Vul a.u.b waardes in";
            }
        }
    }
}

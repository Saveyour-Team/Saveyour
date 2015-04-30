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
using System.Diagnostics;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window{
        private Boolean isLoggedIn = false;
        public static String username;
        public static String userData;
        private Boolean firstUsernameFocus = true;
        private Boolean firstPasswordFocus = true;
        private Boolean firstPasswordConfirmFocus = true;
        public Register()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }



        public Boolean loggedIn()
        {
            return isLoggedIn;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            username = usernameField.Text;
            String password = passwordField.Password;
            String confirm = confirmPasswordField.Password;
            if (!password.Equals(confirm))
            {
                loginStatusLabel.Content = "Password fields don't match!";
                return;
            }
            String command = "register";

            //Create a network connection and connect
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), username + "\r\r\r" + password + "\r\r\r" + command);
            Debug.WriteLine(response);

            String[] splitAt = { "\r\r\n" };
            String[] responseData = response.Split(splitAt, StringSplitOptions.None);
            //userData = responseData[1];



          
            if (responseData[0].Contains("Invalid"))
            {
                loginStatusLabel.Content = "Invalid username/password";
            }
            else if (responseData[0].Contains("Certificate"))
            {
                loginStatusLabel.Content = "You need to add the SaveYour Certificate!";
            }
            else if (responseData[0].Contains("Registered"))
            {
                loginStatusLabel.Content = "Successfully Registered!";
            }
            else if (responseData[0].Contains("Username already exists!"))
            {
                loginStatusLabel.Content = "Username already exists!";
            }
            else
            {
                loginStatusLabel.Content = "Error connecting to server!";
            }
        }

        private void addCertificateButton_Click(object sender, RoutedEventArgs e)
        {
            NetworkControl.addCertificate();
        }

        private void usernameGotFocus(object sender, RoutedEventArgs e)
        {
            if (!firstUsernameFocus)
            {
                return;
            }
            firstUsernameFocus = false;
            usernameField.Text = "";

        }

        private void passwordGotFocus(object sender, RoutedEventArgs e)
        {
            if (!firstPasswordFocus)
            {
                return;
            }
            firstPasswordFocus = false;
            passwordField.Password = "";

        }

        private void passwordConfirmGotFocus(object sender, RoutedEventArgs e)
        {
            if (!firstPasswordConfirmFocus)
            {
                return;
            }
            firstPasswordConfirmFocus = false;
            confirmPasswordField.Password = "";

        }

        private void onPasswordKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                registerButton_Click(this, e);
            }
        }

        private void destroyRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

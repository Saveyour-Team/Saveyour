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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window{
        private Boolean isLoggedIn = false;
        public static String username;
        public static String userData;
        private Boolean firstUsernameFocus = true;
        private Boolean firstPasswordFocus = true;
        public LoginWindow()
        {
            InitializeComponent();
        }

        public Boolean loggedIn()
        {
            return isLoggedIn;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            username = usernameField.Text;
            String password = passwordField.Text;
            String command = "login";

            //Create a network connection and connect
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), username + "\r\r\r" + password + "\r\r\r" + command);
            Debug.WriteLine(response);

            String[] splitAt = { "\r\r\n" };
            String[] responseData = response.Split(splitAt, StringSplitOptions.None);
            //userData = responseData[1];

            if (responseData.Length > 1)
            {
                userData = responseData[1];
                Debug.WriteLine("UserData: " + userData);
            }
           

            if (response.Contains("Logged in as"))
            {
                //Feedback newForm = new Feedback(this);
                //newForm.ShowDialog();
                isLoggedIn = true;
                Shell.getShell(); //Boots the shell
                Shell.getSaveLoader().setLogin(username, password);
                this.Hide();
                //loginStatusLabel.Content = username + " has logged in!";
                loggedInWindow loggedIn = (loggedInWindow) Shell.launch("loggedInWindow");

                loggedIn.load(username + " likes data!");

                //Quicknotes quicknotes = (Quicknotes) Shell.launch("Quicknotes");
                //quicknotes.load("Type your notes here!");

                Settings settings = (Settings)Shell.launch("Settings");

                this.Close();
            }
            else if (response.Contains("Invalid"))
            {
                loginStatusLabel.Content = "Invalid username/password";
            }
            else if (response.Contains("Certificate"))
            {
                loginStatusLabel.Content = "You need to add the SaveYour Certificate!";
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
            passwordField.Text = "";

        }

        private void onPasswordKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginButton_Click(this, e);
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
    }
}

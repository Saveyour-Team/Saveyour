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

            //Create a network connection and connect
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), username + "," + password);
            Debug.WriteLine(response);

            char[] delimiters = { ',', ':' };

            String[] hold = response.Split(delimiters);

            for (int i = 0; i < hold.Length; i++)
            {
                hold[i] = hold[i].Trim();
                //Debug.WriteLine(hold[i]);
            }

            userData = hold[hold.Length - 1];
            Console.WriteLine("Harro");

            if (response.Contains("Logged in as"))
            {
                //Feedback newForm = new Feedback(this);
                //newForm.ShowDialog();
                isLoggedIn = true;
                Shell.getShell(); //Boots the shell
                this.Hide();
                //loginStatusLabel.Content = username + " has logged in!";
                loggedInWindow loggedIn = (loggedInWindow)Shell.launch("loggedInWindow");

                loggedIn.load(username + " likes data!");
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
    }
}

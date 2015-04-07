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
using System.Reflection;
using SaveyourUpdate;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, SaveyourUpdatable
    {
        public static String username;
        private Boolean firstUsernameFocus = true;
        private Boolean firstPasswordFocus = true;
        public static String userData;
        public SaveyourUpdater updater;
        private Boolean isLoggedIn;
        public LoginWindow()
        {
            InitializeComponent();
            this.lblVersion.Content = this.ApplicationAssembly.GetName().Version.ToString();
            updater = new SaveyourUpdater(this);
            isLoggedIn = false;
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
            String userData = null;
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
                Shell.getShell(username, password); //Boots the shell (which sets up SaveLoader)
                //Shell.getSaveLoader().setLogin(username, password);
                this.Hide();
                //loginStatusLabel.Content = username + " has logged in!";
                loggedInWindow loggedIn = (loggedInWindow)Shell.launch("loggedInWindow");
                loggedIn.load(username + " likes data!");
                Quicknotes quicknotes = (Quicknotes)Shell.launch("Quicknotes");
                quicknotes.load("Type your notes here!");
                if (userData != null)
                {
                    Shell.getSaveLoader().loadToLaunch(userData);
                    Shell.getSaveLoader().loadModules(userData);
                }
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
        
        private void addCertificateButton_Click(object sender, RoutedEventArgs e)
        {
            NetworkControl.addCertificate();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            updater.doUpdate();
        }

        public string ApplicationName
        {
            get { return "Saveyour"; }
        }

        public string ApplicationID
        {
            get { return "Saveyour"; }
        }

        public Assembly ApplicationAssembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        public Uri UpdateXmlLocation
        {
            get { return new Uri("https://github.com/Saveyour-Team/Release/raw/master/SaveyourXML.xml"); }
            //This should be the XML file found on github. This must be the RAW version so that it will start downloading.
            //This XML file is also in the release repo so it will look for the latest update possible.
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }






    }
}

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
    public partial class LoginWindow : Window, SaveyourUpdatable{
        private Boolean isLoggedIn = false;
        public static String username;
        private Boolean firstUsernameFocus = true;
        private Boolean firstPasswordFocus = true;
        public SaveyourUpdater updater;

        public LoginWindow()
        {
            InitializeComponent();

            //Sets start position to center of screen
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //Updates the label to the current version of Saveyour
            this.lblVersion.Content = this.ApplicationAssembly.GetName().Version.ToString();
            updater = new SaveyourUpdater(this); //Creates and update object to allow update fetching and applying
        }

        public Boolean loggedIn()
        {
            return isLoggedIn;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /*
         * This event handler handles when a user clicks the login button. It will attempt to connect to the network, and then send the username
         * and password to authenticate the user.
         * */
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            loginStatusLabel.Content = "Logging into Saveyour..";

            username = usernameField.Text;
            String password = passwordField.Password;
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
                
                if (userData != null)
                {
                    //Shell.getSaveLoader().loadToLaunch(userData);
                }
                                  

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

        /*
         * Adds a certificate for a TLS connection. 
         * */
        private void addCertificateButton_Click(object sender, RoutedEventArgs e)
        {
            NetworkControl.addCertificate();
        }

        /*
         * This event handler clears the contents of the username field when it is clicked for the first time.
         * */
        private void usernameGotFocus(object sender, RoutedEventArgs e)
        {
            if (!firstUsernameFocus)
            {
                return;
            }
            firstUsernameFocus = false;
            usernameField.Text = "";
        }

        /*
         * This event handler clears the contents of the password field when it is clicked for the first time.
         * */
        private void passwordGotFocus(object sender, RoutedEventArgs e)
        {
            if (!firstPasswordFocus)
            {
                return;
            }
            firstPasswordFocus = false;
            passwordField.Password = "";

        }

        /*
         * This event handlers allows the user to hit enter to log in instead of clicking the button.
         * */
        private void onPasswordKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginButton_Click(this, e);
            }
        }

        /*
         * This event handler handles when the user hits the register button. A register window will be created and displayed to the user.
         * */
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        /*
         * This event handler handles the button click of the update button. It will use the SaveyourUpdate object created in the contructor
         * to update the application. See SaveyourUpdate project for more information on how this works.
         * */
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

        /*
         * This event handler handles when the user holds down the left mouse button on the title bar. It allows the user
         * to drag the window around.
         * */
        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /*
         * This event handler handles when the user clicks the X button in the login window. It should shutdown the application.
         * */
        private void destroyApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /*
         * This event handler handles right and middle mouse button clicks so that it does not crash the program since DragMove() does
         * not handle right and middle mouse buttons.
         * */
        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed)
                e.Handled = true;
        }

        private void webClick(object sender, RoutedEventArgs e)
        {
            string targetURL = @"http://saveyour.herokuapp.com";
            System.Diagnostics.Process.Start(targetURL);
        }
    }
}

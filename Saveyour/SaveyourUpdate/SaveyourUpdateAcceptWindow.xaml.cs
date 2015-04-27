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

namespace SaveyourUpdate
{
    /// <summary>
    /// Interaction logic for SaveyourUpdateAcceptWindow.xaml
    /// </summary>
    internal partial class SaveyourUpdateAcceptWindow : Window
    {

        private SaveyourUpdatable applicationInfo;
        private SaveyourUpdateXML updateInfo;
        private SaveyourUpdateInfoWindow updateInfoWindow;

        internal SaveyourUpdateAcceptWindow(SaveyourUpdatable applicationInfo, SaveyourUpdateXML updateInfo)
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            this.applicationInfo = applicationInfo;
            this.updateInfo = updateInfo;

            //this.Text = this.applicationInfo.ApplicationName + " - Update Available";
            //This was supposed to set the text at the top of the Window to the application name
            //Unsure if this is still needed

            /*
            if (this.applicationInfo.ApplicationIcon != null)
            {
                this.Icon = this.applicationInfo.ApplicationIcon;
            }
            */

            this.lblNewVersion.Content = String.Format("New Version: {0}", this.updateInfo.Version.ToString());
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (this.updateInfoWindow == null)
            {
                this.updateInfoWindow = new SaveyourUpdateInfoWindow(this.applicationInfo, this.updateInfo);
            }

            this.updateInfoWindow.ShowDialog();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

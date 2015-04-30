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
    /// This class generates a Window that allows the user to check if they want to download the update or not. At this point,
    /// there is necessarily an update available to download.
    /// </summary>
    internal partial class SaveyourUpdateAcceptWindow : Window
    {

        private SaveyourUpdatable applicationInfo;
        private SaveyourUpdateXML updateInfo;
        private SaveyourUpdateInfoWindow updateInfoWindow;

        internal SaveyourUpdateAcceptWindow(SaveyourUpdatable applicationInfo, SaveyourUpdateXML updateInfo)
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen; //Centers the window to the screen

            this.applicationInfo = applicationInfo;
            this.updateInfo = updateInfo;

            this.lblNewVersion.Content = String.Format("New Version: {0}", this.updateInfo.Version.ToString()); //Updates the label so that it displays the version of the new and current version of Saveyour
        }

        /*
         * This event handler is if the user selects Yes to downloading the update. A response is recorded which will let SaveyourUpdater know that the user
         * would like to download the update. Then this window is closed.
         * */
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /*
         * This event handler is if the user selects No to downloading the update. A response is recorded which will let SaveyourUpdater know that the user
         * would not like to download the update. Then this window is closed.
         * */
        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /*
         * This event handler is if the user selects Details to downloading the update. The info window is then created and displayed to the user if it does not already exist.
         * */
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (this.updateInfoWindow == null)
            {
                this.updateInfoWindow = new SaveyourUpdateInfoWindow(this.applicationInfo, this.updateInfo);
            }

            this.updateInfoWindow.ShowDialog();
        }

        /*
         * This event hanlder allows the window to be dragged around the screen. There is a "title bar" that can be
         * dragged around due to this event handler.
         * */
        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        /*
         * This event handler checks for what type of mouse press occurred. Since DragMove() does not handle right
         * clicks, they are ignored due to this event handler.
         * */
        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed)
                e.Handled = true;
        }
    }
}

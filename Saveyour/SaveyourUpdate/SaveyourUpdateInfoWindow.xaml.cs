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
    /// Interaction logic for SaveyourUpdateInfoWindow.xaml
    /// The purpose of this class is to create the update information Window. It will display the update information
    /// which is fetched from the github and located in the XML file on github.
    /// </summary>
    internal partial class SaveyourUpdateInfoWindow : Window
    {
        internal SaveyourUpdateInfoWindow(SaveyourUpdatable applicationInfo, SaveyourUpdateXML updateInfo)
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //Updates the Title label in the Window
            //This is no longer really used as of ver 1.1.0.0, however this is the title of
            //the window so it has not been removed.
            this.Title = applicationInfo.ApplicationName + "Update Info";

            //Updates the version label in the Window
            this.lblVersions.Content = String.Format("Current Version: {0}\nUpdate Version: {1}",
                applicationInfo.ApplicationAssembly.GetName().Version.ToString(), updateInfo.Version.ToString());

            //Create document object so that information can be added to the RichTextBox
            FlowDocument ObjFdoc = new FlowDocument();
            Paragraph ObjPara1 = new Paragraph();
            ObjPara1.Inlines.Add(new Run(updateInfo.Description));
            ObjFdoc.Blocks.Add(ObjPara1);

            //Make the RichTextBox display the document created earlier.
            txtDescription.Document = ObjFdoc;  

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
         * This event handler handles the event where the user hits the back button. The window is hidden rather than disposed so that
         * SaveyourUpdater does not need to recreate this Window if the user wants to click details again.
         * */
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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

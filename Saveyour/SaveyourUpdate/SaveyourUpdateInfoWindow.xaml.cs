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
    /// </summary>
    internal partial class SaveyourUpdateInfoWindow : Window
    {
        internal SaveyourUpdateInfoWindow(SaveyourUpdatable applicationInfo, SaveyourUpdateXML updateInfo)
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //if (applicationInfo.ApplicationIcon != null)
            //    this.Icon = applicationInfo.ApplicationIcon;
            //Need to figure out how to configure Icons in WPF forms

            this.Title = applicationInfo.ApplicationName + "Update Info";
            this.lblVersions.Content = String.Format("Current Version: {0}\nUpdate Version: {1}",
                applicationInfo.ApplicationAssembly.GetName().Version.ToString(), updateInfo.Version.ToString());

            FlowDocument ObjFdoc = new FlowDocument();
            Paragraph ObjPara1 = new Paragraph();
            ObjPara1.Inlines.Add(new Run(updateInfo.Description));

            ObjFdoc.Blocks.Add(ObjPara1);

            // Finally Assign the FlowDocuemnt object to Document Property fo RichTextBox Control.  

            txtDescription.Document = ObjFdoc;  

        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
                e.Handled = true;
        }
    }
}

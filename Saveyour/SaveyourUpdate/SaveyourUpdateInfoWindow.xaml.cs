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

            //if (applicationInfo.ApplicationIcon != null)
            //    this.Icon = applicationInfo.ApplicationIcon;
            //Need to figure out how to configure Icons in WPF forms

            this.Title = applicationInfo.ApplicationName + "Update Info";
            this.lblVersions.Content = String.Format("Current Version: {0}\nUpdate Version: {1}",
                applicationInfo.ApplicationAssembly.GetName().Version.ToString(), updateInfo.Version.ToString());

            FlowDocument flowDoc = new FlowDocument();
            Paragraph paragraph1 = new Paragraph();
            paragraph1.Inlines.Add(new Run(updateInfo.Description));
            flowDoc.Blocks.Add(paragraph1);

            txtDescription.Document = flowDoc;
        }
    }
}

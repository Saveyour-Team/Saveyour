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

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Modules : Window, Module
    {
        Window qnotes;
        Window weeklytd;

        public Modules()
        {
            InitializeComponent();            
        }

        private void QN_Click(object sender, RoutedEventArgs e)
        {
            if (qnotes.IsVisible)
                qnotes.Hide();
            else if (qnotes.IsLoaded)
                qnotes.Show();
        }

        private void WTD_Click(object sender, RoutedEventArgs e)
        {
            if (weeklytd.IsVisible)
                weeklytd.Hide();
            else if (weeklytd.IsLoaded)
                weeklytd.Show();
        }

        public void addQNotes(Window module)
        {
            qnotes = module;
        }

        public void addWTD(Window module)
        {
            weeklytd = module;
        }

        public String moduleID()
        {
            return "Settings";
        }

        public Boolean update()
        {
            return false;
        }

        public String save()
        {
            return "";
        }

        public Boolean load(String data)
        {
            return false;
        }

        public Boolean Equals(Module other)
        {
            return false;
        }

        private void GC_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

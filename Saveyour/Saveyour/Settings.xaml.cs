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
    public partial class Settings : Window, Module
    {
        Window qnotes;

        public Settings()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (qnotes.IsVisible)
                qnotes.Hide();
            else if (qnotes.IsLoaded)
                qnotes.Show();
        }

        public void addQNotes(Window module)
        {
            qnotes = module;
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
    }
}

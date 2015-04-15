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
    /// Interaction logic for Homework.xaml
    /// </summary>
    public partial class Homework : Window, Module
    {

        GroupBox first = null;
        Window parent = null;

        public Homework()
        {
            InitializeComponent();

            first = (GroupBox)FindName("subject1");
            parent = (Window)FindName("hWindow");


        }

        public String moduleID()
        {
            return "Homework";
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
            return moduleID().Equals(other.moduleID());
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

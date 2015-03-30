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
    /// Interaction logic for Quicknotes.xaml
    /// </summary>
    public partial class Quicknotes : Window, Module
    {
        public Quicknotes()
        {
            InitializeComponent();
        }

        public String moduleID()
        {
            return "Quicknotes";
        }

        public Boolean update()
        {
            return false;
        }

        public String save()
        {
            return QuicknotesText.Text;
        }

        public Boolean load(String data)
        {
            QuicknotesText.Text = data;
            return true;
        }

        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }


        private void onKeyUp(object sender, KeyEventArgs e)
        {
            Shell.getSaveLoader().save();
        }
    }
}

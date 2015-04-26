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

	private QuicknotesControl qnControl;

        public Quicknotes(QuicknotesControl control)
        {
            InitializeComponent();
		    qnControl = control;
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
          //  Shell.getSaveLoader().save();
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Shell.getSaveLoader().save();
        }

        private void lostFocus(object sender, RoutedEventArgs e)
        {
            Shell.getSaveLoader().save();
        }

        private void AddQN_Click(object sender, RoutedEventArgs e)
        {
		    qnControl.addQuickNote();            
        }

        private void RemoveQN_Click(object sender, RoutedEventArgs e)
        {
		    qnControl.removeQuickNote(this);
        }
    }
}

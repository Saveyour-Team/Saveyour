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

        //This is the parent module that launched this instance of quicknotes.  We need it to tell it when to add or remove quicknotes.
    	private QuicknotesControl qnControl;

        public Quicknotes(QuicknotesControl control)
        {
            InitializeComponent();
		    qnControl = control;

            Left = (System.Windows.SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            Top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height;
        }


     
        /***** MODULE INTERFACE METHODS *****/

        //Returns this module's type ID.
        public String moduleID()
        {
            return "Quicknotes";
        }

        //Currently unused, in the future it may be used if we abstract the GUI control away from WPF forms.
        public Boolean update()
        {
            return false;
        }

        //Returns a string containing the text currently displayed in Quicknotes for saving.
        public String save()
        {
            return QuicknotesText.Text;
        }

        //Loads the string given into the quicknotes text display.
        public Boolean load(String data)
        {
            QuicknotesText.Text = data;
            return true;
        }

        //This module is equal to another if they are both quicknotes modules.
        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }

        /***** END OF MODULE INTERFACE METHODS *****/
	



        /***** DATA BINDINGS TO XAML ELEMENTS *****/


    
        //This used to be used for saving, but we found it more efficient to save on lost focus and when the app is closed.
        private void onKeyUp(object sender, KeyEventArgs e)
        {
          //  Shell.getSaveLoader().save();
        }

        /*This is called when the window is closed.
        *It turns out that lostFocus is called when closing as well, so this was redundant.  We leave it in case we want to use it later for some other purpose.
         */
        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Shell.getSaveLoader().save();
        }

        //This is called whenever another window is selected. We save when this happens.
        private void lostFocus(object sender, RoutedEventArgs e)
        {
            Shell.getSaveLoader().save();
        }

        //This is called when the + button is clicked, it creates a new Quicknotes window.
        private void AddQN_Click(object sender, RoutedEventArgs e)
        {
		    Quicknotes changePos = qnControl.addQuickNote();
            changePos.Left = this.Left + 325;
        }

        //This is called when the x button is clicked, it removes this Quicknotes window.
        private void RemoveQN_Click(object sender, RoutedEventArgs e)
        {
		    qnControl.removeQuickNote(this);
        }

        //This is called when you click and hold on the top of the window so you can drag it around.
        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }        

        //This is called when you double click on the window.  If the default message is present it clears the window.
        private void QuicknotesText_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (QuicknotesText.Text.Contains("Type notes here!"))
                QuicknotesText.Text = "";
        }

        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
                e.Handled = true;
        }

        /***** END OF DATA BINDINGS TO XAML ELEMENTS *****/
    }
}

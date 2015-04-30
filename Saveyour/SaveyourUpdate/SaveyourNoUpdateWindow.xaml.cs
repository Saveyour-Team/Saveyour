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
    /// Interaction logic for SaveyourNoUpdateWindow.xaml
    /// This class is simply used to generate a message box showing that there are no available updates for Saveyour
    /// </summary>
    internal partial class SaveyourNoUpdateWindow : Window
    {
        internal SaveyourNoUpdateWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen; //Center window when created
        }

        /*
         * This event handler is when the user clicks on the "OK" button.
         * It will dispose of the Window and free any resources that it had been using.
         * */
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        /*
         * This event hanlder allows the window to be dragged around the screen. There is a "title bar" that can be
         * dragged around due to this event handler.
         * */
        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove(); //
        }

        /*
         * This event handler checks for what type of mouse press occurred. Since DragMove() does not handle right
         * clicks, they are ignored due to this event handler.
         * */
        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
                e.Handled = true;
        }
    }
}

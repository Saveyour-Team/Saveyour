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
    /// Interaction logic for AddHomeworkSubject.xaml
    /// </summary>
    public partial class AddHomeworkSubject : Window
    {
        private String input;

        public AddHomeworkSubject()
        {
            InitializeComponent();
            Empty.Visibility = Visibility.Hidden;
        }

        public String getSubject()
        {
            if (input != null && input != "")
                return input;
            else
                return "Empty Subject"; //Homework.xaml.cs will handle this case and not add the subject when seeing this
        }

        /***** LOGIC FOR DATA BINDINGS IN XAML *****/

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            input = Subject.Text; //Saves text of inputted subject title 
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            input = "";       //Homework.xaml.cs will also handle "" inputs where it will not add the subject
            this.Close();            
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (input == "")
            {
                Empty.Visibility = Visibility.Visible; //Warns user that input is blank and does not continue.
            }
            else
            {
                input = Subject.Text; //Success scenario of having text in input
                this.Close();
            }    
            
        }        

        private void Subject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Subject.Text = ""; //Clears out whatever is inputted in textbox for Subject
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed)
                e.Handled = true;
        }

        /***** END OF LOGIC FOR DATA BINDINGS IN XAML *****/


    }
}

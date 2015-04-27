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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            input = Subject.Text;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            input = "";
            this.Close();            
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (input == "")
            {
                Empty.Visibility = Visibility.Visible;
            }
            else
            {
                input = Subject.Text;
                this.Close();
            }    
            
        }

        public String getSubject()
        {
            if (input != null && input != "")
                return input;
            else
                return "Empty Subject";
        }

        private void Subject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Subject.Text = "";
        }


    }
}

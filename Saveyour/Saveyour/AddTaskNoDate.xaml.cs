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
using System.Text.RegularExpressions;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for AddTaskNoDate.xaml
    /// </summary>
    public partial class AddTaskNoDate : Window
    {

        private DateTime taskDate;
        private Task theTask;
        private bool firstFocus = true;
        public AddTaskNoDate(Window parent)
        {
            this.Owner = parent;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            taskDate = DateTime.MinValue;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            theTask = new Task(taskTitle.Text, TaskDescription.Text, Convert.ToInt32(lblWeight.Content), taskDate);

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public Task getTask()
        {
            return theTask;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblWeight.Content = weightSlider.Value.ToString();
        }

        private void descriptBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (firstFocus)
            {
                TaskDescription.Text = "";
                firstFocus = false;
            }
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

    }
}

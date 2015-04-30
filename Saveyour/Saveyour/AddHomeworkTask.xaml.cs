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
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddHomeworkTask : Window
    {

        private DateTime taskDate;
        private String taskDescription;
        private Task theTask;
        private bool firstFocus = true;
        public AddHomeworkTask(Window parent)
        {
            this.Owner = parent;
            InitializeComponent();
            TaskCalendar.SelectedDate = DateTime.Today;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /***** LOGIC FOR DATABINDINGS IN XAML *****/

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            taskDate = (DateTime)TaskCalendar.SelectedDate;
            this.DialogResult = true;
            taskDescription = TaskDescription.Text;
            taskDate = (DateTime) TaskCalendar.SelectedDate;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void descriptBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (firstFocus)
            {
                TaskDescription.Text = "";
                firstFocus = false;
            }
        }

        public String getTaskDescription()
        {
            return taskDescription;
        }

        public DateTime getTaskDate()
        {
            return taskDate;
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

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
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {

        private DateTime taskDate;
        private String taskDescription;
        public AddTaskWindow(Window parent)
        {
            this.Owner = parent;
            InitializeComponent();
            TaskCalendar.SelectedDate = DateTime.Today;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            taskDate = (DateTime)TaskCalendar.SelectedDate;
            taskDescription = TaskDescription.Text;
            this.DialogResult = true;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public DateTime getTaskDate()
        {
            return taskDate;
        }
        public String getTaskDescription()
        {
            return taskDescription;
        }
    }
}

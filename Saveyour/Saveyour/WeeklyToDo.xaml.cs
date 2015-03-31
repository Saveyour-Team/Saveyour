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
    /// Interaction logic for WeeklyToDo.xaml
    /// </summary>
    /// 
  
    public partial class WeeklyToDo : Window, Module
    {
        public WeeklyToDo()
        {
            InitializeComponent();
        }

        public String moduleID()
        {
            return "WeeklyToDo";
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
            //load stuff from data...



            //Hide any segments not being used...
            if (MondayTasks.Text.Equals(""))
            {
                MondayTitleBorder.Visibility = System.Windows.Visibility.Collapsed;
                MondayTaskBorder.Visibility = System.Windows.Visibility.Collapsed;
            }

            return false;
        }

        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }

        private void addTaskButton(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWin = new AddTaskWindow();
            addTaskWin.ShowInTaskbar = false;
            addTaskWin.Owner = Application.Current.MainWindow;
            Boolean result = (Boolean) addTaskWin.ShowDialog();
            DateTime newTaskDate;
            String newTaskDescription;
            if ( (result == null) || (!result))
            {

            }
            newTaskDate = addTaskWin.getTaskDate();
            newTaskDescription = addTaskWin.getTaskDescription();
            //Do stuff!

            
        }
    }
}

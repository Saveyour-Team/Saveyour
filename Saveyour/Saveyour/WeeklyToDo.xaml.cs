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
using System.Diagnostics;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for WeeklyToDo.xaml
    /// </summary>
    /// 



  
    public partial class WeeklyToDo : Window, Module
    {
        private class DateTask
        {
            public DateTime date;
            public String task;
        }

        private List<DateTask> dates = new List<DateTask>();

        private DateTime nextWeek;

        public WeeklyToDo()
        {
            InitializeComponent();

            //this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 1);
            //this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.2);

            Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width;
            Top = 0;

            nextWeek = DateTime.Today.AddDays(7);
            nextWeek = new DateTime(nextWeek.Year, nextWeek.Month, nextWeek.Day);

            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    MondayTitle.Text = MondayTitle.Text + " (TODAY)";
                    break;
                case DayOfWeek.Tuesday:
                    TuesdayTitle.Text = TuesdayTitle.Text + " (TODAY)";
                    break;
                case DayOfWeek.Wednesday:
                    WednesdayTitle.Text = WednesdayTitle.Text + " (TODAY)";
                    break;
                case DayOfWeek.Thursday:
                    ThursdayTitle.Text = ThursdayTitle.Text + " (TODAY)";
                    break;
                case DayOfWeek.Friday:
                    FridayTitle.Text = FridayTitle.Text + " (TODAY)";
                    break;
                case DayOfWeek.Saturday:
                SaturdayTitle.Text = SaturdayTitle.Text + " (TODAY)";
                    break;
                case DayOfWeek.Sunday:
                    SundayTitle.Text = SundayTitle.Text + " (TODAY)";
                    break;
            }
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
            String output = "";
            foreach (DateTask task in dates)
            {
                output = output + task.date.ToString() + "\t\t" + task.task + "\r\t\r"; 
            }
            return output;
        }

        public Boolean load(String data)
        {
            //load stuff from data...



            //Hide any segments not being used...
            /*
            if (MondayTasks.Text.Equals(""))
            {
                MondayTitleBorder.Visibility = System.Windows.Visibility.Collapsed;
                MondayTaskBorder.Visibility = System.Windows.Visibility.Collapsed;
            }*/

            Debug.WriteLine("WklyTDLoading: " + data);
            String[] splitAt = { "\r\t\r" };
            String[] moduleData = data.Split(splitAt, StringSplitOptions.None);
            DateTask newDate;
            String[] dateTaskString;

            String[] splitAt2 = { "\t\t" };

            for (int i = 0; i < moduleData.Length; i++)
            {
                Debug.WriteLine("ModuleDat: " + moduleData[i]);
                dateTaskString = moduleData[i].Split(splitAt2, StringSplitOptions.None);
                if (moduleData.Length > 1)
                {
                    try
                    {
                        newDate = new DateTask();
                        Debug.WriteLine("!:" + dateTaskString[0]);
                        newDate.date = DateTime.Parse(dateTaskString[0]);
                        newDate.task = dateTaskString[1];
                        dates.Add(newDate);
                        display(newDate);
                    }
                    catch(FormatException e)
                    {
                        Debug.WriteLine("Invalid WeeklyTodDo Task Format!");
                    }

                }

            }


            return false;
        }

        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }

        private void display(DateTask task)
        {
            if (task.date.CompareTo(nextWeek) > 0)
            {
                return;
            }

            switch (task.date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    MondayTasks.Text = MondayTasks.Text +task.task + "\n";
                    break;
                case DayOfWeek.Tuesday:
                    TuesdayTasks.Text = TuesdayTasks.Text + task.task + "\n";
                    break;
                case DayOfWeek.Wednesday:
                    WednesdayTasks.Text = WednesdayTasks.Text + task.task + "\n";
                    break;
                case DayOfWeek.Thursday:
                    ThursdayTasks.Text = ThursdayTasks.Text + task.task + "\n";
                    break;
                case DayOfWeek.Friday:
                    FridayTasks.Text = FridayTasks.Text + task.task + "\n";
                    break;
                case DayOfWeek.Saturday:
                    SaturdayTasks.Text = SaturdayTasks.Text + task.task + "\n";
                    break;
                case DayOfWeek.Sunday:
                    SundayTasks.Text = SundayTasks.Text + task.task + "\n";
                    break;
            }
        }

        private void addTaskButton(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Clicked!");
            AddTaskWindow addTaskWin = new AddTaskWindow(this);
            addTaskWin.ShowInTaskbar = false;
            Nullable<bool> result =  addTaskWin.ShowDialog();
            DateTime newTaskDate;
            String newTaskDescription;
            if ( !result.HasValue || !result.Value)
            {
                return;
            }
            newTaskDate = addTaskWin.getTaskDate();
            newTaskDescription = addTaskWin.getTaskDescription();
            DateTask newTask = new DateTask();
            newTask.date = newTaskDate;
            newTask.task = newTaskDescription;
            dates.Add(newTask);
            display(newTask);

            
        }
        private void onLostFocus(object sender, RoutedEventArgs e)
        {
            Shell.getSaveLoader().save();
        }
    }
}

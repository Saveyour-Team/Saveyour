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
using System.Windows.Markup;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary; 

namespace MyApp.Tools
{

    [ValueConversion(typeof(string), typeof(string))]
    public class RatioConverter : MarkupExtension, IValueConverter
    {
        private static RatioConverter _instance;

        public RatioConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { // do not let the culture default to local to prevent variable outcome re decimal syntax
            double size = System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            return size.ToString("G0", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        { // read only converter...
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new RatioConverter());
        }

    }
}

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
        private DateTime yesterday;
        private List<TextBlock> days = new List<TextBlock>();
        private List<TextBlock> taskDays = new List<TextBlock>();
        private DateTime curTopDay;
        private Dictionary<DateTime,List<Task>> hashTasks;
        private Border[] borders;



        public WeeklyToDo()
        {
            InitializeComponent();

            //this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 1);
            //this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.2);

            Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width;
            Top = 0;

            hashTasks = new Dictionary<DateTime,List<Task>>();

            curTopDay = DateTime.Today;

            nextWeek = DateTime.Today.AddDays(7);
            nextWeek = new DateTime(nextWeek.Year, nextWeek.Month, nextWeek.Day);

            yesterday = DateTime.Today.AddDays(-1);
            yesterday = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);

            TextBlock today = null;
            Border todayBorder = null;
            days.Add(SundayTitle);
            days.Add(MondayTitle);
            days.Add(TuesdayTitle);
            days.Add(WednesdayTitle);
            days.Add(ThursdayTitle);
            days.Add(FridayTitle);
            days.Add(SaturdayTitle);

            taskDays.Add(SundayTasks);
            taskDays.Add(MondayTasks);
            taskDays.Add(TuesdayTasks);
            taskDays.Add(WednesdayTasks);
            taskDays.Add(ThursdayTasks);
            taskDays.Add(FridayTasks);
            taskDays.Add(SaturdayTasks);

            borders = new Border[14];
            borders[0] = SundayTitleBorder;
            borders[1] = SundayTaskBorder;
            borders[2] = MondayTitleBorder;
            borders[3] = MondayTaskBorder;
            borders[4] = TuesdayTitleBorder;
            borders[5] = TuesdayTaskBorder;
            borders[6] = WednesdayTitleBorder;
            borders[7] = WednesdayTaskBorder;
            borders[8] = ThursdayTitleBorder;
            borders[9] = ThursdayTaskBorder;
            borders[10] = FridayTitleBorder;
            borders[11] = FridayTaskBorder;
            borders[12] = SaturdayTitleBorder;
            borders[13] = SaturdayTaskBorder;

            int numToday = (int)DateTime.Today.DayOfWeek;
            today = days[numToday];
            todayBorder = borders[numToday * 2];
            reOrderDays();

            int j = 0;
            foreach (TextBlock day in days)
            {
                    numToday--;
                   // day.Text += " " + DateTime.Today.AddDays(numToday * -1).ToString("d");
                   day.Text += " " + DateTime.Today.AddDays((j - numToday + 7)%7).ToString("d");
                   j++;
            }
            today.Text += " (TODAY)";
            //COLORS ARE SUBJECT TO CHANGE (someone change them if they have a good color scheme!)
            today.Foreground = new SolidColorBrush(Colors.Green); //Changes text color
            todayBorder.Background = new SolidColorBrush(Colors.Cyan); //Changes background color
        }

        private void reOrderDays()
        {
            //Reorder the days so that the current day of the week is at the top.
            int startDay = (int)DateTime.Today.DayOfWeek; //Sunday = 0... Saturday = 6.
            int order = 0;
            for (int i = 0; i <= 13; i = i + 2)
            {
                //Order specifies where in the ordering of days a given day should go.
                order = (i / 2 - startDay + 7) % 7; //So if the startDay is 1, day 0 goes to place -1%7 = 6, and day 3 goes to (3 - 2)%7 = place 1.
                //The +7 above is needed because apparently c# uses % as remainder and not modulo.
                //Debug.WriteLine("Order: " + order +", i: " + i);
                //Since there are 2 rows per day, we put each "order" at row 2*order and 2*order+1.
                Grid.SetRow(borders[i], 2 * order + 1); //Moves the Title border and everything in it to the given row.
                Grid.SetRow(borders[i + 1], 2 * order + 2); //Moves the Task border and everything in it to the given row.

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
                        Debug.WriteLine("Invalid WeeklyToDo Task Format!");
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
            if (task.date.CompareTo(nextWeek) > 0 || task.date.CompareTo(yesterday) <= 0)
            {
                return;
            }

            int taskDay = (int)task.date.DayOfWeek;
            taskDays[taskDay].Text = taskDays[taskDay].Text + task.task + "\n";
        }

        private void addTaskButton(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWin = new AddTaskWindow(this);
            addTaskWin.ShowInTaskbar = false;
            Nullable<bool> result =  addTaskWin.ShowDialog();
            if ( !result.HasValue || !result.Value)
            {
                return;
            }

            Task task = addTaskWin.getTask();

            //newTaskDate = addTaskWin.getTaskDate();
            //newTaskDescription = addTaskWin.getTaskDescription();
            DateTask newTask = new DateTask();
            newTask.date = task.getDate();
            newTask.task = task.getTitle();
            dates.Add(newTask);

            //Try to add a new list containing the task.
            try
            {
                List<Task> temp = new List<Task>();
                temp.Add(task);
                hashTasks.Add(task.getDate(), temp);
            }
            //If the key already exists, add the task to the list.
            catch(ArgumentException err){
                hashTasks[task.getDate()].Add(task);
            }


            display(newTask);
            Shell.getSaveLoader().save();
        }


        private void onLostFocus(object sender, RoutedEventArgs e)
        {
            //Shell.getSaveLoader().save();
        }

        private void backWeek_Click(object sender, RoutedEventArgs e)
        {
            int numToday = (int)DateTime.Today.DayOfWeek;
            int count = 0;
            curTopDay = curTopDay.AddDays(-7);
            clearDisplay();

            foreach (TextBlock day in days)
            {
                day.Text = "";
                day.Text = curTopDay.AddDays(count - numToday).ToString("dddd");
                day.Text += " " + curTopDay.AddDays(count - numToday).ToString("d");
                count++;
            }
            if (curTopDay.CompareTo(nextWeek) < 0 && curTopDay.CompareTo(yesterday) > 0)
            {
                reOrderDays();
                days[numToday].Text += " (TODAY)";
            }

        }

        private void forwardWeek_Click(object sender, RoutedEventArgs e)
        {
            int numToday = (int)DateTime.Today.DayOfWeek;
            int count = 0;
            curTopDay = curTopDay.AddDays(7);
            clearDisplay();

            foreach (TextBlock day in days)
            {
                day.Text = "";
                day.Text = curTopDay.AddDays(count - numToday).ToString("dddd");
                day.Text += " " + curTopDay.AddDays(count - numToday).ToString("d");
                count++;
            }
            if (curTopDay.CompareTo(nextWeek) < 0 && curTopDay.CompareTo(yesterday) > 0)
            {
                reOrderDays();
                days[numToday].Text += " (TODAY)";
            }
        }

        private void clearDisplay()
        {
            foreach (TextBlock day in taskDays)
            {
                day.Text = "";
            }
        }

        private void loadDisplayTasks()
        {

        }

    }
}

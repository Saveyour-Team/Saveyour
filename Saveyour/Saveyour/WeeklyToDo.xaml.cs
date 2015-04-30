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

        //Dates used to determine whether or not to show tasks.
        private DateTime nextWeek;
        private DateTime yesterday;

        //The elements on the xaml form that represent the Day's titles and task lists.
        private List<TextBlock> days = new List<TextBlock>();
        private List<StackPanel> taskDays = new List<StackPanel>();

        //The date of the day currently on the top of the weekly display
        private DateTime curTopDay;

        //A list of all the tasks in the module, keyed by the date they are assigned for.
        private Dictionary<DateTime,List<Task>> hashTasks;

        //The borders of the days on the xaml form
        private Border[] borders;

        MainViewModel newModel;
        public WeeklyToDo()
        {
            InitializeComponent();
            newModel = new MainViewModel();
            this.DataContext = newModel;
            //Moves WeeklyToDo to the topright of the screen on launch.
            Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width;
            Top = 0;
            //Moves the add task button slightly lower from the top, and ensures centering
            Thickness point = addTaskBtn.Margin;
            addTaskBtn.Margin = new Thickness(point.Left, point.Top + 10, point.Right, point.Bottom);

            //Initialization of the class's fields
            hashTasks = new Dictionary<DateTime,List<Task>>();

            curTopDay = DateTime.Today;

            nextWeek = DateTime.Today.AddDays(7);
            //Forces nextWeek's date to be at the very beginning of the day
            nextWeek = new DateTime(nextWeek.Year, nextWeek.Month, nextWeek.Day);

            yesterday = DateTime.Today.AddDays(-1);
            //Forces yesterday's date to be at the very beginning of the day
            yesterday = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);

            TextBlock today = null;
            Border todayBorder = null;

            //Initialize our lists of days, tasks, and borders.

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

            //Stored the current day of the week (0 = Sunday, ..., 6 = Saturday)
            int numToday = (int)DateTime.Today.DayOfWeek;

            today = days[numToday];
            todayBorder = borders[numToday * 2];
            reOrderDays(); //Puts Today's date at the top of the display and sorts the rest accordingly.


            //Relabel the titles with the date of each day, and color them by the tasks present in each day.
            int j = 0;
            foreach (TextBlock day in days)
            {
		       colorByWeights(j);
               day.Text = "";
               day.Text = curTopDay.AddDays((j - numToday + 7)%7).ToString("dddd");
               day.Text += " " + curTopDay.AddDays((j - numToday + 7) % 7).ToString("d");
               j++;
            }

            reOrderDays();
          
            //Label today with the (TODAY) label.
            today.Text += " (TODAY)";
            //COLORS ARE SUBJECT TO CHANGE (someone change them if they have a good color scheme!)
            today.Foreground = new SolidColorBrush(Colors.Black); //Changes text color
            //todayborder.background = new solidcolorbrush(Colors.Cyan); //changes background color
        }
	
	/*Recolors the given day of the week (0=Sunday ... 6 = Saturday) based upon the weights of all the tasks in that day. */
	private void colorByWeights(int dayOfWeek){ //Currently only adds in today.
		// Converts the int for the dayOfWeek to the date that it corrsponds to
		DateTime day = curTopDay.AddDays((dayOfWeek + 7 - (int)(curTopDay.DayOfWeek) )%7 );
		int weight = sumOfTaskWeights(day);
		if (weight < 5){
            		//borders[dayOfWeek*2].Background = new SolidColorBrush(Colors.Green); //changes background color of title
            		borders[dayOfWeek*2 + 1].Background = new SolidColorBrush(Colors.Green); //changes background color of tasks list
		}
		else if(weight >=5 && weight < 8){		
            		//borders[dayOfWeek*2].Background = new SolidColorBrush(Colors.Yellow); //changes background color of title
            		borders[dayOfWeek*2 + 1].Background = new SolidColorBrush(Colors.Yellow); //changes background color of tasks list
		}

				
		else if(weight >=8 && weight < 10){		
            		//borders[dayOfWeek*2].Background = new SolidColorBrush(Colors.Orange); //changes background color of title
            		borders[dayOfWeek*2 + 1].Background = new SolidColorBrush(Colors.Orange); //changes background color of tasks list
		}

						
		else if(weight >=10){		
            		//borders[dayOfWeek*2].Background = new SolidColorBrush(Colors.Red); //changes background color of title
                    	borders[dayOfWeek * 2 + 1].Background = new SolidColorBrush(Colors.Red); //changes background color of tasks list
		}
	}


	/*Calculates the sum of the weights of all tasks on a given day */
	private int sumOfTaskWeights(DateTime day){
		int sum = 0;
        try
        {
            List<Task> taskList = hashTasks[day];
            foreach (Task task in taskList)
            {
                sum += task.getWeight();
            }
        }
        catch (KeyNotFoundException e)
        {
		//No tasks for this day, weight should stay at 0.
        }
		return sum;
	}

        /*
         * Reorders the days in the WeeklyToDo display so that the current day of the week is on top.
         */
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

        //Returns the ID tag of this module
        public String moduleID()
        {
            return "WeeklyToDo";
        }

        //Currently unused, may be used for abstracting GUI control later.
        public Boolean update()
        {
            return false;
        }

        /**
         * Saves the WeeklyToDo data in the format:
         * taskTitle \t\t TaskDescription \t\t taskWeight \t\t taskDateString \t\r\t (next task info...)
         **/
        public String save()
        {
            String output = "";
            foreach (KeyValuePair<DateTime, List<Task>> pair in hashTasks)
            {
                List<Task> taskList = pair.Value;
                foreach (Task task in taskList)
                {
                    if (output == "")
                    {
                        output = task.getTitle() + "\t\t" + task.getDescription() + "\t\t" + task.getWeight() + "\t\t" + task.getDate().ToString(); 
                    }
                    else
                    {
                        output = output + "\r\t\r" + task.getTitle() + "\t\t" + task.getDescription() + "\t\t" + task.getWeight() + "\t\t" + task.getDate().ToString(); 
                    }
                
                }
            }
            Debug.WriteLine("Saving: " + output);
            return output;
        }


        //Removes a given task from the hashTable and saves.
        private void removeTask(Task task)
        {
            List<Task> taskList = hashTasks[task.getDate()];
            taskList.Remove(task);
            Shell.getSaveLoader().save();
        }


        /*Create everything needed to display the tasks on the weekly calendar.*/
        private void createTaskLabel(Task task, StackPanel daysTasks){
            //A stackpanel to contain the task title and description textblocks
            StackPanel taskStack = new StackPanel();


            //Task Title TextBlock
            TextBlock taskLabel = new TextBlock();
            taskLabel.Text = task.getTitle();

            //Task Description TextBlock
            TextBlock taskDescriptLabel = new TextBlock();
            //Sets margin to be (Left, Top, Right, Bottom)
            taskDescriptLabel.Margin = new Thickness(10,0,0,0);
            taskDescriptLabel.Text = task.getDescription();

            //Task Removal Button
            Button removeTaskBtn = new Button();
            removeTaskBtn.Content = "X";
            removeTaskBtn.Tag = task;

            removeTaskBtn.ContentTemplate = this.Resources["remTemplate"] as DataTemplate;

            removeTaskBtn.Click += new RoutedEventHandler(taskRemoveBtn_Click);
            removeTaskBtn.Width = this.Width / 2;
            //removeTaskBtn.HorizontalAlignment = HorizontalAlignment.Center;


            //Add these to the stackpanel
            taskStack.Children.Add(taskLabel);
            taskStack.Children.Add(taskDescriptLabel);
            taskStack.Children.Add(removeTaskBtn);

            //Add listener for when the Title Label is clicked.
            taskLabel.MouseLeftButtonDown += new MouseButtonEventHandler(taskLabel_Click);

            //Adds the task information to the days list of tasks.
            daysTasks.Children.Add(taskStack);

            taskLabel.Visibility = Visibility.Visible;
            taskStack.Visibility = Visibility.Visible;
            removeTaskBtn.Visibility = Visibility.Collapsed;
            taskDescriptLabel.Visibility = Visibility.Collapsed;

        }

        //This is called when the "X" button below an expanded task is clicked.  sender.Tag contains the actual task involved
        private void taskRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Retrieves the task stored in removeBtns Tag field and removes it from our hashTable
            Task task = (Task)(sender as FrameworkElement).Tag;
            removeTask(task);
            Debug.WriteLine("called removetask on "+ task.getTitle());

            //Removes all the elements displaying the task on the WeeklyToDo GUI
            StackPanel taskStack = (StackPanel)((Button)sender).Parent;
            taskStack.Children.Clear();
	    
	    //Update the colors based upon the new list of tasks for that day.
	    int dayOfWeek = (int)task.getDate().DayOfWeek;
	    colorByWeights(dayOfWeek);
        }

        /* This method is called when a TaskTitle textblock is clicked! It shows or hides the task description as appropriate*/
        private void taskLabel_Click(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("Sender: " + sender.ToString() + "E: " + e.ToString());
            //Get the stackpanel that the textblock is in and search its children
            StackPanel taskStack = (StackPanel)((TextBlock)sender).Parent;
            //IEnumerable children = LogicalTreeHelper.GetChildren(taskStack);
            foreach (DependencyObject child in taskStack.Children)
            {
                //If it's not the title textblock, then toggle its visibility!
                if (child != sender)
                {
                    if (!((FrameworkElement)child).IsVisible)
                    {
                        ((FrameworkElement)child).Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ((FrameworkElement)child).Visibility = Visibility.Collapsed;
                    }
                }
            }

        }

        /**Loads in the tasks from the WeeklyToDo save string and displays them.
         * 
         **/
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
            //This array contains one string of task data at each index.
            String[] loadedTaskStrings = data.Split(splitAt, StringSplitOptions.None);
            //This array contains {"Title","Description",Weight,Date}
            String[] taskParameters;

            String[] splitAt2 = { "\t\t" };

            for (int i = 0; i < loadedTaskStrings.Length; i++)
            {
               // Debug.WriteLine("ModuleDat: " + moduleData[i]);
                taskParameters = loadedTaskStrings[i].Split(splitAt2, StringSplitOptions.None);
                if (taskParameters.Length == 4)
                {
                    try
                    {
                        Task newTask = new Task(taskParameters[0], taskParameters[1], Convert.ToInt32(taskParameters[2]), DateTime.Parse(taskParameters[3]));
                       // Debug.WriteLine("!:" + dateTaskString[0]);
                        addTask(newTask);
                        //If the task is in the current week, display it now.
                        if (newTask.getDate().CompareTo(nextWeek) < 0 && newTask.getDate().CompareTo(yesterday) > 0)
                        {
                            displayTask(newTask);
                        }
                    }
                    catch(FormatException e)
                    {
                        Debug.WriteLine("Invalid WeeklyToDo Task Format!");
                    }

                }
                else
                {
                    Debug.WriteLine("Invalid WeeklyToDo Task Format!");
                }

            }
		
	    //Now that the tasks are loaded, color the days by the task weights.
	    for(int i = 0; i < 7; i++){
		colorByWeights(i);
	    }

            return false;
        }

        //Two modules are equal if they are of the same type.
        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }



        /*Always displays the task given as input.*/
        private void displayTask(Task task)
        {
            int taskDay = (int)task.getDate().DayOfWeek;
            //taskDays[taskDay].Text = taskDays[taskDay].Text + task.getTitle() + "\n";
            StackPanel daysTasks = taskDays[taskDay];
            createTaskLabel(task, daysTasks);
        }

        /* For a given day is looks up all the tasks in hashTasks for that day and displays them on the week's display.   
         */
        private void displayDaysTasks(DateTime day)
        {
            try
            {
                List<Task> taskList = hashTasks[day];
                foreach (Task task in taskList)
                {
                    displayTask(task);
                }
            }
            catch (KeyNotFoundException e)
            {
                //If key's not there we don't need to do anything.
            }
        }


        //Adds a task to the hashTasks hashtable keyed by its date.
        private void addTask(Task task)
        {
            try
            {
                //Try to add the new date and task to the Dictionary
                List<Task> temp = new List<Task>();
                temp.Add(task);
                hashTasks.Add(task.getDate(), temp);
            }
            //If the key already exists, add the task to the list.
            catch (ArgumentException err)
            {
                hashTasks[task.getDate()].Add(task);
            }
        }

        /*This is called when the "Add Task" button is clicked.
         * It creates a new AddTask window and waits for its response (which is a task) then adds that task to this WeeklyToDo module,
         * and displays it if it is a task assigned for the currently showing week.
         * 
         */
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
            /*DateTask newTask = new DateTask();
            newTask.date = task.getDate();
            newTask.task = task.getTitle();
            dates.Add(newTask);*/

            //Try to add a new list containing the task.
            addTask(task);



            DateTime theNextWeek = curTopDay.AddDays(7);
            //Forces nextWeek's date to be at the very beginning of the day
            theNextWeek = new DateTime(theNextWeek.Year, theNextWeek.Month, theNextWeek.Day);

            DateTime lastDay = curTopDay.AddDays(-1);
            //Forces the past day's date to be at the very beginning of the day
            lastDay = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day);

            //If the task is in the current week, display it.
            if (task.getDate().CompareTo(theNextWeek) < 0 && task.getDate().CompareTo(lastDay) > 0)
            {
                displayTask(task);
		        colorByWeights((int)task.getDate().DayOfWeek);
            }

            
            Shell.getSaveLoader().save();
        }


        //This is called when focus is lost. We used to save in this case, but now we save only when tasks are added or removed.
        private void onLostFocus(object sender, RoutedEventArgs e)
        {
            //Shell.getSaveLoader().save();
        }

        //This is called when the backarrow button is clicked.  It moves the display back a week.
        private void backWeek_Click(object sender, RoutedEventArgs e)
        {
            int numToday = (int)DateTime.Today.DayOfWeek;
            int count = 0;
            curTopDay = curTopDay.AddDays(-7);
            clearDisplay();


            //Label the dates of the days of the week.
            foreach (TextBlock day in days)
            {
                day.Text = "";
                day.Text = curTopDay.AddDays((count - numToday + 7) % 7).ToString("dddd");
                day.Text += " " + curTopDay.AddDays((count - numToday + 7) % 7).ToString("d");
                count++;
            }

            //If the week is the week containing Today, label Today.
            if (curTopDay.CompareTo(nextWeek) < 0 && curTopDay.CompareTo(yesterday) > 0)
            {
                reOrderDays();
                days[numToday].Text += " (TODAY)";
            }

            //Display the tasks for the current week
            for (int i = 0; i < 7; i++)
            {
                DateTime day = curTopDay.AddDays(i);
                displayDaysTasks(day);
		colorByWeights(i);
            }
	
        }

        //This is called when the forwardarrow button is clicked.  It moves the display forward a week.
        private void forwardWeek_Click(object sender, RoutedEventArgs e)
        {
            int numToday = (int)DateTime.Today.DayOfWeek;
            int count = 0;
            curTopDay = curTopDay.AddDays(7);
            clearDisplay();

            //Label the dates of the days of the week.
            foreach (TextBlock day in days)
            {
                day.Text = "";
                day.Text = curTopDay.AddDays((count - numToday + 7) % 7).ToString("dddd");
                day.Text += " " + curTopDay.AddDays((count - numToday + 7) % 7).ToString("d");
                count++;
            }

            //If the week is the week containing Today, label Today.
            if (curTopDay.CompareTo(nextWeek) < 0 && curTopDay.CompareTo(yesterday) > 0)
            {
                reOrderDays();
                days[numToday].Text += " (TODAY)";
            }

            //Display the tasks for the current week
            for (int i = 0; i < 7; i++)
            {
                DateTime day = curTopDay.AddDays(i);
                displayDaysTasks(day);	
		colorByWeights(i);
            }
        }

        //This removes all of the tasks from the week's display.
        private void clearDisplay()
        {
            foreach (StackPanel day in taskDays)
            {
                //Removes all the tasks on each day.
                day.Children.Clear();
            }
        }

        //This allows the window to be moved by clicking and dragging the top if it.
        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed)
                e.Handled = true;
        }

        public class MainViewModel
        {
            public MainViewModel()
            {

            }

            private ICommand clickMeCommand;
            public ICommand ClickMeCommand
            {
                get
                {
                    if (clickMeCommand == null)
                        clickMeCommand = new RelayCommand(i => this.doThis(i), null);
                    return clickMeCommand;
                }
            }

            private void doThis(object sender)
            {
                Console.WriteLine("PRESSED BUTTON!!");
            }

        }

        public class RelayCommand : ICommand
        {
            readonly Action<object> execute;
            readonly Predicate<object> canExecute;

            public RelayCommand(Action<object> executeDelegate, Predicate<object> canExecuteDelegate)
            {
                execute = executeDelegate;
                canExecute = canExecuteDelegate;
            }

            bool ICommand.CanExecute(object parameter)
            {
                return canExecute == null ? true : canExecute(parameter);
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            void ICommand.Execute(object parameter)
            {
                execute(parameter);
            }
        }

    }
}

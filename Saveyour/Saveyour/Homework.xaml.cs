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
using System.IO;
using System.Windows.Markup;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Xml;
using System.Text.RegularExpressions;
using System.Timers;
using System.ComponentModel;
using System.Windows.Media.Animation;


namespace Saveyour
{
    /// <summary>
    /// Interaction logic for Homework.xaml
    /// </summary>
    public partial class Homework : Window, Module
    {
        //Task class object for the Listview
        public class Task
        {
            public String AssignmentName { get; set; }
            public String AssignmentDate { get; set; }

            public String AssignmentShortenedDate { get; set; }
            public String AssignmentSubject { get; set; }

        }

        private const int MAX_SUBJECTS = 10;
        MainViewModel newView;
        private static TabControl mainTabControl;
        private static ObservableCollection<Task> taskGroupAll;
        ObservableCollection<Task> taskGroup;
        private static TabItem AllTab;
        Storyboard newStoryBoard;
        public Homework()
        {
            mainTabControl = new TabControl();
            taskGroupAll = new ObservableCollection<Task>();
            AllTab = new TabItem();
            InitializeComponent();
            AllTab.Header = "All";
            AllTab.Content = createNewList(taskGroupAll);
            mainTabControl.Items.Insert(0, Homework.AllTab);
            mainTabControl.SelectedItem = AllTab;

            mainTabControl.HorizontalAlignment = HorizontalAlignment.Left;
            mainTabControl.Height = 391;
            mainTabControl.Margin = new Thickness(13, 28, 0, 0);
            mainTabControl.VerticalAlignment = VerticalAlignment.Top;
            mainTabControl.Width = 302;

            //Setting up a ModelView for clicking on buttons in the Listview
            errorLabel.Visibility = Visibility.Hidden;
            newView = new MainViewModel();
            this.DataContext = newView;
            //Creating of the All Tab that will contain all the tasks

            //Animation for  showing Label.
            newStoryBoard = new Storyboard();
            Console.WriteLine("ADDED IN CONSTRUCTOR!!!");
 
            
            //This needs to be scalable to multiple xaml elements.
            //We need to load the information for the subjects here.

            //After loading the information, we increase numSubjects and take the data from load to construct each GroupBox for each Grid

            //Load the GroupBox, then the Grid, then each task in Task, then each date in Date

            //                        

        }

        public String moduleID()
        {
            return "Homework";
        }

        public Boolean update()
        {
            return false;
        }

        public String save()
        {
            String output = "";
            foreach (TabItem subject in (ItemCollection) mainTabControl.Items)
            {
                output = output + subject.Header + ":";
                foreach (Task task in (ObservableCollection<Task>)((ListView)subject.Content).ItemsSource)
                {
                    output = output + task.AssignmentName + "," + task.AssignmentDate + "," + task.AssignmentShortenedDate + "," + task.AssignmentSubject +"\t\t";
                }
                output = output + "\r\t\r";
            }

            Debug.WriteLine("Saving: " + output);

            return output;
        }

        public Boolean load(String data)
        {
            Console.WriteLine("data = " + data);
            //Get the string from database and load it
            //Use substrings to find a subject name and all its tasks in it
            Debug.WriteLine("Homework Loading: " + data);
            String[] splitAt = { "\r\t\r" };
            String[] splitAt2 = { ":" };
            String[] splitAt3 = {"\t\t"};
            String[] splitAt4 = {","};
            String[] listOfSubjects = data.Split(splitAt, StringSplitOptions.None);
            String[] separateTasks;
            TabItem restoreSubject;
            ObservableCollection<Task> restoreSource;

            for (int i = 0; i < listOfSubjects.Length; i++)
            {
                if (!listOfSubjects[i].Equals(""))
                {
                    //Split the subject string into the Subject and Task Name
                    separateTasks = listOfSubjects[i].Split(splitAt2, StringSplitOptions.None);
                    restoreSource = new ObservableCollection<Task>();
                    restoreSubject = new TabItem();
                    if (!separateTasks[0].Equals("All"))
                    {
                        //If there is tasks under the subject
                        if (!string.IsNullOrWhiteSpace(separateTasks[1]))
                        {
                            //Split the string of all tasks into each individual task
                            String[] restoreTasks = separateTasks[1].Split(splitAt3, StringSplitOptions.None);
                            try
                            {
                                //Goes through all the tasks and adds it into an ObservableCollection<Task>
                                for (int j = 0; j < restoreTasks.Length; j++)
                                {
                                    if (!string.IsNullOrWhiteSpace(restoreTasks[j]))
                                    {
                                        String[] restoreTaskInfo = restoreTasks[j].Split(splitAt4, StringSplitOptions.None);

                                        Task newTask = new Task();
                                        newTask.AssignmentName = restoreTaskInfo[0];
                                        newTask.AssignmentDate = restoreTaskInfo[1];
                                        newTask.AssignmentShortenedDate = restoreTaskInfo[2];
                                        newTask.AssignmentSubject = restoreTaskInfo[3];
                                        restoreSource.Add(newTask);
                                        taskGroupAll.Add(newTask);
                                    }
                                }
                            }
                            catch (FormatException e)
                            {
                                Debug.WriteLine("Invalid WeeklyToDo Task Format!");
                            }                           
                        }
                        restoreSubject.Header = separateTasks[0];
                        restoreSubject.Content = createNewList(restoreSource);
                        mainTabControl.Items.Add(restoreSubject);
                    }

                }
            }
            Console.WriteLine(mainTabControl);
            AllTab.Content = createNewList(taskGroupAll);
            mainTabControl.SelectedItem = AllTab;
            windowGrid.Children.Add(mainTabControl);
            Console.WriteLine(((TabItem)mainTabControl.SelectedItem).Header);
            Console.WriteLine(mainTabControl);
            return true;
        }


        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }

        private void addSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            //When addSubjectButton is clicked, show a window where user can pick time and date
            AddHomeworkSubject display = new AddHomeworkSubject();
            display.ShowDialog();

            String check = display.getSubject();

            if (check.Equals("") || check.Equals("Empty Subject"))
            {
                return;
            }
            //Create a new tab that uses the subject name.
            TabItem newTab = new TabItem();
            newTab.Header = check; //Need to make sure we cannot exit window until Submit or Cancel is pressed.
            newTab.Content = createNewList(null);
            mainTabControl.Items.Add(newTab);
            //mainTabControl = subjectsTab;
            Shell.getSaveLoader().save();
        }
        
        public static void deleteTask(object sender)
        {
            //When the button to delete task is pressed, find the task in the same row as the button
            Task item = (Task)sender;
            TabItem tabitem = (TabItem) mainTabControl.SelectedItem;
            if (((TabItem)mainTabControl.SelectedItem) == AllTab)
            {
                taskGroupAll.Remove(item);
                TabItem removeFromSubject = null;
                foreach (TabItem tab in mainTabControl.Items)
                {
                    if (tab.Header.Equals(item.AssignmentSubject))
                    {

                        Console.WriteLine(tab.Header + " " + item.AssignmentSubject);
                        removeFromSubject = tab;
                    }
                }
                ((ObservableCollection<Task>)((ListView)removeFromSubject.Content).ItemsSource).Remove(item);
                ((ListView)AllTab.Content).Items.Refresh();
                Console.WriteLine("Removed!");
                Shell.getSaveLoader().save();
                return;
            }
            
      
            //Remove the task
            ((ObservableCollection<Task>)((ListView)((TabItem)mainTabControl.SelectedItem).Content).ItemsSource).Remove(item);
            //Find the task in the All tab and remove it from there too
            taskGroupAll.Remove(item);

            ((ListView)((TabItem)mainTabControl.SelectedItem).Content).Items.Refresh();

            //Refresh and Update List
            //((ListView)getTab.Content).Items.Refresh();

            //((ListView)((TabItem)mainTabControl.SelectedItem).Content).Items.Refresh();

            Shell.getSaveLoader().save();
        }

 

        private void addTask(object sender, RoutedEventArgs e)
        {
            //When user clicks on addTask, show a window where they can pick the date, task name and a description
            AddHomeworkTask setTaskWindow = new AddHomeworkTask(this);
            setTaskWindow.ShowInTaskbar = false;
            Nullable<bool> result = setTaskWindow.ShowDialog();
            if (!result.HasValue || !result.Value)
            {
                return;
            }
            //Get all the information
            String description = setTaskWindow.getTaskDescription();
            String date = setTaskWindow.getTaskDate().Year.ToString() + setTaskWindow.getTaskDate().Month.ToString() + setTaskWindow.getTaskDate().Day.ToString();
            String shortenedDate = setTaskWindow.getTaskDate().ToShortDateString();
            shortenedDate = shortenedDate.Remove(shortenedDate.Length - 5); 

            TabItem setTab = mainTabControl.SelectedItem as TabItem;
            Console.WriteLine(setTab);
            Task newTask = new Task { AssignmentName = description, AssignmentDate = date, AssignmentShortenedDate = shortenedDate, AssignmentSubject = (String) setTab.Header };

            //Add the task to the currently selected tab by gettings its list and adding it to the observablecollection
            ObservableCollection<Task> currentList = (ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource;
            if(currentList != taskGroupAll)
                sortAllTab(newTask,(ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource);
            sortAllTab(newTask, taskGroupAll);
            ((ListView)setTab.Content).Items.Refresh();
            //mainTabControl = subjectsTab;


            Shell.getSaveLoader().save();

        }

        private void sortAllTab(Task sortTask, ObservableCollection<Task> list)
        {
            int i = 0;
            String convertA;
            int convertB = int.Parse(sortTask.AssignmentDate);
            foreach(Task getTask in list){
                convertA = getTask.AssignmentDate;
                if (int.Parse(convertA) > convertB)
                    break;
                i++;
            }

            list.Insert(i, sortTask);
        }

        private ListView createNewList(ObservableCollection<Task> list)
        {
            
            ListView newList = new ListView();
            GridViewColumn nameColumn = new GridViewColumn();
            GridViewColumn dateColumn = new GridViewColumn();
            GridViewColumn editColumn = new GridViewColumn();
            GridView newGrid = new GridView();
            nameColumn.Width = 195;
            nameColumn.Header = "Assignment";
            nameColumn.DisplayMemberBinding = new Binding("AssignmentName");
            dateColumn.Width = 45;
            dateColumn.Header = "Date";
            dateColumn.DisplayMemberBinding = new Binding("AssignmentShortenedDate");
            editColumn.Width = 30;
            editColumn.Header = "Edit";
            editColumn.CellTemplate = this.Resources["editColumnTemplate"] as DataTemplate;
            newGrid.Columns.Add(nameColumn);
            newGrid.Columns.Add(dateColumn);
            newGrid.Columns.Add(editColumn);

            newList.View = newGrid;
            if(list == null){
                taskGroup = new ObservableCollection<Task>();
                newList.ItemsSource = taskGroup;
            }
            else
                newList.ItemsSource = list;

            return newList;
        }
        

        public class MainViewModel
        {
            public MainViewModel()
            {

            }

            private ICommand deleteCommand;
            private ICommand archiveCommand;
            public ICommand DeleteCommand
            {
                get
                {
                    if (deleteCommand == null)
                        deleteCommand = new RelayCommand(i => this.deleteSelectedTask(i), null);
                    return deleteCommand;
                }
            }
            public ICommand ArchiveCommand
            {
                get
                {
                    if (archiveCommand == null)
                        archiveCommand = new RelayCommand(i => this.archiveSelectedTask(i), null);
                    return archiveCommand;
                }
            }

            private void deleteSelectedTask(object sender)
            {
                Console.WriteLine("Pressed delete button!");
                deleteTask(sender);
            }

            private void archiveSelectedTask(object sender)
            {
                Console.WriteLine("Pressed archive task button!");
                //archiveTask(sender);
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
        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed)
                e.Handled = true;
        }

        private void removeSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainTabControl.SelectedItem == AllTab)
            {
                errorLabel.Visibility = System.Windows.Visibility.Visible;
                TimeSpan duration = TimeSpan.FromMilliseconds(500); //

                DoubleAnimation fadeInAnimation = new DoubleAnimation() { From = 0.0, To = 1.0, Duration = new Duration(duration) };

                DoubleAnimation fadeOutAnimation = new DoubleAnimation() { From = 1.0, To = 0.0, Duration = new Duration(duration) };
                fadeOutAnimation.BeginTime = TimeSpan.FromSeconds(2);

                Storyboard.SetTargetName(fadeInAnimation, errorLabel.Name);
                Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity", 1));
                newStoryBoard.Children.Add(fadeInAnimation);
                newStoryBoard.Begin(errorLabel);

                Storyboard.SetTargetName(fadeOutAnimation, errorLabel.Name);
                Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity", 0));
                newStoryBoard.Children.Add(fadeOutAnimation);
                newStoryBoard.Begin(errorLabel);
                newStoryBoard.Completed += delegate { errorLabel.Visibility = System.Windows.Visibility.Hidden; };
            }
            if (mainTabControl.SelectedItem != AllTab)
            {
                mainTabControl.Items.Remove(mainTabControl.SelectedItem);
            }
        }
    }
}

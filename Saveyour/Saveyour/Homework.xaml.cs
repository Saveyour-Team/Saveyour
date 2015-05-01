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

        private class Task
        {
            public String AssignmentName { get; set; }
            public String AssignmentDate { get; set; }

            public String AssignmentShortenedDate { get; set; }

        }

        private class Subject
        {
            public ObservableCollection<Task> taskCollection;

            public Subject()
            {
                taskCollection = new ObservableCollection<Task>();
            }
        }

        private const int MAX_SUBJECTS = 10;
        //Subject[] subjects = new Subject[MAX_SUBJECTS];
        MainViewModel newView;
        private static TabControl mainTabControl;
        ObservableCollection<Task> taskGroupAll;
        ObservableCollection<Task> taskGroup;
        TabItem AllTab;
        Storyboard newStoryBoard;
        public Homework()
        {
            InitializeComponent();
            errorLabel.Visibility = Visibility.Hidden;
            newView = new MainViewModel();
            this.DataContext = newView;
            taskGroupAll = new ObservableCollection<Task>();
            //subjects[0] = new Subject();
            
            AllTab = new TabItem();
            AllTab.Header = "All";
            taskGroupAll = new ObservableCollection<Task>();
            AllTab.Content = createNewList(taskGroupAll);
            subjectsTab.Items.Add(AllTab);
            Console.WriteLine(subjectsTab);
            newStoryBoard = new Storyboard();
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
            int i = 0;
            foreach (TabItem subject in (ItemCollection) subjectsTab.Items)
            {
                i++;
                output = output + subject.Header + ":";
                foreach (Task task in (ObservableCollection<Task>)((ListView)subject.Content).ItemsSource)
                {
                    output = output + task.AssignmentName + "," + task.AssignmentDate + "," + task.AssignmentShortenedDate + "\t\t";
                }
                output = output + "\r\t\r";
            }
            
            Debug.WriteLine("Saving: " + output);
            Debug.WriteLine(i);
            return output;
        }

        public Boolean load(String data)
        {
            
            Console.WriteLine(data);
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
                    if (separateTasks[0].Equals("All"))
                    {
                        restoreSubject = AllTab;
                        restoreSource = taskGroupAll;
                    }
                    else
                    {
                        restoreSubject = new TabItem();
                        restoreSource = new ObservableCollection<Task>();

                    }
                    
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
                                    restoreSource.Add(newTask);
                                }
                            }
                        }
                        catch (FormatException e)
                        {
                            Debug.WriteLine("Invalid WeeklyToDo Task Format!");
                        }
                    }
                    restoreSubject.Header = separateTasks[0];
                    Console.WriteLine(restoreSource);
                    restoreSubject.Content = createNewList(restoreSource);
                    Console.WriteLine("Content = " + restoreSubject.Content);
                    Console.WriteLine("Newly created = " + restoreSubject);
                    if (restoreSubject != AllTab)
                    {
                        subjectsTab.Items.Add(restoreSubject);
                    }
                }
            }

            mainTabControl = subjectsTab;
            Console.WriteLine("DONE LOADING!!! " + mainTabControl);
            return true;
        }


        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }

        private void addSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            
            AddHomeworkSubject display = new AddHomeworkSubject();
            display.ShowDialog();

            String check = display.getSubject();

            if (check.Equals("") || check.Equals("Empty Subject"))
            {
                return;
            }
            
            TabItem newTab = new TabItem();
            newTab.Header = check; //Need to make sure we cannot exit window until Submit or Cancel is pressed.
            newTab.Content = createNewList(null);
            subjectsTab.Items.Add(newTab);
            mainTabControl = subjectsTab;
            Shell.getSaveLoader().save();
        }
        
        public static void deleteTask(object sender)
        {
            Task item = (Task)sender;
            Console.Write("Item Name:");
            Console.WriteLine(item.AssignmentName);
            Console.WriteLine(mainTabControl.SelectedItem);
            TabItem tabitem = (TabItem) mainTabControl.SelectedItem;

            Console.WriteLine("maintabcontrol = " + mainTabControl);
            Console.WriteLine("tabitem = " +tabitem);
            ListView listview = (ListView) tabitem.Content;
            Console.WriteLine(listview);
            int index = ((ListView)((TabItem)mainTabControl.SelectedItem).Content).Items.IndexOf(item);
            Console.Write(index);
            ((ObservableCollection<Task>)((ListView)((TabItem)mainTabControl.SelectedItem).Content).ItemsSource).Remove(item);
            TabItem getTab = null;
            foreach (TabItem tab in mainTabControl.Items)
            {
                if (tab.Header.Equals("All")){
                    getTab = tab;
                }
                    
            }
            ((ObservableCollection<Task>)((ListView)getTab.Content).ItemsSource).Remove(item);
            ((ListView)((TabItem)mainTabControl.SelectedItem).Content).Items.Refresh();

            Shell.getSaveLoader().save();
        }

        private static void archiveTask(object send)
        {
            Console.WriteLine("??????????????????");
        }

        private void addTask(object sender, RoutedEventArgs e)
        {
            AddHomeworkTask setTaskWindow = new AddHomeworkTask(this);
            setTaskWindow.ShowInTaskbar = false;
            Nullable<bool> result = setTaskWindow.ShowDialog();
            if (!result.HasValue || !result.Value)
            {
                return;
            }
            String description = setTaskWindow.getTaskDescription();
            String date = setTaskWindow.getTaskDate().Year.ToString() + setTaskWindow.getTaskDate().Month.ToString() + setTaskWindow.getTaskDate().Day.ToString();
            String shortenedDate = setTaskWindow.getTaskDate().ToShortDateString();
            shortenedDate = shortenedDate.Remove(shortenedDate.Length - 5); 

            TabItem setTab = subjectsTab.SelectedItem as TabItem;
            Console.WriteLine(setTab);
            Task newTask = new Task { AssignmentName = description, AssignmentDate = date, AssignmentShortenedDate = shortenedDate };

            //((ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource).Add(newTask);
            ObservableCollection<Task> currentList = (ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource;
            if(currentList != taskGroupAll)
                sortAllTab(newTask,(ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource);
            sortAllTab(newTask, taskGroupAll);
            ((ListView)setTab.Content).Items.Refresh();
            mainTabControl = subjectsTab;
            //Console.WriteLine(((ListView)setTab.Content));
            //subjects[0].taskCollection.Add(newTask);

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
            nameColumn.Width = 180;
            nameColumn.Header = "Assignment";
            nameColumn.DisplayMemberBinding = new Binding("AssignmentName");
            dateColumn.Width = 45;
            dateColumn.Header = "Date";
            dateColumn.DisplayMemberBinding = new Binding("AssignmentShortenedDate");
            editColumn.Width = 45;
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
                archiveTask(sender);
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
            if (subjectsTab.SelectedItem == AllTab)
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
            if (subjectsTab.SelectedItem != AllTab)
            {
                subjectsTab.Items.Remove(subjectsTab.SelectedItem);
            }
        }
    }
}

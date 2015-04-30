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
        public Homework()
        {
            InitializeComponent();
            newView = new MainViewModel();
            this.DataContext = newView;
            taskGroup = new ObservableCollection<Task>();
            //subjects[0] = new Subject();
            
            AllTab = new TabItem();
            AllTab.Header = "All";
            taskGroupAll = new ObservableCollection<Task>();
            AllTab.Content = createNewList(taskGroupAll);
            subjectsTab.Items.Add(AllTab);
            mainTabControl = subjectsTab;

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
            return "Not implemented yet";
        }

        public Boolean load(String data)
        {
            //Need to de-tokenize based on String given for subject names. Delimiter is ','.

            /****** LOAD FLOW DOCUMENT *******/

            //FileStream xamlFile = new FileStream(@"savedFiles\test.xaml", FileMode.Open, FileAccess.Read);
            // and parse the file with the XamlReader.Load method.
            //FlowDocument content = XamlReader.Load(xamlFile) as FlowDocument;
            // Finally, set the Document property to the FlowDocument object that was
            // parsed from the input file.            
            //rightBox.Document = content;

            //xamlFile.Close();          

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
        }
        
        public static void deleteTask(object sender)
        {
            Task item = (Task)sender;
            Console.Write("Item Name:");
            Console.WriteLine(item.AssignmentName);
            TabItem tabitem = (TabItem) mainTabControl.SelectedItem;
            Console.WriteLine(tabitem);
            ListView listview = (ListView) tabitem.Content;
            Console.WriteLine(listview);
            int index = ((ListView)((TabItem)mainTabControl.SelectedItem).Content).Items.IndexOf(item);
            Console.Write(index);
            ((ObservableCollection<Task>)((ListView)((TabItem)mainTabControl.SelectedItem).Content).ItemsSource).Remove(item);

            ((ListView)((TabItem)mainTabControl.SelectedItem).Content).Items.Refresh();

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
            String date = setTaskWindow.getTaskDate().ToShortDateString();
            String shortenedDate = date.Remove(date.Length - 5);

            TabItem setTab = subjectsTab.SelectedItem as TabItem;
            Console.WriteLine(setTab);
            Task newTask = new Task { AssignmentName = description, AssignmentDate = date, AssignmentShortenedDate = shortenedDate };


            ((ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource).Add(newTask);
            Console.WriteLine(((ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource));
            ((ListView)setTab.Content).Items.Refresh();
            mainTabControl = subjectsTab;
            //Console.WriteLine(((ListView)setTab.Content));
            //subjects[0].taskCollection.Add(newTask);

        }

        private ListView createNewList(ObservableCollection<Task> list)
        {
            
            ListView newList = new ListView();
            GridViewColumn nameColumn = new GridViewColumn();
            GridViewColumn dateColumn = new GridViewColumn();
            GridViewColumn editColumn = new GridViewColumn();
            GridView newGrid = new GridView();
            nameColumn.Width = 190;
            nameColumn.Header = "Assignment";
            nameColumn.DisplayMemberBinding = new Binding("AssignmentName");
            dateColumn.Width = 45;
            dateColumn.Header = "Date";
            dateColumn.DisplayMemberBinding = new Binding("AssignmentShortenedDate");
            editColumn.Width = 35;
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
            if (e.RightButton == MouseButtonState.Pressed)
                e.Handled = true;
        }
    }
}

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

        ObservableCollection<Task> taskCollection;
        ObservableCollection<TabItem> subjectsCollection;
        ListView defaultListStyle;
        String tabXAML;
        StringReader readString;
        XmlReader reader;
        TabItem setTab;
        Button addButton;
        

        //ObservableCollection<Task> taskCollection;

        private class Subject
        {
            public ObservableCollection<Task> taskCollection;
            String name;

            public Subject()
            {
                taskCollection = new ObservableCollection<Task>();
            }
        }
        
        private const int MAX_SUBJECTS = 10;
        private int numSubjects = 1;
        private int totalSubjects = 1;
        Subject[] subjects = new Subject[MAX_SUBJECTS];

        public Homework()
        {
            InitializeComponent();

            subjectsCollection = new ObservableCollection<TabItem>();
            taskList.ItemsSource = new ObservableCollection<Task>();
            defaultListStyle = taskList;
            tabXAML = XamlWriter.Save(taskList);


            //taskCollection = new ObservableCollection<Task>();
            subjects[0] = new Subject();
            taskList.ItemsSource = subjects[0].taskCollection; //This needs to be scalable to multiple xaml elements.
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Task_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
            newTab.Header = display.getSubject(); //Need to make sure we cannot exit window until Submit or Cancel is pressed.
            readString = new StringReader(tabXAML);
            reader = XmlReader.Create(readString);
            ListView newList = (ListView)XamlReader.Load(reader);
            
            addButton = (Button) newList.FindName("deleteButton");
            if (addButton != null)

            {
                Console.WriteLine("Is Not NULL WAHOO!!");
                addButton.Click += new RoutedEventHandler(deleteTask);
            }
            addButton = (Button) LogicalTreeHelper.FindLogicalNode(newList, "archiveButton") as Button;
            if(addButton != null)
                addButton.Click += new RoutedEventHandler(archiveTask);
            
            taskCollection = new ObservableCollection<Task>();
            newList.ItemsSource = taskCollection;
            newTab.Content = newList;
            subjectsTab.Items.Add(newTab);
            reader.Close();
        }

        private void RichTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }


        private void deleteTask(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("SJDBFGJSDGSD");
            Task item = (Task) (sender as Button).DataContext;

            int index = ((ListView)((TabItem)subjectsTab.SelectedItem).Content).Items.IndexOf(item);
            ((ObservableCollection<Task>)((ListView)((TabItem)subjectsTab.SelectedItem).Content).ItemsSource).Remove(item);

            ((ListView)((TabItem)subjectsTab.SelectedItem).Content).Items.Refresh();
           
        }

        private void archiveTask(object send, RoutedEventArgs e)
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
            ((ObservableCollection<Task>)((ListView)setTab.Content).ItemsSource).Add(new Task { AssignmentName = description, AssignmentDate = date, AssignmentShortenedDate = shortenedDate });
            ((ListView)setTab.Content).Items.Refresh();

            Console.WriteLine(date);
            subjects[0].taskCollection.Add(new Task { AssignmentName = description, AssignmentDate = date, AssignmentShortenedDate = shortenedDate });
            taskList.Items.Refresh();

        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


    }
}

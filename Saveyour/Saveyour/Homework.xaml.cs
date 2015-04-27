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
        
        private const int MAX_SUBJECTS = 5;
        private int numSubjects = 0;
        private int totalSubjects = 0;
        Subject[] subjects = new Subject[MAX_SUBJECTS];

        public Homework()
        {
            InitializeComponent();
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
            return null;
        }

        public Boolean load(String data)
        {
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
            if (totalSubjects == MAX_SUBJECTS)
            {
                Console.WriteLine("Max Subjects reached!");
                return;
            }
            if (numSubjects < MAX_SUBJECTS)
            {
                hWindow.Width += 325;
                homeworkPanel.Width += 310;

                windowGrid.Width = hWindow.Width;
                double marginLeft = addSubjectButton.Margin.Left;
                addSubjectButton.Margin = new Thickness(marginLeft += 325, 10, 0, 0);

                string gridXaml = XamlWriter.Save(panelGrid);

                StringReader stringReader = new StringReader(gridXaml);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                Grid newGrid = (Grid)XamlReader.Load(xmlReader);

                homeworkPanel.Children.Add(newGrid);
                numSubjects++;
                totalSubjects++;
            }
            else
            {
                hWindow.Height += homeworkPanel.Height;
                windowGrid.Height += homeworkPanel.Height;
                homeworkPanel.Height += homeworkPanel.Height;
                homeworkPanel.Orientation = System.Windows.Controls.Orientation.Vertical;
                homeworkPanel.Children.Add(createSubject());
                numSubjects = 0;
                totalSubjects++;
                homeworkPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;
            }

        }

        private void RichTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private Grid createSubject()
        {
            Grid newGrid = new Grid();
            newGrid.Height = panelGrid.Height;
            newGrid.Width = panelGrid.Width;
            
            Label newAssignmentLabel = new Label();
            Label newDateLabel = new Label();
            TextBox newSubjectBox = new TextBox();
            ListView newList = new ListView();
            newList.HorizontalAlignment = taskList.HorizontalAlignment;
            newList.VerticalAlignment = taskList.VerticalAlignment;
            newList.Width = taskList.Width;
            newList.Height = taskList.Height;
            newList.Margin = new Thickness(taskList.Margin.Left,taskList.Margin.Top, taskList.Margin.Right, taskList.Margin.Bottom);
            newList.HorizontalContentAlignment = taskList.HorizontalContentAlignment;

            newSubjectBox.Text = subjectBox.Text;
            newSubjectBox.HorizontalAlignment = subjectBox.HorizontalAlignment;
            newSubjectBox.VerticalAlignment = subjectBox.VerticalAlignment;
            newSubjectBox.VerticalContentAlignment = subjectBox.VerticalContentAlignment;
            newSubjectBox.Width = subjectBox.Width;
            newSubjectBox.Height = subjectBox.Height;
            newSubjectBox.HorizontalContentAlignment = subjectBox.HorizontalContentAlignment;
            newSubjectBox.Margin = new Thickness(subjectBox.Margin.Left, subjectBox.Margin.Top, subjectBox.Margin.Right, subjectBox.Margin.Bottom);
            
            newGrid.Children.Add(newSubjectBox);
            newGrid.Children.Add(newList);

            /******* Adding logic for saving each subject into data structure *******/            

            //subjects[numSubjects - 1] = newSubject;            
            return newGrid;
        }

        private void deleteTask(object sender, RoutedEventArgs e)
        {
            Task item = (Task) (sender as Button).DataContext;
            int index = taskList.Items.IndexOf(item);
            subjects[0].taskCollection.Remove(item);

            taskList.Items.Refresh();
           
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
            Console.WriteLine(description);
            String date = setTaskWindow.getTaskDate().ToShortDateString();
            String shortenedDate = date.Remove(date.Length - 5);
            Console.WriteLine(date);
            subjects[0].taskCollection.Add(new Task { AssignmentName = description, AssignmentDate = date, AssignmentShortenedDate = shortenedDate });
            taskList.Items.Refresh();
        }

        private void leftTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mouseEvent(object sender, MouseEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            Console.WriteLine(item);
        }

        private void subjectBox_DClick(object sender, System.EventArgs e)
        {
            subjectBox.Text = "";
        }

    }
}

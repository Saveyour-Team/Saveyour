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
    /// Interaction logic for Homework.xaml
    /// </summary>
    public partial class Homework : Window, Module
    {
        private class Subject
        {

        }

        private const int MAX_SUBJECTS = 5;
        private int numSubjects = 1;

        GroupBox[] subjects = new GroupBox[MAX_SUBJECTS];

        public Homework()
        {
            InitializeComponent();
            maxWarning.Visibility = Visibility.Hidden;

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
            String returnThis = "";

            for (int i = 0; i < numSubjects; i++)
            {
                returnThis += "{" + subjects[i].Name + "}";
                //returnThis += "{" + subjects[i].TextInput + "}";
            }
               
            return "";
        }

        public Boolean load(String data)
        {

            return false;
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
            if (numSubjects < MAX_SUBJECTS)
            {
                hWindow.Width += 300;
                homeworkPanel.Width += 310;

                windowGrid.Width = hWindow.Width;
                double marginLeft = addSubjectButton.Margin.Left;
                addSubjectButton.Margin = new Thickness(marginLeft += 300, 10, 0, 0);

                homeworkPanel.Children.Add(createSubject());
                numSubjects++;
            }
            else
            {
                maxWarning.Visibility = Visibility.Visible;
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

            newAssignmentLabel.Content = assignmentLabel.Content;
            newAssignmentLabel.HorizontalAlignment = assignmentLabel.HorizontalAlignment;
            newAssignmentLabel.VerticalAlignment = assignmentLabel.VerticalAlignment;
            newAssignmentLabel.Width = assignmentLabel.Width;
            newAssignmentLabel.Height = assignmentLabel.Height;
            newAssignmentLabel.Margin = new Thickness(assignmentLabel.Margin.Left,assignmentLabel.Margin.Top,assignmentLabel.Margin.Bottom,assignmentLabel.Margin.Right);

            newDateLabel.Content = dateLabel.Content;
            newDateLabel.HorizontalAlignment = dateLabel.HorizontalAlignment;
            newDateLabel.VerticalAlignment = dateLabel.VerticalAlignment;
            newDateLabel.Width = dateLabel.Width;
            newDateLabel.Height = dateLabel.Height;
            newDateLabel.Margin = new Thickness(dateLabel.Margin.Left,dateLabel.Margin.Top,dateLabel.Margin.Bottom,dateLabel.Margin.Right);

            newSubjectBox.Text = subjectBox.Text;
            newSubjectBox.HorizontalAlignment = subjectBox.HorizontalAlignment;
            newSubjectBox.VerticalAlignment = subjectBox.VerticalAlignment;
            newSubjectBox.VerticalContentAlignment = subjectBox.VerticalContentAlignment;
            newSubjectBox.Width = subjectBox.Width;
            newSubjectBox.Height = subjectBox.Height;
            newSubjectBox.HorizontalContentAlignment = subjectBox.HorizontalContentAlignment;
            newSubjectBox.Margin = new Thickness(subjectBox.Margin.Left, subjectBox.Margin.Top, subjectBox.Margin.Bottom, subjectBox.Margin.Right);

            RichTextBox leftBox = new RichTextBox();
            RichTextBox rightBox = new RichTextBox();
            leftBox.HorizontalAlignment = leftTextBox.HorizontalAlignment;
            leftBox.VerticalAlignment = leftTextBox.VerticalAlignment;
            leftBox.Height = leftTextBox.Height;
            leftBox.Width = leftTextBox.Width;
            leftBox.Margin = new Thickness(leftTextBox.Margin.Left,leftTextBox.Margin.Top,leftTextBox.Margin.Bottom,leftTextBox.Margin.Right);
            
            rightBox.HorizontalAlignment = rightTextBox.HorizontalAlignment;
            rightBox.VerticalAlignment = rightTextBox.VerticalAlignment;
            rightBox.Height = rightTextBox.Height;
            rightBox.Width = rightTextBox.Width;
            rightBox.Margin = new Thickness(rightTextBox.Margin.Left,rightTextBox.Margin.Top,rightTextBox.Margin.Bottom,rightTextBox.Margin.Right);

            newGrid.Children.Add(leftBox);
            newGrid.Children.Add(rightBox);
            newGrid.Children.Add(newAssignmentLabel);
            newGrid.Children.Add(newDateLabel);
            newGrid.Children.Add(newSubjectBox);

            /******* Adding logic for saving each subject into data structure *******/


            return newGrid;
        }

        private void leftTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}

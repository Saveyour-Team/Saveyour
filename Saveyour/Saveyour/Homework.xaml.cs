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
        private const int MAX_SUBJECTS = 7;
        private int numSubjects = 0;

        GroupBox first = null;
        Window parent = null;
        Grid wholeGrid = null;

        GroupBox[] subjects = new GroupBox[MAX_SUBJECTS];

        public Homework()
        {
            InitializeComponent();

            first = (GroupBox)FindName("subject1");
            parent = (Window)FindName("hWindow");
            wholeGrid = (Grid)FindName("HGrid");

            //We need to load the information for the subjects here.

            //After loading the information, we increase numSubjects and take the data from load to construct each GroupBox for each Grid

            //Load the GroupBox, then the Grid, then each task in Task, then each date in Date

            //

            //wholeGrid.Height = 30;

            Window testWindow = new Window()
            {
                Title = "TESTING WINDOW",
                Height = 300,
                Width = 300                                
            };
            
            testWindow.Show();

            wholeGrid.Children.Add(new GroupBox()
            {
                //Name = "SUBJECT TEST"
                Header = "SUBJECT TEST"
            });
            
            
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
    }
}

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
using Saveyour;

namespace TaskBatch
{
    /// <summary>
    /// Interaction logic for TaskBatchWindow.xaml
    /// </summary>
    internal partial class TaskBatchWindow : Window
    {

        WeeklyToDo WeeklyInstance;
        LinkedList<Saveyour.Task> tasklist = new LinkedList<Saveyour.Task>();
        
        internal TaskBatchWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //Adding all the logic here
            //get weekly to do
            WeeklyInstance = Shell.getWeeklyToDo(); //This is always null because of the way plugins are loaded. 

            Debug.WriteLine("INSTANCE: " + WeeklyInstance);
           
        }

        
        private List<DateTimeSum> ListOfDays(){
            WeeklyInstance = Shell.getWeeklyToDo();
            List<DateTimeSum> l = new List<DateTimeSum>();
            DateTime d = DateTime.Today;
            DateTime stop = d.AddDays(14);
            
            while(d<=stop){
                int tasksWeight = WeeklyInstance.sumOfTaskWeights(d);  
                DateTimeSum temp = new DateTimeSum();
                temp.setDateTime(d);
                temp.setDayWeight(WeeklyInstance.sumOfTaskWeights(d));
                l.Add(temp);


            }
            return sortList(l);
        }

        private List<DateTimeSum> sortList(List<DateTimeSum> l){
            //List<DateTimeSum> newList = new List<DateTimeSum>();
            int lsize = l.Count;
            int min; //min is the smallest
            for(int i=0; i<lsize; i++){
                min = i;
                for (int j = i+1; j < lsize; j++) { 
                    if(l[j].compareTo(l[min])==-1){
                        min = j;
                    }
                }
                if (min != i) {
                    l = swap(l,i,min);
                }
            }
            return l;//sorted list
        }

        private List<DateTimeSum> swap(List<DateTimeSum> l, int p1, int p2) {
            DateTimeSum temp = l[p1];
            DateTimeSum temp2 = l[p2];
            l[p1] = temp2;
            l[p2] = temp;
            return l;
            
        }

        private void addTaskButton(object sender, RoutedEventArgs e)
        {
            WeeklyInstance = Shell.getWeeklyToDo(); 
            Saveyour.Task task = WeeklyInstance.selectAddNoDateTask(e);//getting task from WeeklyInstance

            if (task != null)
            {
                StackPanel taskStack = new StackPanel();

                TextBlock taskLabel = new TextBlock();
                if (taskLabel != null)
                { 
                    taskLabel.Text = task.getTitle();
                    taskLabel.Margin = new Thickness(50, 0, 0, 0);

                    taskStack.Children.Add(taskLabel);
                    
                    AvailableDates.Children.Add(taskStack);
                    tasklist.AddLast(task);//adds task to end of taskList
                }
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

        private void addWeeklyToDo_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationWindow confirm = new ConfirmationWindow();

            //CALCULATE THE IDEAL DAY TO PLACE TASKS
            List<DateTimeSum> allDays = sortList(ListOfDays());//returns a sorted lists with all the days for the next 2 weeks in ascending order from lowest to highest task weight for day
            DateTimeSum idealDay = allDays[0];//gets index at the top, which is the one with the least task weight
            DateTime toAdd = idealDay.getDateTime(); //This is the ideal date to add the task to. Want to add Task to this Date
            String date = toAdd.ToString();
            confirm.displayMessage("Would you like to add the tasks to the date\n " + date);
            
            bool? result = confirm.ShowDialog();
            if (result == true)
            {
                AvailableDates.Children.Clear();
                
                // ADD THE TASKS AT THE DATE
                foreach(Saveyour.Task t in tasklist) {
                    WeeklyInstance.addTask(t);//adds task to weekly instance
                }
            }
        }

    }

    internal class DateTimeSum {
        
        DateTime d;
        int sum;


        public void setDateTime(DateTime d){
            this.d = d;
        }

        public void setDayWeight(int s){
            this.sum = s;
        }
    
        public DateTime getDateTime(){
            return this.d;
        }

        public int getDayWeight(){
            return this.sum;
        }

        public bool Equals(DateTimeSum ds2){
            int s1 = this.getDayWeight();
            int s2 = ds2.getDayWeight();
            DateTime d1 = this.getDateTime();
            DateTime d2 = ds2.getDateTime();

            if(s1==s2 && d1.Equals(d2)){
                return true;
            } else {
                return false;
            }
        }

        //ok, so for compareTo- 0 is equals, 1 is greater than, and -1 is less than 
        public int compareTo(DateTimeSum ds2){          
            int s1 = this.getDayWeight();
            int s2 = ds2.getDayWeight();
            DateTime d1 = this.getDateTime();
            DateTime d2 = ds2.getDateTime();
            if(this.Equals(ds2)){
                return 0;
            }
            else if(s1<s2){
                return -1;
            }
            else {
                return 1;
            }


        }



    }

   
 }

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

        internal TaskBatchWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //Adding all the logic here
            //get weekly to do
            WeeklyInstance = Shell.getWeeklyToDo(); //This is always null because of the way plugins are loaded. 
            //WeeklyInstance.sumOfTaskWeights();
            //Pull Weekly Data
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
            return l;
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
            return l;
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
            Saveyour.Task task = WeeklyInstance.selectAddNoDateTask(e);

            //CALCULATE THE BEST DATE HERE
            ConfirmationWindow confirm = new ConfirmationWindow();

            confirm.displayMessage("Would you like to add the task  to the date\n " + "(the date goes here)");

            if (task != null)
            {
                bool? result = confirm.ShowDialog();
                if (result == true)
                {
                    // ADD THE TASK AT THE DATE
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

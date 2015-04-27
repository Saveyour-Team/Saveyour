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
using Saveyour;

namespace TaskBatch
{
    /// <summary>
    /// Interaction logic for TaskBatchWindow.xaml
    /// </summary>
    internal partial class TaskBatchWindow : Window, IComparable<DateTimeSum>
    {

        WeeklyToDo WeeklyInstance;

        internal TaskBatchWindow()
        {
            InitializeComponent();
            //Adding all the logic here
            //get weekly to do
            WeeklyInstance = Shell.getWeeklyToDo();
            //WeeklyInstance.sumOfTaskWeights();
            //Pull Weekly Data

        
        }

        private List<DateTimeSum> ListOfDays(DateTime start, DateTime stop){
            List<DateTimeSum> l = new List<DateTimeSum>();
            DateTime d = start;
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
            DateTimeSum newList = new DateTimeSum();
            int lsize = l.Count;
            foreach(DateTimeSum d in l){
            
            }

            return l;
        }
        

    }

    internal class DateTimeSum : IComparable{
        
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

            if(s1==s2 && d1.Equals(d2){
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

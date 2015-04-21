using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saveyour
{
    public class Task
    {
        private String title, description;
        private int weight;
        private DateTime date;

        public Task(String Title, String Description, int Weight, DateTime Date)
        {
            title = Title;
            description = Description;
            weight = Weight;
            date = Date;
        }

        public DateTime getDate()
        {
            return date;
        }

        public String getTitle()
        {
            return title;
        }

        public String getDescription()
        {
            return description;
        }


        public int getWeight()
        {
            return weight;
        }

        public void setTitle(String newTitle)
        {
            title = newTitle;
        }

        public void setDescription(String newDescription)
        {
            description = newDescription;
        }

        public void setWeight(int newWeight)
        {
            weight = newWeight;
        }

        public void setDate(DateTime newDate)
        {
            date = newDate;
        }
    }
}

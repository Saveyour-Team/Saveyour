using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Saveyour
{
    public partial class Feedback : Form
    {

        /*
         * For now, this class window is just here as a placeholder that will display data from the database.
         * For now it will only confirm that you have logged in and nothing more.
         */

        Form parent = null;
        
        public Feedback(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            label1.Text = Login.username + " has logged in";
            label2.Text = Login.username + " likes " + Login.data;
            //ReadWrite.write(Login.username + " likes " + Login.data); Testing for writing data.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (parent != null)
            {
                parent.Dispose();
                this.Dispose(); //disposing the parent should make sure that this doesn't run; was unsure if this was needed
            }
            else
            {
                this.Dispose();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.AutoReset = false;
            t.Elapsed += new ElapsedEventHandler(hideSaved);
            t.Start();

            ReadWrite.write(label2.Text);
            label3.Show();
            
        }
      
        private void hideSaved(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                label3.Hide();
            });
        }
    }
}

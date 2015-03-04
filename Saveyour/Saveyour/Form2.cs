using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saveyour
{
    public partial class Form2 : Form
    {

        /*
         * For now, this class window is just here as a placeholder that will display data from the database.
         * For now it will only confirm that you have logged in and nothing more.
         */

        Form parent;
        public Form2() 
        {
            InitializeComponent();
        }

        /*
         * A specialized constructor was created so that we can destroy the login from this new window.
         * This may not be relevant later because right now you can think of Form1 as being the parent of Form2
         * When Form1 is destroyed, Form2 is also destroyed. Right now it just hides Form1 and destroys it when
         * Form2 is no longer need. 
         * 
         * We may want to find a way to create Form2 without making Form1 the parent.
         * */
        public Form2(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            label1.Text = Form1.username + " has logged in";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.Dispose();
            this.Dispose(); //disposing the parent should make sure that this doesn't run; was unsure if this was needed
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

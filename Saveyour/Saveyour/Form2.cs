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

        public Form2(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            label1.Text = Form1.username + " has logged in";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.Dispose();
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

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
         * For now, this class window is just here as a placeholder
         * This placeholder is to be filled in with the next window that should be created
         * after logging in. For now it will only confirm that you have logged in and nothing more.
         * */

        Form parent;
        public Form2() 
        {
            InitializeComponent();
        }

        public Form2(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.Dispose();
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
    }
}

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
    public partial class Quicknotes : Form, Module
    {
        public Quicknotes()
        {
            InitializeComponent();
        }

        public String save()
        {      
            return textBox1.Text;
        }

        public Boolean load(String data)
        {
            textBox1.Text = data;
            return true;
        }       

        public String moduleID()
        {
            return "Quicknotes";   
        }

        public Boolean update()
        {
            return false;
        }

        public Boolean Equals(Module other)
        {
            return (moduleID().Equals(other.moduleID()));
        }

    }
}

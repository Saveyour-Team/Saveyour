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
            return false;
        }

        public String[] getDates()
        {
            return null;
        }

        public String moduleID()
        {
            return null;   
        }

        public Boolean update()
        {
            return false;
        }

    }
}

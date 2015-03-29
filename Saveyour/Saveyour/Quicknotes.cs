using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Saveyour
{
    public partial class Quicknotes : Form, Module
    {

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Shell.getSaveLoader().save();
            Debug.WriteLine("Quicknotes closed");
            if (Application.OpenForms.Count == 0)
            {
                Application.Exit();
            }
         
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            Shell.getSaveLoader().save();
        }
        public Quicknotes()
        {
            InitializeComponent();
            this.FormClosing += OnFormClosing;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
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

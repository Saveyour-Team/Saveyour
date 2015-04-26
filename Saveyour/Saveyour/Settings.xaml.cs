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
using System.Windows.Forms;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, Module
    {
        QuicknotesControl qnotes;
        Window weeklytd;
        Window gcalendar;
	
	private KeyboardHook keyHook = new KeyboardHook();
	
        public Settings()
        {
            InitializeComponent();            
       		// Create a listener for hotkeys
       		keyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(key_pressed);
       		// Register Alt+F12 as a hotkey
        	keyHook.RegisterHotKey(ModifierKeys.Alt,Keys.D0);
	}

	private void key_pressed(object sender, KeyPressedEventArgs e){
		if (e.Key.Equals(Keys.D0) && e.Modifier.Equals(ModifierKeys.Alt)){
			toggleAll();
		}

	}
	
	private void toggleAll(){	
                qnotes.ToggleVisibility();
            
            if (weeklytd.IsVisible)
            {
                weeklytd.Hide();
            }
            else if (weeklytd.IsLoaded) 
            {
                weeklytd.Show();
                weeklytd.Activate();
                //weeklytd.Topmost = true;
            }

            if (gcalendar.IsVisible) 
            {
                gcalendar.Hide();
            }
            else if (gcalendar.IsLoaded) 
            {
                gcalendar.Show();
                gcalendar.Activate();
                //gcalendar.Topmost = true;
            }

            if (this.IsVisible)
            {
                this.Hide();
            }
            else if (this.IsLoaded)
            {
                this.Show();
                this.Activate();
                //this.Topmost = true;
            }
	}

        private void QN_Click(object sender, RoutedEventArgs e)
        {
            qnotes.ToggleVisibility();
        }

        private void WTD_Click(object sender, RoutedEventArgs e)
        {
            if (weeklytd.IsVisible)
                weeklytd.Hide();
            else if (weeklytd.IsLoaded)
                weeklytd.Show();
        }

        private void GC_Click(object sender, RoutedEventArgs e)
        {
            if (gcalendar.IsVisible)
                gcalendar.Hide();
            else if (gcalendar.IsLoaded)
                gcalendar.Show();
        }

        public void addQNotes(Module qnc)
        {
            qnotes = (QuicknotesControl) qnc;
        }

        public void addWTD(Window module)
        {
            weeklytd = module;
        }

        public void addGC(Window module)
        {
            gcalendar = module;
        }

        public String moduleID()
        {
            return "Settings";
        }

        public Boolean update()
        {
            return false;
        }

        public String save()
        {
            return "";
        }

        public Boolean load(String data)
        {
            return false;
        }

        public Boolean Equals(Module other)
        {
            return false;
        }

        
    }
}

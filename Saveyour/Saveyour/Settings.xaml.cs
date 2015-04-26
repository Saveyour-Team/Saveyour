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

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, Module
    {
        Window qnotes;
        Window weeklytd;
        Window gcalendar;
	
	private KeyboardHook keyHook = new KeyBoardHook();
	
        public Settings()
        {
            InitializeComponent();            
       		// Create a listener for hotkeys
       		keyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(key_pressed);
       		// Register Alt+F12 as a hotkey
        	keyHook.RegisterHotKey(ModifierKeys.Alt,Keys.F12);
	}

	private key_pressed(object sender, KeyPressedEventArgs e){
		if (e.Key().Equals(Keys.F12) && e.Modifier().Equals(ModifierKeys.Alt)){
			toggleAll();
		}

	}
	
	private void toggleAll(){	
            if (qnotes.IsVisible)
                qnotes.Hide();
            else if (qnotes.IsLoaded)
                qnotes.Show();

            if (weeklytd.IsVisible)
                weeklytd.Hide();
            else if (weeklytd.IsLoaded)
                weeklytd.Show();


            if (gcalendar.IsVisible)
                gcalendar.Hide();
            else if (gcalendar.IsLoaded)
                gcalendar.Show();
	}

        private void QN_Click(object sender, RoutedEventArgs e)
        {
            if (qnotes.IsVisible)
                qnotes.Hide();
            else if (qnotes.IsLoaded)
                qnotes.Show();
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

        public void addQNotes(Window module)
        {
            qnotes = module;
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

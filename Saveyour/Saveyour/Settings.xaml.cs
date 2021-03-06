﻿using System;
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
using System.Reflection;
using System.Diagnostics;
using PluginContracts;
using SaveyourUpdate;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, Module, SaveyourUpdatable
    {
        QuicknotesControl qnotes;
        Window weeklytd;
        Window gcalendar;
        Window homework;

        SaveyourUpdater updater;

        bool toggledAll;
       
        //This allows for us to globally bind a key in Windows to a macro
	    private KeyboardHook keyHook = new KeyboardHook(); 
	
        public Settings()
        {
            toggledAll = false;

            InitializeComponent();            
       		// Create a listener for hotkeys
       		keyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(key_pressed);
       		
            // Register Ctrl + 0 as a hotkey
        	keyHook.RegisterHotKey(ModifierKeys.Control,Keys.D0);

            keyHook.RegisterHotKey(ModifierKeys.Control, Keys.D1);

            keyHook.RegisterHotKey(ModifierKeys.Control, Keys.D2);

            keyHook.RegisterHotKey(ModifierKeys.Control, Keys.D3);

            keyHook.RegisterHotKey(ModifierKeys.Control, Keys.D4);
            
            updater = new SaveyourUpdater(this);

            Left = 0;
            double taskBar = Convert.ToDouble((Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height).ToString());
            Top = System.Windows.SystemParameters.PrimaryScreenHeight - (taskBar + this.Height);

	    }
        	    
        /***** HOTKEY BINDING LOGIC *****/	

	    private void toggleAll(){
            Dictionary<string, IPlugin> plugins = Shell.getPlugins();
            foreach (KeyValuePair<string, IPlugin> plugin in plugins)
            {
                if (plugin.Value.getInstance() != null)
                {
                    if (plugin.Value.getInstance().IsVisible && !toggledAll)
                    {
                        plugin.Value.getInstance().Hide();
                    }
                    else if (toggledAll)
                        plugin.Value.getInstance().Show();
                }
            }

            if (qnotes.getVisibility() && !toggledAll) {
                qnotes.Hide();
            }
            else if (toggledAll)
            {
                qnotes.Show();
            }
            
            if (weeklytd.IsVisible && !toggledAll)
            {
                weeklytd.Hide();
            }
            else if (weeklytd.IsLoaded && toggledAll) 
            {
                weeklytd.Show();
                weeklytd.Activate();
                //weeklytd.Topmost = true;
            }

            if (gcalendar.IsVisible && !toggledAll) 
            {
                gcalendar.Hide();
            }
            else if (gcalendar.IsLoaded && toggledAll) 
            {
                gcalendar.Show();
                gcalendar.Activate();
                //gcalendar.Topmost = true;
            }

            if (homework.IsVisible && !toggledAll)
            {
                homework.Hide();
            }
            else if (homework.IsLoaded && toggledAll)
            {
                homework.Show();
                homework.Activate();
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

        /***** END OF HOTKEY BINDING LOGIC *****/


        /***** LOGIC FOR RETRIEVING MODULES FROM SHELL *****/
        
        public void addQNotes(Module qnc)
        {
            qnotes = (QuicknotesControl) qnc;
        }

        public QuicknotesControl getQNotes()
        {
            return qnotes;
        }

        public void addWTD(Window module)
        {
            weeklytd = module;
        }

        public Window getWTD()
        {
            return weeklytd;
        }

        public void addGC(Window module)
        {
            gcalendar = module;
        }

        public void addHW(Window module)
        {
            homework = module;
        }

        public Window getHW()
        {
            return homework;
        }

        /***** END OF LOGIC FOR RETRIEVING MODULES FROM SHELL *****/


        /***** MODULE INTERFACE AND QUICK INFORMATION METHODS *****/

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

        public string ApplicationName
        {
            get { return "Saveyour"; }
        }

        public string ApplicationID
        {
            get { return "Saveyour"; }
        }

        public Assembly ApplicationAssembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        public Uri UpdateXmlLocation
        {
            get { return new Uri("https://github.com/Saveyour-Team/Release/raw/master/SaveyourXML.xml"); }
            //This should be the XML file found on github. This must be the RAW version so that it will start downloading.
            //This XML file is also in the release repo so it will look for the latest update possible.
        }

        /***** END OF MODULE INTERFACE AND QUICK INFORMATION METHODS *****/


        /***** LOGIC FOR DATA BINDINGS XAML ELEMENTS *****/

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            updater.doUpdate();
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

        private void HW_Click(object sender, RoutedEventArgs e)
        {
            if (homework.IsVisible)
                homework.Hide();
            else if (homework.IsLoaded)
                homework.Show();
        }

        private void key_pressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key.Equals(Keys.D0) && e.Modifier.Equals(ModifierKeys.Control))
            {
                toggleAll();
                toggledAll = !toggledAll;
            }
            else if (e.Key.Equals(Keys.D1) && e.Modifier.Equals(ModifierKeys.Control))
            {
                if (qnotes.getVisibility())
                {
                    qnotes.Hide();
                }
                else
                {
                    qnotes.Show();
                }
            }
            else if (e.Key.Equals(Keys.D2) && e.Modifier.Equals(ModifierKeys.Control))
            {
                if (weeklytd.IsVisible)
                    weeklytd.Hide();
                else if (weeklytd.IsLoaded)
                    weeklytd.Show();
            }
            else if (e.Key.Equals(Keys.D3) && e.Modifier.Equals(ModifierKeys.Control))
            {
                if (gcalendar.IsVisible)
                    gcalendar.Hide();
                else if (gcalendar.IsLoaded)
                    gcalendar.Show();
            }
            else if (e.Key.Equals(Keys.D4) && e.Modifier.Equals(ModifierKeys.Control))
            {
                if (homework.IsVisible)
                    homework.Hide();
                else if (homework.IsLoaded)
                    homework.Show();
            }



        }


        private void exitBtn_clicked(object sender, RoutedEventArgs e)
        {
            Shell.getSaveLoader().save();
            System.Windows.Application.Current.Shutdown();

        }

        private void setHotkey_clicked(object sender, RoutedEventArgs e)
        {

        }

        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed)
                e.Handled = true;
        }

        private void AddQN_Click(object sender, RoutedEventArgs e)
        {
            qnotes.addQuickNote();
        }

        /***** END OF LOGIC FOR DATA BINDINGS XAML ELEMENTS *****/
        
    }
}

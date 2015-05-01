using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.IO;
using PluginContracts;

namespace Saveyour
{
    public class Shell{
    
        private static Modlist modlist;
        private static SaveLoader saveLoad;
        private static Shell theShell;
        private static Settings settings;
        private static Dictionary<string, IPlugin> _Plugins;
        private static WeeklyToDo weeklyToDo;
        public static Homework gg;
        public static bool loadPluginModules()
        {
            //ICollection<IPlugin> plugins = PluginLoader.LoadPlugins("Plugins");
            string path = Directory.GetCurrentDirectory();

            Debug.WriteLine("PATH: " + path);

            if (!Directory.Exists(path + "/Modules"))
            {
                Debug.WriteLine("Modules Folder does not exist");
                Directory.CreateDirectory("Modules");
            }
            try
            {
                ICollection<IPlugin> plugins = GenericPluginLoader<IPlugin>.LoadPlugins("Modules");
                foreach (var item in plugins)
                {
                    _Plugins.Add(item.Name, item);
                    Debug.WriteLine("Added" + item.Name);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /* This method is run when the application closes.  We use it to force one last save of the user data. */
        static void OnProcessExit(object sender, EventArgs e)
        {
            getSaveLoader().save();
            Debug.WriteLine("Saving before exit...");
        }

        /**Returns the Shell or creates a new one if it does not exist.  This enforces the singleton pattern. */
        public static Shell getShell()
        {
            if (theShell == null)
            {
                theShell = new Shell();
            }
            return theShell;
        }

        /**Returns the Shell or creates a new one if it does not exist.  This enforces the singleton pattern.  
         * This method differs from the one above by allowing a username and password to be entered for saving to the server if this is the first call of getShell.
         * Otherwise it just returns the already existing shell.
         */
        public static Shell getShell(String username, String password)
        {
            if (theShell == null)
            {
                theShell = new Shell(username, password);
            }
            return theShell;
        }

        /*
         * Launches the module corresponding to the given modID and adds it to modlist.  This also checks to make sure duplicate modules of the same type are not launched.
         */
        public static Module launch(String modID)
        {
            //Run 'modID' + '.exe' in the SaveYour/Modules folder to be implemented later.
            Module newModule;
            if (modID.Equals("QuicknotesControl") && !modlist.hasName("QuicknotesControl"))
            {
                newModule = new QuicknotesControl();
                
            }
            else if (modID.Equals("WeeklyToDo") && !modlist.hasName("WeeklyToDo"))
            {
               newModule = new WeeklyToDo();
               weeklyToDo = (WeeklyToDo)newModule;
            }
            else if (modID.Equals("Google Calendar") && !modlist.hasName("Google Calendar"))
            {
                newModule = new GoogleCalendar();
            }
            else if (modID.Equals("Homework"))
            {
                newModule = new Homework();
                gg = (Homework)newModule;
            }
            else 
            {
                Debug.WriteLine("Shell attempted to launch an invalid moduleID: " + modID);
                return null;
            }

            Debug.WriteLine("Launching: "+modID);

            if ((newModule != null) && modlist.add((Module)newModule))
            {
                if (modID.Equals("QuicknotesControl")){
                    settings.addQNotes(newModule);
                }
                else if (modID.Equals("WeeklyToDo"))
                {
                    settings.addWTD((Window) newModule);
                }
                else if (modID.Equals("Google Calendar"))
                {
                    settings.addGC((Window) newModule);
                }
                else if (modID.Equals("Homework"))
                {
                    settings.addHW((Window)newModule);
                }
                newModule.Show();
            }
         
            return (Module)newModule;
        }
        
        /** This private constructor creates a shell with no username and password.  It's private to enforce the Singleton pattern, and is called by getShell when appropriate.*/
        private Shell() : this(null,null)
        {

            modlist = new Modlist();
 
            saveLoad = new SaveLoader();
            settings = new Settings();

            Debug.WriteLine("Booting other modules");
            saveLoad.load();
            settings.Show();


        }

        /** This private constructor creates a shell with the given username and password.  It's private to enforce the Singleton pattern, and is called by getShell when appropriate.*/
        private Shell(String username, String password)
        {

            Shell._Plugins = new Dictionary<string, IPlugin>();

            bool hasPlugins = loadPluginModules();

            if (hasPlugins)
            {
                foreach (KeyValuePair<string, IPlugin> module in _Plugins)
                {

                    if (module.Value != null)
                    {
                        module.Value.Do();
                    }
                }
            }

            modlist = new Modlist();

            saveLoad = new SaveLoader();
            saveLoad.setLogin(username, password);
            settings = new Settings();

            Debug.WriteLine("Booting other modules");
            
            //saveLoad.loadToLaunch();
            //weeklyToDo = null;

            

            //launch("Quicknotes");
            /*
            launch("WeeklyToDo");
            launch("Google Calendar");

            launch("Homework");
            launch("QuicknotesControl");
            */
            saveLoad.load();
            
            settings.Show();
        }

        /** Returns the modList used by the shell to keep track of running modules*/
        public static Modlist getModList()
        {
            return modlist;
        }

        /** Returns the SaveLoader instance that is used to save all module data*/
        public static SaveLoader getSaveLoader(){
            return saveLoad;
        }


        public static Dictionary<string, IPlugin> getPlugins()
        {
            return _Plugins;
        }

        public static WeeklyToDo getWeeklyToDo() {
            return weeklyToDo;
        }

        public void startApp(){            

            //Application.Run(userLogin);
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;



namespace Saveyour
{
    public class Shell{
    
        private static Modlist modlist;
        private static SaveLoader saveLoad;
        private static Shell theShell;
        private static Settings settings;

        static void OnProcessExit(object sender, EventArgs e)
        {
            getSaveLoader().save();
            Debug.WriteLine("Saving before exit...");
        }

        public static Shell getShell()
        {
            if (theShell == null)
            {
                theShell = new Shell();
            }
            return theShell;
        }

        public static Shell getShell(String username, String password)
        {
            if (theShell == null)
            {
                theShell = new Shell(username, password);
            }
            return theShell;
        }

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
            }
            else if (modID.Equals("Google Calendar") && !modlist.hasName("Google Calendar"))
            {
                newModule = new GoogleCalendar();
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
                newModule.Show();
            }
         
            return (Module)newModule;
        }

        private Shell() : this(null,null)
        {

            modlist = new Modlist();
 
            saveLoad = new SaveLoader();
            settings = new Settings();

            Debug.WriteLine("Booting other modules");
            saveLoad.loadToLaunch();
            settings.Show();


        }
        private Shell(String username, String password)
        {

            modlist = new Modlist();

            saveLoad = new SaveLoader();
            saveLoad.setLogin(username, password);
            settings = new Settings();

            Debug.WriteLine("Booting other modules");
            saveLoad.loadToLaunch();

            //launch("Quicknotes");
            launch("WeeklyToDo");
            launch("Google Calendar");
            launch("QuicknotesControl");

 
             
            
            settings.Show();

        }

        public static Modlist getModList()
        {
            return modlist;
        }

        public static SaveLoader getSaveLoader(){
            return saveLoad;
        }

        public void startApp(){            

            //Application.Run(userLogin);
            
        }

    }
}

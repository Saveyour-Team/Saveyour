﻿using System;
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
        private static Modules settings;

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
            Window newModule;
            if (modID.Equals("loggedInWindow"))
            {
                newModule = new loggedInWindow();
            }
            else if (modID.Equals("Quicknotes"))
            {
                newModule = new Quicknotes();
                
            }
            else if (modID.Equals("WeeklyToDo"))
            {
                newModule = new WeeklyToDo();
            }
            else if (modID.Equals("Google Calendar"))
            {
                newModule = new GoogleCalendar();
            }
            else 
            {
                newModule = new Quicknotes();
                Debug.WriteLine("Shell attempted to launch an invalid moduleID: " + modID);
                return null;
            }

            Debug.WriteLine("Launching: "+modID);

            if ((newModule != null) && modlist.add((Module)newModule))
            {
                if (modID.Equals("Quicknotes")){
                    settings.addQNotes(newModule);
                }
                else if (modID.Equals("WeeklyToDo"))
                {
                    settings.addWTD(newModule);
                }
                else if (modID.Equals("Google Calendar"))
                {
                    settings.addGC(newModule);
                }
                newModule.Show();
            }
         
            return (Module)newModule;
        }

        private Shell() : this(null,null)
        {

            modlist = new Modlist();
 
            saveLoad = new SaveLoader();
            settings = new Modules();

            Debug.WriteLine("Booting other modules");
            saveLoad.loadToLaunch();
            settings.Show();


        }
        private Shell(String username, String password)
        {

            modlist = new Modlist();

            saveLoad = new SaveLoader();
            saveLoad.setLogin(username, password);
            settings = new Modules();

            Debug.WriteLine("Booting other modules");
            saveLoad.loadToLaunch();
            launch("WeeklyToDo");
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

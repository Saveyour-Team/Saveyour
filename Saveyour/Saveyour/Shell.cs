using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;



namespace Saveyour
{
    class Shell{
    
        private static Modlist modlist;
        private static SaveLoader saveLoad;
        private static Shell theShell;

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
            else
            {
                newModule = new Quicknotes();
                Debug.WriteLine("Shell attempted to launch an invalid moduleID: " + modID);
                return null;
            }

            Debug.WriteLine("Launching: "+modID);

            if ((newModule != null) && modlist.add((Module)newModule))
            {
                newModule.Show();
            }
         
            return (Module)newModule;
        }
        private Shell()
        {            

            //Saveyour.App app = new Saveyour.App();
            //app.InitializeComponent();
           // app.Run();
           // userLogin = new LoginWindow();
           // userLogin.ShowDialog();
            modlist = new Modlist();

 
           saveLoad = new SaveLoader();

            Debug.WriteLine("Booting other modules");
            saveLoad.loadToLaunch();


        }

        public static Modlist getModList()
        {
            return modlist;
        }

        public static SaveLoader getSaveLoader(){
            if (saveLoad == null){
                saveLoad = new SaveLoader();
            }
            return saveLoad;
        }

        public void startApp(){            

            //Application.Run(userLogin);
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace Saveyour
{
    class Shell{
    
        private static Modlist modlist;
        private static SaveLoader saveLoad;
        private static Login userLogin;
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
            //Run 'modID' + '.exe' in the SaveYour/Modules folder
            Form newModule;
            if (modID.Equals("Feedback"))
            {
                newModule = new Feedback();
            }
            else{
                newModule = new Quicknotes();
            }
            Debug.WriteLine("Launching: "+modID);

            modlist.add((Module)newModule);
            newModule.Show();
            return (Module)newModule;
        }
        private Shell()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            userLogin = new Login();
            modlist = new Modlist();

            userLogin.ShowDialog();
            if (userLogin.loggedIn())
            {
                saveLoad = new SaveLoader();
              //  saveLoad.loadToLaunch();
                Debug.WriteLine("Booting other modules");
   


            }

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

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
        private static LoginWindow userLogin;
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
            Window newModule;
            if (modID.Equals("Feedback"))
            {
            }
            Debug.WriteLine("Launching: "+modID);

            modlist.add((Module)newModule);
            newModule.Show();
            return (Module)newModule;
        }
        private Shell()
        {            

            Saveyour.App app = new Saveyour.App();
            app.InitializeComponent();
            app.Run();
            userLogin = new LoginWindow();
            userLogin.ShowDialog();
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

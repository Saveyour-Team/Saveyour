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
        private static Boolean shellLaunched;
        private static Shell theShell;

        public static Shell getShell()
        {
            if (!shellLaunched)
            {
                theShell = new Shell();
                shellLaunched = true;
            }
            return theShell;
        }

        public static Module launch(String modID)
        {
            //Run 'modID' + '.exe' in the SaveYour/Modules folder
            Module newModule = new Quicknotes();
            modlist.add(newModule);
            return newModule;
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

        public void startApp(){            

            //Application.Run(userLogin);
            
        }

    }
}

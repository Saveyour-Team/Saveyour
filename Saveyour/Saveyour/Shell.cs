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
        Login userLogin;
        public Shell()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            userLogin = new Login();

            Debug.WriteLine("Got here");
            userLogin.ShowDialog();
            if (userLogin.loggedIn())
            {
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

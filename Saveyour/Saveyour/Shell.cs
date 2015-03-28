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
    class Shell
    {
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

        public void startApp(){            

            //Application.Run(userLogin);
            
        }

    }
}

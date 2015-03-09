using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

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
        }

        public void startApp(){

            Application.Run(userLogin);
            
        }

    }
}

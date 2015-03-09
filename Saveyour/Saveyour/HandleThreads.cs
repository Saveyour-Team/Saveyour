using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace Saveyour
{
    class HandleThreads
    {
        String loginResult;
        public HandleThreads()
        {
            loginResult = "";
        }

        private BackgroundWorker processLogin = new BackgroundWorker();
        private AutoResetEvent isFinished = new AutoResetEvent(false);
        public void startProcessing(String[] userInfo)
        {
            processLogin.DoWork += authenticateLogin;
            processLogin.RunWorkerCompleted += authenticationStatus;
            processLogin.RunWorkerAsync(userInfo);

        }

        private void authenticateLogin(object sender, DoWorkEventArgs e)
        {
            NetworkControl network = new NetworkControl();

            String[] userInfo = (String[])e.Argument;
            String response = network.Connect(network.getIP(), userInfo[0] + "," + userInfo[1]);


            if (response.Contains("Logged in as"))
            {
                e.Result = "Logged in as";
            }
            else if (response.Contains("Invalid"))
            {
                e.Result = "Invalid";
            }
            else
            {
                e.Result = "Error connecting to server!";
            }

            loginResult = (String) e.Result;
            isFinished.Set();

        }

        private void authenticationStatus(object sender, RunWorkerCompletedEventArgs e)
        {
            loginResult = (String)e.Result;
            Console.WriteLine("In runworkercompleted: " + loginResult);
        }

        public String getLoginStatus()
        {
            return loginResult;
        }

        public AutoResetEvent getEventHandler()
        {
            return isFinished;
        }
    }

}

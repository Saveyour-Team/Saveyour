using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Saveyour
{
    public class SaveLoader
    {
        public static String saveFile = "saveddata.txt";
        public static String username = "";
        public static String password = "";

        /**
        * Returns the savedata string for all the modules with format 
        * [Last Modified: DATETIME]\r\r\n{ModuleID}{DATA FOR THE MODULE}\r\r\n{Module2ID}{DATA FOR THE MODULE2}\r\r\n
        * String save() return null if the save output is invalid (because it contains \r\r\n) 
        **/
        private String saveModules()
        {
            Modlist modList = Shell.getModList();
            String output = "";
            DateTime date = DateTime.UtcNow;
            String datePattern = @"yyyyMMddHHmmssffff"; //18 length string of date/time
            String currentTime = date.ToString(datePattern);
            output = output + "[Last Modified: " + currentTime + "]\r\r\n";
            foreach (Module m in modList)
            {
                String save = m.save();
                if (save == null || save.Contains("\r\r\n"))
                {
                    Debug.WriteLine("Error! Module: " + m.moduleID() + " has invalid save output! Skipping!\n");
                }
                else if (m.moduleID().Equals("loggedInWindow"))
                {
                    Debug.WriteLine("Not saving loggedInWindow to file as it is launched from LoginWindow...");
                }
                else
                {
                    output = output + "{" + m.moduleID() + "}" + "{" + m.save() + "}\r\r\n";
                }
            }
            return output;
        }


        /**
         * This method causes SaveLoader to aggregate savedata from the modules using the saveModules() method, then saves that data to disk and to server.
         **/
        public void save()
        {
            String savedata = saveModules();
            ReadWrite.writeStringTo(savedata, saveFile);

            if ((username == "") || (password == ""))
            {
                return;
            }
            String message = username + "\r\r\r" + password + "\r\r\r" + "upload" + "\r\r\r" +savedata;
            Thread saveThread = new Thread(new ParameterizedThreadStart(this.saveToNetwork));
            saveThread.Start(message);

            //Thread will autoterminate once the method saveToNetwork finishes!

        }

        /** This method is called by threads that save the userdata to the server!
         ** It receives a String as input, it's classified as object in the header for threading reasons!*/
        private void saveToNetwork(object message)
        {
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), (String)message);
            Debug.WriteLine(response);
        }


 
        /**
         * Loads saved userdata from the network and local disk depending on which is available, and which was modified most recently.  Then it sends each module the data it previously saved.
         */
        public bool load(){
            //This network connection is NOT threaded, as we want to program to halt and try and get data from the server before loading!
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), username + "\r\r\r" + password + "\r\r\r" + "login");
            String[] splitAt = { "\r\r\r" };
            String[] moduleData = response.Split(splitAt, StringSplitOptions.None);
            Debug.WriteLine("THIS IS MODULE DATA: " + moduleData[1]);
            return loadToLaunch(moduleData[1]);
        }

        /**
         *  The input String is given by the load() method and always contains the data obtained from the server (if any).
         *  This method then loads savedata from the disk and then determines which savedata to load (if both exist it takes the one most recently modified to load),
         *  and finally launches each module mentioned in the savedata and loads them with the data they had previously saved.
         */
        private bool loadToLaunch(String input) //input is the server data called from loadToLaunch()
        {
            bool localFile = true;

           // Debug.WriteLine("Input server: " + input);
            String local = ReadWrite.readStringFrom(saveFile);
            if (local.Equals("File not found.")){
                //Debug.WriteLine("Couldn't find saveddata.txt!");                
                localFile = false;
            }
            if (input.Equals(""))
            {
                if (!localFile)
                    return false;
            }

            Modlist modList = Shell.getModList();
            String modID;
            String modData;
            String[] splitAt = { "\r\r\n" };
            String[] moduleData = input.Split(splitAt, StringSplitOptions.None);
            String[] diskData = local.Split(splitAt, StringSplitOptions.None);

            String lastModifiedHeader = "";
            String lastModified = "";
            String lastModifiedHeaderLocal = "";
            String lastModifiedLocal = "";


            //The following logic is how the program determines what dataset (local or network input) was most recently modified and uses it for loading.

            //Debug.WriteLine("Length: " + moduleData[0].Length);
            //Debug.WriteLine(moduleData[0]);
            if (moduleData[0].Length == 35)
            {
                lastModifiedHeader = moduleData[0].Substring(0, 16);
                //Debug.WriteLine(moduleData[0]);
            }

            if (diskData[0].Length == 35)
            {
                lastModifiedHeaderLocal = diskData[0].Substring(0, 16);
               //Debug.WriteLine(diskData[0]);
            }

            int i = 0;
            if (lastModifiedHeader.Equals("[Last Modified: "))
            {
                i = 1;
                lastModified = moduleData[0].Substring(16, 18);

            }
            if (lastModifiedHeaderLocal.Equals("[Last Modified: "))
            {
                i = 1;
                lastModifiedLocal = diskData[0].Substring(16, 18);

            }

            if (lastModifiedHeader.Equals(""))
            {
                input = local;
                moduleData = diskData;
                //Debug.WriteLine("Local write is more recent, using it instead. Blank");
            }
            else if (lastModifiedHeaderLocal.Equals(""))
            {
                //Do nothing.
            }
            else if (Convert.ToInt64(lastModified) <= Convert.ToInt64(lastModifiedLocal)){
                input = local;
                //Debug.WriteLine("Local write is more recent, using it instead.");
                moduleData = diskData;
            }

            //Now that we have decided upon which data to load, we need to actually go through and load it by splitting up the savestring and sending the appropriate data around to modules.
            for (; i < moduleData.Length; i++)
            {
                try
                {
                   // Debug.WriteLine(moduleData[i]);
                    int idx = moduleData[i].IndexOf('}');
                    modID = moduleData[i].Substring(1, moduleData[i].IndexOf('}')-1);
                   // Debug.WriteLine(modID+ "++");
                    modData = moduleData[i].Substring(idx + 2, moduleData[i].Length - (idx+3));
                   // Debug.WriteLine("Moddata: "+modData);
                    Module launched = Shell.launch(modID);
                    if (launched != null) {
                        launched.load(modData);
                    }
                    
                }
                catch (ArgumentOutOfRangeException)
                {
                   // Debug.WriteLine("Invalid saveddata.txt: " + moduleData[i]);
                }
            }

            return true;
        }

        /**
         * This is used to tell the SaveLoader the username and password for when it makes network connections to the server.
         */
        public void setLogin(String user, String pwd)
        {
            username = user;
            password = pwd;
            saveFile = username + ".txt";
        }
    }
}

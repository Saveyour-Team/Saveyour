using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Saveyour
{
    public class SaveLoader
    {
        String saveFile = "saveddata.txt";
        String username = "";
        String password = "";

        /**
        * Returns the savedata string for all the modules with format 
        * [Last Modified: DATETIME]\r\r\n{ModuleID}{DATA FOR THE MODULE}\r\r\n{Module2ID}{DATA FOR THE MODULE2}\r\r\n
        * String save() return null if the save output is invalid (because it contains \r\n) 
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
                if (m.save().Contains("\r\r\n"))
                {
                    Debug.WriteLine("Error! Module: " + m.moduleID() + " has \\r\\n in its save output! Skipping!\n");
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

        public void save()
        {
            String savedata = saveModules();
            ReadWrite.writeStringTo(savedata, saveFile);

            if ((username == "") || (password == ""))
            {
                return;
            }
            String message = username + "\r\r\r" + password + "\r\r\r" + "upload" + "\r\r\r" +savedata;
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), message);
            Debug.WriteLine(response);
        }


        private Boolean loadModules(String input)
        {
            Boolean foundAll = true;
            Modlist modList = Shell.getModList();
            String modID;
            String modData;
            String[] splitAt = { "\r\r\n" };
            String[] moduleData = input.Split(splitAt, StringSplitOptions.None);
            if (moduleData.Length < 1)
            {
               // Debug.WriteLine("Invalidly formatted string passed to loadModules in SaveLoader.\n");
                return false;
            }
            String lastModifiedHeader = "";
            String lastModified = "";
            if (moduleData[0].Length == 34){
                 lastModifiedHeader = moduleData[0].Substring(0, 16);
            }
            
            int i = 0;
            if (lastModifiedHeader.Equals("[Last Modified: "))
            {
                i = 1;
                lastModified = moduleData[0].Substring(16, 18);
            }
            for (; i < moduleData.Length; i++)
            {

                try
                {
                  //  Debug.WriteLine(moduleData[i]);
                    int idx = moduleData[i].IndexOf('}');
                    modID = moduleData[i].Substring(1, moduleData[i].IndexOf('}') - 1);
                   // Debug.WriteLine(modID + "++");
                    modData = moduleData[i].Substring(idx + 2, moduleData[i].Length - (idx + 3));
                  //  Debug.WriteLine("Moddata: " + modData);
                    Module launched = Shell.launch(modID);
                    //launched.load(modData);
                    Boolean found = false;
                    foreach (Module m in modList)
                    {
                        if (m.moduleID().Equals(modID))
                        {
                            m.load(modData);
                            found = true;
                            break;
                        }
                    }
                    foundAll = foundAll && found;
                }
                catch (ArgumentOutOfRangeException)
                {
                   // Debug.WriteLine("Invalid saveddata.txt: " + moduleData[i]);
                    foundAll = false;
                }

            }
            return foundAll;

        }

        public Boolean load()
        {
            String data = ReadWrite.readStringFrom(saveFile);
           // Debug.WriteLine("Loaded: " + data);
            return loadModules(data);
        }

        public bool loadToLaunch(){
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), username + "\r\r\r" + password + "\r\r\r" + "login");
            String[] splitAt = { "\r\r\r" };
            String[] moduleData = response.Split(splitAt, StringSplitOptions.None);
            Debug.WriteLine("THIS IS MODULE DATA: " + moduleData[1]);
            return loadToLaunch(moduleData[1]);
        }

        //Launch all modules that have saved settings
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
                    launched.load(modData);
                }
                catch (ArgumentOutOfRangeException)
                {
                   // Debug.WriteLine("Invalid saveddata.txt: " + moduleData[i]);
                }
            }

            return true;
        }

        public void setLogin(String user, String pwd)
        {
            username = user;
            password = pwd;
            saveFile = username + ".txt";
        }
    }
}

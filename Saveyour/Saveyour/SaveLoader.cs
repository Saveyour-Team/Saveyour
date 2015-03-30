using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Saveyour
{
    class SaveLoader
    {
        String saveFile = "saveddata.txt";

        /**
        * Returns the savedata string for all the modules with format 
        * {ModuleID}{DATA FOR THE MODULE}\r\r\n
        * String save() return null if the save output is invalid (because it contains \r\n) 
        **/
        private String saveModules()
        {
            Modlist modList = Shell.getModList();
            String output = "";
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
                    output = output + "{" + m.moduleID() + "}" + "{" + m.save() + "}\r\n";
                }
            }
            return output;
        }

        public void save()
        {
            ReadWrite.writeStringTo(saveModules(), saveFile);
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
                Debug.WriteLine("Invalidly formatted string passed to loadModules in SaveLoader.\n");
                return false;
            }
            //-1 since there's nothing after the last \r\n
            for (int i = 0; i < moduleData.Length; i++)
            {
   
                try
                {
                    int idx = moduleData[i].IndexOf('}');
                    modID = moduleData[i].Substring(1, moduleData[i].IndexOf('}') - 1);
                    modData = moduleData[i].Substring(idx + 2, moduleData[i].LastIndexOf('}') - idx - 2);
                    Module launched = Shell.launch(modID);
                    launched.load(modData);
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
                    Debug.WriteLine("Invalid saveddata.txt");
                    foundAll = false;
                }
               
            }
            return foundAll;

        }

        public Boolean load()
        {
            String data = ReadWrite.readStringFrom(saveFile);
            Debug.WriteLine("Loaded: " + data);
            return loadModules(data);
        }

        //Launch all modules that have saved settings
        public void loadToLaunch()
        {
            String input = ReadWrite.readStringFrom(saveFile);

            Modlist modList = Shell.getModList();
            String modID;
            String modData;
            String[] splitAt = { "\r\r\n" };
            String[] moduleData = input.Split(splitAt, StringSplitOptions.None);

            for (int i = 0; i < moduleData.Length; i++)
            {
                try
                {
                    int idx = moduleData[i].IndexOf('}');
                    modID = moduleData[i].Substring(1, moduleData[i].IndexOf('}')-1);
                    modData = moduleData[i].Substring(idx+2, moduleData[i].LastIndexOf('}') - idx -2);
                    Module launched = Shell.launch(modID);
                    launched.load(modData);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Debug.WriteLine("Invalid saveddata.txt");
                }
            }
        }
    }
}

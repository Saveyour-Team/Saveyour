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


        /**
        * Returns the savedata string for all the modules with format 
         * \r\n{
         * {ModuleID}
         * {
         * DATA FOR THE MODULE 
         * }
         * }
        * String save() return null if the save output is invalid (because it contains \r\n) 
        **/
        private String saveModules()
        {
            Modlist modList = Shell.getModList();
            String output = "";
            foreach (Module m in modList)
            {
                if (m.save().Contains("\r\n"))
                {
                    Debug.WriteLine("Error! Module: " + m.moduleID() + " has \\r\\n in its save output!\n");
                    return null;
                }
                output = output + "\r\n{" + m.moduleID() + "}\n" + "{\n" + m.save() + "\n}\n}";
            }
            return output;
        }

        public void save()
        {
            ReadWrite.write(saveModules());
        }


        private Boolean loadModules(String input)
        {
            Boolean foundAll = true;
            Modlist modList = Shell.getModList();
            String modID;
            String modData;
            String[] splitAt = { "\r\n{" };
            String[] moduleData = input.Split(splitAt, StringSplitOptions.None);

            for (int i = 0; i < moduleData.Length; i++)
            {
                modID = moduleData[i].Substring(0, moduleData[i].IndexOf('}'));
                modData = moduleData[i].Substring(moduleData[i].IndexOf('{'), moduleData[i].LastIndexOf('}'));
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
            return foundAll;

        }

        public Boolean load()
        {
            String data = ReadWrite.read();
            return loadModules(data);
        }

        //Launch all modules that have saved settings
        public void loadToLaunch()
        {
            String input = ReadWrite.read();

            Modlist modList = Shell.getModList();
            String modID;
            String modData;
            String[] splitAt = { "\r\n{" };
            String[] moduleData = input.Split(splitAt, StringSplitOptions.None);

            for (int i = 0; i < moduleData.Length; i++)
            {
                modID = moduleData[i].Substring(0, moduleData[i].IndexOf('}'));
                modData = moduleData[i].Substring(moduleData[i].IndexOf('{'), moduleData[i].LastIndexOf('}'));
                Module launched = Shell.launch(modID);
                launched.load(modData);
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Saveyour
{
    class Modlist
    {
        List<Module> modules = new List<Module>();

        public void add(Module input)
        {
            if (modules.Contains(input))
                return;
            modules.Add(input);
            
        }

        public void remove(Module input)
        {
            if (modules.Contains(input))
                modules.Remove(input);                                
        }
        /**
         * Returns the savedata string for all the modules with format \r\n{\n{\nModuleID}\n{DATA FOR THE MODULE \n} \r\n}
         * String save() return null if the save output is invalid (because it contains \r\n) 
         **/
        public String save()
        {
            String output = "";
            foreach (Module m in modules){
                if (m.save().Contains("\r\n"))
                {
                    Debug.WriteLine("Error! Module: " + m.moduleID() + " has \\r\\n in its save output!\n");
                    return null;
                }
                output = output + "\r\n{"+m.moduleID()+"}\n" + "{\n" + m.save()+"\n}\r\n}";
            }
            return output;
        }
    }
}

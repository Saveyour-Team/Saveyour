using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace Saveyour
{
    //A data structure used to contain all of the modules in Saveyour
    public class Modlist : IEnumerable<Module>
    {       
        List<Module> modules = new List<Module>(); 
        List<String> names = new List<String>();    //We need to keep track of the names of each module so that we don't spawn duplicates in Shell

        public Boolean add(Module input)
        {
            Debug.WriteLine("Adding " + input.moduleID());
            if (modules.Contains(input)) //Check for duplicate module already in listf
            {
                return false;
            }
            modules.Add(input);
            names.Add(input.moduleID());
            return true;
            
        }

        public void remove(Module input)
        {
            if (modules.Contains(input))
                modules.Remove(input);                                
        }
 
        public IEnumerator<Module> GetEnumerator(){ //This just allows us to iterate through our list of modules
            return modules.GetEnumerator();
        }

        public bool hasName(String name) //A helper method to detect duplicates
        {
            return names.Contains(name);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

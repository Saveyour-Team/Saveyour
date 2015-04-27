using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace Saveyour
{
    public class Modlist : IEnumerable<Module>
    {       
        List<Module> modules = new List<Module>();
        List<String> names = new List<String>();

        public Boolean add(Module input)
        {
            Debug.WriteLine("Adding " + input.moduleID());
            if (modules.Contains(input)) 
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
 
        public IEnumerator<Module> GetEnumerator(){
            return modules.GetEnumerator();
        }

        public bool hasName(String name)
        {
            return names.Contains(name);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

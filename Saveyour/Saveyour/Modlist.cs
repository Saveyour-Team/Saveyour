using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace Saveyour
{
    class Modlist : IEnumerable<Module>
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
 
        public IEnumerator<Module> GetEnumerator(){
            return modules.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

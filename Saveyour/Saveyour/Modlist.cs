using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saveyour
{
    class Modlist
    {
        List<Module> modules = new List<Module>();

        public Boolean add(Module input)
        {
            if (modules.Contains(input))
                return false;
            modules.Add(input);

            return true;
        }

        public Boolean remove(Module input)
        {
            if (modules.Contains(input))
                modules.Remove(input);
            modules.Add(input);
            return true;
        }
    }
}

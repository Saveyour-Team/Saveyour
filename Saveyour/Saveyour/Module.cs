using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saveyour
{
    public interface Module : IEquatable<Module>
    {
        String moduleID();              

        Boolean update();

        String save();

        Boolean load(String data);

        Boolean Equals(Module other);
   
    }
}

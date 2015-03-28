using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saveyour
{
    public interface Module 
    {
        String moduleID();              

        Boolean update();

        String save();

        Boolean load(String data);

        String[] getDates();
   
    }
}

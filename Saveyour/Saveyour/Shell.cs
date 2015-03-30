using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saveyour
{
    class Shell
    {
        Shell()
        {
            Saveyour.App app = new Saveyour.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}

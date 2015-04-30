using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SaveyourUpdate
{
    /*
     * This interface forces Saveyour to have an Application Name, Application ID, Application Assembly, and
     * a link to download and check for updates from. This ensures that the SaveyourUpdate project has the
     * necessary functions to use to check for updates.
     * */
    public interface SaveyourUpdatable
    {
        String ApplicationName { get; }
        String ApplicationID { get; }
        Assembly ApplicationAssembly { get; }
        Uri UpdateXmlLocation { get; }
    }
}

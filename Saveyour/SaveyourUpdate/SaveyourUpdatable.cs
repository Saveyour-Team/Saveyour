using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SaveyourUpdate
{
    public interface SaveyourUpdatable
    {
        String ApplicationName { get; }
        String ApplicationID { get; }
        Assembly ApplicationAssembly { get; }
        Uri UpdateXmlLocation { get; }
    }
}

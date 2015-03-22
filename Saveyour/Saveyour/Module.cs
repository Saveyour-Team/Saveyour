using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saveyour
{
    abstract class Module
    {
        protected int moduleID;

        public Module()
        {

        }

        public Boolean add()
        {
            return true;
        }

        public Boolean delete()
        {
            return true;
        }

        public Boolean update()
        {
            return true;
        }

        public Boolean save()
        {
            return true;
        }

        public Boolean load()
        {
            return true;
        }

        abstract public Array getDates();

    }
}

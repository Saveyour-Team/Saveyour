using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saveyour
{
    public static class ReadWrite
    {
        public static Boolean write(String settings)
        {
            String directory = @"C:\Saveyour\";

            bool exists = System.IO.Directory.Exists(directory);

            if (!exists)
                System.IO.Directory.CreateDirectory(directory);

            System.IO.File.WriteAllText(directory + "settings.txt", settings);
            return true;
        }

        public static String read()
        {
            String directory = @"C:\Saveyour\";

            bool exists = System.IO.Directory.Exists(directory);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(directory);
                return "No settings found. Please save settings before loading";
            }

            string text = System.IO.File.ReadAllText(@"C:\Saveyour\settings.txt");
            return text;
        }
    }
}

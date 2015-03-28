using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saveyour
{
    public static class ReadWrite
    {
        static String directory = @"savedFiles\"; //Saves to where the .exe is located
        //String directory = @"C:\Saveyour\";

        public static Boolean write(String settings)
        {                                   
            bool exists = System.IO.Directory.Exists(directory);

            if (!exists)
                System.IO.Directory.CreateDirectory(directory);
            
            System.IO.File.WriteAllText(directory + "settings.txt", settings);
            return true;
        }

        public static String read()
        {           

            bool exists = System.IO.Directory.Exists(directory);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(directory);
                return "No settings found. Please save settings before loading";
            }

            string text = System.IO.File.ReadAllText(directory + "settings.txt");
            return text;
        }
    }
}

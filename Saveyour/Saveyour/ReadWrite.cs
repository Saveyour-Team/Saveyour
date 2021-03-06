﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Saveyour
{
    public static class ReadWrite
    {
        //Creates the directory for Saveyour userdata to be stored
        static String directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Saveyour\\";

        public static Boolean write(String settings)
        {                                   
            bool exists = System.IO.Directory.Exists(directory);

            if (!exists) //Checks for existence of directory before deleting it, although I believe CreateDirectory() may already take care of that case
                System.IO.Directory.CreateDirectory(directory);
            
            System.IO.File.WriteAllText(directory + "settings.txt", settings);
            return true;
        }

        public static Boolean writeStringTo(String output, String filename)
        {
            bool exists = System.IO.Directory.Exists(directory);

            if (!exists)
                System.IO.Directory.CreateDirectory(directory);

            System.IO.File.WriteAllText(directory + filename, output);
            return true;
        }

        //Reads user data from the directory created 
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

        public static String readStringFrom(String filename)
        {

            bool exists = System.IO.Directory.Exists(directory); //Checks if the directory exists

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(directory);
                return "Directory path does not exist. Creating new directory";
            }
            
            exists = System.IO.File.Exists(directory + filename); //Checks if file exists

            if (!exists)
            {                
                return "File not found.";
            }
            
            string text = System.IO.File.ReadAllText(directory + filename);
            return text;
        }
    }
}

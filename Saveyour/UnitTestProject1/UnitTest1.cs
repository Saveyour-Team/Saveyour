using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saveyour;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //Tests Shell's getShell method
        [TestMethod]
        public void testGetShell()
        {
            Boolean val = false;
            Shell tempshell = Shell.getShell("John","Password");

            if (tempshell != null)
                val = true;

            Assert.IsTrue(val);

        }
        //Tests Shell's launch method
        [TestMethod]
        public void testShellLaunch()
        {
            Shell tempshell = Shell.getShell("John", "Password");
            Module actual = Shell.launch("Quicknotes");
            Quicknotes expected = new Quicknotes();

            Assert.AreEqual(expected, actual, "Not Equal");

        }
        //Tests Shell's getSaveLoader method
        [TestMethod]
        public void testGetShell()
        {
            Shell tshell = Shell.getShell();
            SaveLoader expected = tshell.getSaveLoader();
            SaveLoader actual = Shell.getSaveLoader();

            Assert.AreEqual(expected, actual, "Not Equal");

        }
        //Testing ReadWrite's write method
        [TestMethod]
        public void writeTest()
        {
            
            Boolean writ = ReadWrite.write("TESTTEST");
            String[] lines = { "TESTTEST" };
            System.IO.File.WriteAllLines(@"C:\Users\Public\WriteLines.txt", lines);

            byte[] file1 = File.ReadAllBytes(@"savedFiles / settings.txt");
            byte[] file2 = File.ReadAllBytes(@"C:\Users\Public\WriteLines.txt");
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    Console.WriteLine("File1: \n", file1[i]);
                    Console.WriteLine("File2: \n", file2[i]);
                    if (file1[i] != file2[i])
                    {
                        
                        writ = false;
                    }
                }
                writ =  true;
            }
            writ =  false;

            Assert.IsTrue(writ);

        }
        //Testing ReadWrite's read method
        [TestMethod]
        public void readTest()
        {
            
            Boolean writ = ReadWrite.write("TESTTEST");
            String[] lines = { "TESTTEST" };
            System.IO.File.WriteAllLines(@"C:\Users\Public\WriteLines.txt", lines);

            byte[] file1 = File.ReadAllBytes(@"savedFiles / settings.txt");
            byte[] file2 = File.ReadAllBytes(@"C:\Users\Public\WriteLines.txt");
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    Console.WriteLine("File1: \n", file1[i]);
                    Console.WriteLine("File2: \n", file2[i]);
                    if (file1[i] != file2[i])
                    {
                        
                        writ = false;
                    }
                }
                writ =  true;
            }
            writ =  false;

            Assert.IsTrue(writ);

        }
        //Testing ReadWrite's writeStringTo method
        [TestMethod]
        public void writeStringTest()
        {
            
            Boolean writ = ReadWrite.write("TESTTEST");
            String[] lines = { "TESTTEST" };
            System.IO.File.WriteAllLines(@"C:\Users\Public\WriteLines.txt", lines);

            byte[] file1 = File.ReadAllBytes(@"savedFiles / settings.txt");
            byte[] file2 = File.ReadAllBytes(@"C:\Users\Public\WriteLines.txt");
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    Console.WriteLine("File1: \n", file1[i]);
                    Console.WriteLine("File2: \n", file2[i]);
                    if (file1[i] != file2[i])
                    {
                        
                        writ = false;
                    }
                }
                writ =  true;
            }
            writ =  false;

            Assert.IsTrue(writ);

        }
        //Testing ReadWrite's readStringFrom method
        [TestMethod]
        public void readStringTest()
        {
            
            Boolean writ = ReadWrite.write("TESTTEST");
            String[] lines = { "TESTTEST" };
            System.IO.File.WriteAllLines(@"C:\Users\Public\WriteLines.txt", lines);

            byte[] file1 = File.ReadAllBytes(@"savedFiles / settings.txt");
            byte[] file2 = File.ReadAllBytes(@"C:\Users\Public\WriteLines.txt");
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    Console.WriteLine("File1: \n", file1[i]);
                    Console.WriteLine("File2: \n", file2[i]);
                    if (file1[i] != file2[i])
                    {
                        
                        writ = false;
                    }
                }
                writ =  true;
            }
            writ =  false;

            Assert.IsTrue(writ);

        }
        //Testing NetworkControl's testIP method
        [TestMethod]
        public void testIP()
        {
            String ip = NetworkControl.getIP();
            String actual = "54.173.26.10";

            Assert.AreEqual(ip, actual, "Not Equal");

        }
        //Testing Modlist's add method
        [TestMethod]
        public void addModTest()
        {
            List<Module> modules = new List<Module>();
            Module mod = Shell.launch("Quicknotes");
            Modlist.add(mod);
            modules.Add(mod);

            Assert.AreEqual(modules[0], Modlist.modules[0], "Not Equal");
            

        }
        //Testing Modlist's remove method
        [TestMethod]
        public void addModTest()
        {
            List<Module> modules = new List<Module>();
            Module mod = Shell.launch("Quicknotes");
            Modlist.add(mod);
            modules.Add(mod);

            Modlist.remove("Quicknotes");
            modules.Remove(mod);

            Assert.AreEqual(modules[0], Modlist.modules[0], "Not Equal");


        }


    }
 }
   






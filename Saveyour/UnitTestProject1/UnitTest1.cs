using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Markup;
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

            Assert.AreNotEqual(Shell.launch("Quicknotes"), actual, "Not Equal");

        }
        //Tests Shell's getSaveLoader method
        [TestMethod]
        public void testSaveLoader()
        {            
            SaveLoader expected = new SaveLoader();
            SaveLoader actual = Shell.getSaveLoader();

            Assert.AreNotEqual(expected, actual, "Not Equal");

        }              
        
        //Testing ReadWrite's writeStringTo method
        [TestMethod]
        public void writeStringTest()
        {
            System.IO.Directory.CreateDirectory("savedFiles");

            ReadWrite.writeStringTo("TESTING", "writetest.txt");

            String written = System.IO.File.ReadAllText(@"savedFiles\writetest.txt");

            Assert.AreEqual(written, "TESTING");

        }
        //Testing ReadWrite's readStringFrom method
        [TestMethod]
        public void readStringTest()
        {
            
            System.IO.Directory.CreateDirectory("savedFiles");

            System.IO.File.WriteAllText(@"savedFiles\readtest.txt", "TESTTEST");

            String written = ReadWrite.readStringFrom(@"readtest.txt");
            
            Assert.AreEqual(written, "TESTTEST");

        }
        //Testing NetworkControl's testIP method
        [TestMethod]
        public void testIP()
        {
            NetworkControl net = new NetworkControl();
            String ip = net.getIP();
            String actual = "54.173.26.10";

            Assert.AreEqual(ip, actual, "Not Equal");

        }
        //Testing Modlist's add method
        [TestMethod]
        public void modAdd()
        {
            Modlist modules = new Modlist();
            Module mod = Shell.launch("Quicknotes");
            modules.add(mod);
            
            int count = 0;

            foreach (Module element in modules)
            {
                count++;
            }

            Assert.AreEqual(1, count, "Equal");            
        }
        //Testing Modlist's remove method
        [TestMethod]
        public void modRemove()
        {
            Modlist modules = new Modlist();
            Module mod = Shell.launch("Quicknotes");
            modules.add(mod);

            int count = 0;

            foreach (Module element in modules)
            {
                count++;
            }

            Assert.AreEqual(1, count, "Equal");


            modules.remove(mod);

            int otherCount = 0;

            foreach (Module element in modules)
            {
                otherCount++;
            }

            Assert.AreEqual(0, otherCount, "Equal");

        }


    }
 }
   






using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saveyour;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testGetShell()
        {
            Boolean val = false;
            Shell tempshell = Shell.getShell("John","Password");

            if (tempshell != null)
                val = true;

            Assert.IsTrue(val);

        }
        [TestMethod]
        public void testShellLaunch()
        {
            Shell tempshell = Shell.getShell("John", "Password");
            Module actual = Shell.launch("Quicknotes");
            Quicknotes expected = new Quicknotes();

            Assert.AreEqual(expected, actual, "Not Equal");

        }
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
    }
 }
   






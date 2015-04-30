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
            Shell tempshell = Shell.getShell("TestUser", "password");

            if (tempshell != null)
                val = true;

            Assert.IsTrue(val);

        }
        //Tests Shell's launch method
        [TestMethod]
        public void testShellLaunch()
        {
            try
            {
                Shell tempshell = Shell.getShell("TestUser", "password");
                Module actual = Shell.launch("Quicknotes");

                Assert.AreEqual(Shell.launch("Quicknotes"), actual, "Not Equal");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Error with hotkey. " + e.Message);
            }

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
            Modlist mods = Shell.getModList();
            Module mod = Shell.launch("QuickNotes");
            mods.add(mod);

            int count = 0;

            foreach (Module element in mods)
            {
                count++;
            }

            Assert.AreEqual(1, count, "Equal");


            mods.remove(mod);

            int otherCount = 0;

            foreach (Module element in mods)
            {
                otherCount++;
            }

            Assert.AreEqual(0, otherCount, "Equal");

        }

        //Testing Modlist's hasName method and names list
        [TestMethod]
        public void modNames()
        {
            Modlist modules = new Modlist();
            Module mod = Shell.launch("Quicknotes");
            modules.add(mod);
            bool hname = false;
            String name = "Quicknotes";

            int count = 0;

            if(modules.hasName("Quicknotes"))
                count++;
            

            hname = modules.hasName(mod.moduleID());

            Assert.AreEqual(1, count, "Equal");
            Assert.AreEqual(name, mod.moduleID(), "Equal");

        }

        //Testing Quicknote's ID method
        [TestMethod]
        public void qnotesID()
        {
            Modlist modules = new Modlist();
            Module mod = Shell.launch("Quicknotes");
            String actual = "Quicknotes";
            String got = mod.moduleID();

            Assert.AreEqual(actual, got, "Not Equal");


        }



        //Testing WeeklyToDo's addTask method
        [TestMethod]
        public void wtdAddTasks()
        {
            WeeklyToDo wtd = new WeeklyToDo();
            DateTime today = new DateTime(2015, 4, 29);
            Saveyour.Task task = new Saveyour.Task("Project", "All Problems", 10, today);
			
			wtd.addTask(task);
			String project = "Project";
            String desc = "All Problems";
            int w = 10;

            List<Saveyour.Task> taskList = wtd.hashTasks[] ;
            foreach (Saveyour.Task tasks in taskList)
            {
                Assert.AreEqual(task.getTitle(), project, "Titles Not Equal");
            	Assert.AreEqual(task.getDescription(), desc, "Descriptions Not Equal");
            	Assert.AreEqual(task.getWeight(), w, "Weights Not Equal");
            	Assert.AreEqual(task.getDate(), today, "Dates Not Equal");
            }


        }

        //Testing WeeklyToDo's removeTask method
        [TestMethod]
        public void wtdRemoveTasks()
        {
            WeeklyToDo wtd = new WeeklyToDo();
            DateTime today = new DateTime(2015, 4, 29);
            Saveyour.Task task = new Saveyour.Task("Project", "All Problems", 10, today);
			
			wtd.addTask(task);
			String project = "Project";
            String desc = "All Problems";
            int w = 10;
            
            Assert.AreEqual(task.getTitle(), project, "Titles Not Equal");
            Assert.AreEqual(task.getDescription(), desc, "Descriptions Not Equal");
            Assert.AreEqual(task.getWeight(), w, "Weights Not Equal");
           	Assert.AreEqual(task.getDate(), today, "Dates Not Equal");
			
			int count = 0;
			List<Saveyour.Task> taskList = wtd.hashTasks[];
            foreach (Saveyour.Task tasks in taskList)
            {
                count++;
            }
            
			Assert.AreEqual(0, count, "Equal");

        }

        //Testing WeeklyToDo's save method
        [TestMethod]
        public void wtdSave()
        {
            WeeklyToDo wtd = new WeeklyToDo();
            DateTime today = new DateTime(2015, 4, 29);
            Saveyour.Task task = new Saveyour.Task("Project", "All Problems", 10, today);

            wtd.addTask(task);
            String actual = "Project\t\tAll Problems\t\t10\t\t04/29/2015";
            String output = wtd.save();



            Assert.AreEqual(actual, output, "Equal");

        }

        //Testing WeeklyToDo's load method
        [TestMethod]
        public void wtdLoad()
        {
            WeeklyToDo wtd = new WeeklyToDo();
            DateTime today = new DateTime(2015, 4, 29);
            Saveyour.Task task = new Saveyour.Task("Project", "All Problems", 10, today);
			
			wtd.addTask(task);
			String output = wtd.save();
			Boolean loaded = wtd.load(output);
            String project = "Project";
            String desc = "All Problems";
            int w = 10;
			
			List<Saveyour.Task> taskList = wtd.hashTasks[];
			int count = 0;
            foreach (Saveyour.Task tasks in taskList)
            {
                Assert.AreEqual(tasks.getTitle(), project, "Titles Not Equal");
            	Assert.AreEqual(tasks.getDescription(), desc, "Descriptions Not Equal");
            	Assert.AreEqual(tasks.getWeight(), w, "Weights Not Equal");
            	Assert.AreEqual(tasks.getDate(), today, "Dates Not Equal");
            	count++;
            }
            
			Assert.AreEqual(1, count, "Equal");
        }

        //Testing WeeklyToDo's ID method
        [TestMethod]
        public void wtdID()
        {
            WeeklyToDo wtd = new WeeklyToDo();
            String actual = "WeeklyToDO";
            String got = wtd.moduleID();

            Assert.AreEqual(actual, got, "Not Equal");


        }

        //Testing the creation of a new task
        [TestMethod]
        public void newTask()
        {
            DateTime today = new DateTime(2015, 4, 26);
            Saveyour.Task task = new Saveyour.Task("Project", "All Problems", 10, today);

            String project = "Project";
            String desc = "All Problems";
            int w = 10;

            Assert.AreEqual(task.getTitle(), project, "Titles Not Equal");
            Assert.AreEqual(task.getDescription(), desc, "Descriptions Not Equal");
            Assert.AreEqual(task.getWeight(), w, "Weights Not Equal");
            Assert.AreEqual(task.getDate(), today, "Dates Not Equal");

        }

        //Testing loginwindow's loggedIn method
        [TestMethod]
        public void loggedIn()
        {
            Saveyour.LoginWindow lw = new LoginWindow();
            bool loggedIn = false;
            String username = "TestUser";
            String password = "password";
            String command = "login";

            //Create a network connection and connect
            NetworkControl network = new NetworkControl();
            String response = network.Connect(network.getIP(), username + "\r\r\r" + password + "\r\r\r" + command);
            //Debug.WriteLine(response);

            String[] splitAt = { "\r\r\n" };
            String[] responseData = response.Split(splitAt, StringSplitOptions.None);
            //userData = responseData[1];
            String userData = null;

            if (responseData.Length > 1)
            {
                userData = responseData[1];
                //Debug.WriteLine("UserData: " + userData);
            }


            if (response.Contains("Logged in as"))
            {
                
                loggedIn = true;
                
            };
            Assert.AreEqual(loggedIn, lw.loggedIn());
        }

        //Testing SaveLoader's save and load methods
        [TestMethod]
        public void saveLoadMods()
        {
            Modlist modules = new Modlist();
            Module mod = Shell.launch("Quicknotes");
            modules.add(mod);
            Module mod2 = Shell.launch("WeeklyToDo");
            modules.add(mod);
            modules.add(mod2);

            int count = 0;

            foreach (Module element in modules)
            {
                count++;
            }

            Assert.AreEqual(2, count, "Equal");

            SaveLoader sv = new SaveLoader();
            sv.save();
            bool load = sv.load();

            Assert.IsTrue(load);

        }

        //Testing SaveLoader's setLogin method
        [TestMethod]
        public void saveLogInInfo()
        {
            
            SaveLoader sv = new SaveLoader();
            String usr = "John";
            String pas = "123";
            String file = "John.txt";
            
            sv.setLogin("John","123");
            
            Assert.AreEqual(sv.username,usr,"Not Equal");
            Assert.AreEqual(sv.password,pas,"Not Equal");
            Assert.AreEqual(sv.saveFile,file,"Not Equal");
            
        }

        //Testing Settings's new module methods
        [TestMethod]
        public void settingsNew()
        {
            
            Settings set = new Settings();
            Modlist modules = new Modlist();
            Module mod = Shell.launch("Quicknotes");
            Module mod2 = Shell.launch("WeeklyToDo");
 
            set.addQNotes(mod);
            set.addWTD(mod2);
                  
            Assert.IsNotNull(set.qnotes);
            Assert.IsNotNull(set.weeklytd);
            
        }

        //Testing Settings's ID method
        [TestMethod]
        public void settingsID()
        {
            Settings set = new Settings();
            String actual = "Settings";
            String got = set.moduleID();

            Assert.AreEqual(actual, got, "Not Equal");


        }

        //Testing Homework's ID method
        [TestMethod]
        public void hwID()
        {
            Homework hw = new Homework();
            String actual = "Homework";
            String got = hw.moduleID();

            Assert.AreEqual(actual, got, "Not Equal");


        }

        //Testing new Homework
        /*[TestMethod]
        public void newHW()
        {
            Homework hw = new Homework();
            //Subject sub = new Subject();

            Assert.IsNotNull(hw.subjects[0]);


        }*/

    }
}



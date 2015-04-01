# Saveyour

## What is Saveyour?
  Saveyour is an organizational, productive application that contains weekly to-do’s, calendars, agendas, notes, etc. into one tool. Its target in particular is the college student; however, entreprenuers, professors, and busy individuals will find the tool useful as well! It supports modularized togglable windows that can be customized by other developers. There are certain macros for quick task management such as parsing highlighted text to add to one's weekly to-do module or to add to one's homework module. There is a button to quickly hide and unhide Saveyour as a reference to one's schedule in a glance. Saveyour is able to take ones tasks and look through one's weekly to-do in order to find an available time to assign those tasks to do. 

Saveyour is extensible and the software written takes into account other developers who may want to add customizations to Saveyour through modules. Saveyour implements these core modules as base features:

#### Quicknotes - Quickly add text to save to local disk and the cloud
#### Homework - Add subjects with tasks and due dates for homework! Parsing will allow for automatic task adding for this module.
#### Weekly To-Do - Displays days of the week as well as tasks that are required throughout that week. Color-coded analysis will allow for one to look through one's day (red - busy, yellow - somewhat busy, green - plenty of freedom).
#### Google Calendar - Displays one's Google Calendar for quick reference incase Weekly To-Do itself does not do the job!


# 1. Source control and build process instructions:

Version control of repository for SaveYour is stored on github. To access repository, follow the link: https://github.com/Saveyour-Team

## 1.1 Client Build and Updates
### 1.1.1 Build Process Instructions
  The client for this project was built on Visual Studio 2013 Ultimate.
  The code can be found at https://github.com/Saveyour-Team/Saveyour. This repository can be cloned by typing in a terminal with git installed: 
“git clone https://github.com/Saveyour-Team/Saveyour.git”. 
  There is a Saveyour.sln in the Saveyour folder that will open Visual Studio. Clicking “Start” near the top of Visual Studio will build a debug version of the application and run the application. Additionally, selecting the Build tab and then clicking Build Solution will create a executable (.exe) file in Saveyour/Saveyour/bin/Debug.

Before logging in, one must click on the "Add Certificate" button in order to authenticate oneself to the server to establish a secure TLS connection. In the future, an installer will be created to automatically create the certificate for the consumer. From there one can then use the following information to login or click the register button to add a new user:

If one wishes to connect to their own server, please change the ip address in the NetworkControl class to the corresponding server ip.

  Some current mockups of usernames/passwords on the database are: 
  Brendan / ih8slackbot 
  John / mongoDBisDaBest 
  Simran / pyth0n
 
  Using any of these pairs should allow you to login using the Saveyour client. Invalid credentials will not allow for login.

  After logging in, a user's module windows will open up for use.

### 1.1.2 Updating and Obtaining Saveyour
  The Client release can be downloaded in the Saveyour-Team Release repository, or directly at https://github.com/Saveyour-Team/Release/raw/master/Saveyour.exe. 
  The latest version of the Saveyour client will be built and pushed to this repository. For now, the client must download the newest version of Saveyour from the release repository. Users should frequently monitor GitHub for updates and releases. 

### 1.1.3 Future Ways of Updating and Obtaining Saveyour
  An updater branch has been created for future use in updating without having to pull/download the newest release from GitHub. Releasing updates to GitHub is a preliminary design. In future releases, there will be an in-client update function that will allow the user to either set automatic updates or manually update from within the client. This feature has already been prototyped, but needs to become more robust before integrating it in the application. Additionally, if time allows, a website to download the Saveyour client will be created. This website will have the latest version of Saveyour at all times.

## 1.2 Deploying Server Updates
  The AWS server automatically pulls, compiles, and deploys the latest version of the SaveYourServer repository whenever changes are pushed to the Master Branch by using a configuration of the “Git Play” addon, which is available at “https://github.com/stewartpark/git-play”

  As a result, when we want to deploy development code to the server all we need to do is merge a development branch to the SaveYourServer master in git using whatever git software we prefer.  If a developer that is not a member of the team wishes to contribute, they may fork their own instance of the repository and then submit a pull request for us to review and approve a merge to the master branch.

# 2. Bug tracking system instructions:

## 2.1  Accessing Bug Tracking System:
  Our bug tracking system is Github’s built-in bug and issue tracking system. To access the bug and issue tracking system for SaveYour app, follow the link: https://github.com/Saveyour-Team/Saveyour/issues. This page displays all issues, bugs, and features/enhancements related to the project. 

  We originally were using Trello for bug-tracking, however we felt it would be more developer-friendly to have our main hub for bug & issue tracking on Github. This would allow the bugs that we log be viewable by the public as well as allow developers with a Github account to log new issues that we might overlook in our development process.

  As a note, since we originally used Trello and shifted to Github, you may see that a large number of bugs were originally logged by our project manager, Nathan. To see who the bug was originally reported by back when we were using Trello, you should click the name of the issue to see a comment describing the original reporter.
	
## 2.2  Filing bugs: 
  To file a bug, click on the “New Issue” button on the top right. Provide the issue with a title and description. An issue can have a label added to it, e.g. “bug” for further classification. An issue can also be assigned milestones, i.e. completion date, and developers assigned to the task. 
	
  For a step-by-step demonstration, with pictures, of how to create an issue on Github, please see Github’s documentation here: https://help.github.com/articles/creating-an-issue/
  
# 3. Testing

## 3.1 Unit Tests
  Unit tests have been added for the Client, Server, and Database. Server and database unit tests can be found in https://github.com/Saveyour-Team/SaveYourServer on the "test" branch. The files are listed as ServerTest.py and TestDB.py. Substantial testing has been done on the server and databse. By running each python file each of which rely on the unittest python library, the python files will start the test suites for each respective component of the system. 
  
  To run these tests on the server and database, you need to be logged into AWS to access the AWS Shell. The server must first be tested from the client by making three new accounts in the application with (username,password): (Bob,Builder),(Barney,Dinosaur),(BeAutY, bEAsT). Note that the password for BeAutY begins with a space. Then, on the shell, run the command "python ServerTest.py" to test the methods of the server on these accounts. To test the database, running "python TestDB.py" will test storaging of documents on the MongoDB database.

## 3.2 Integration/System Tests  
  For the Client unit tests can be found on https://github.com/Saveyour-Team/Saveyour/tree/unittests/Saveyour/UnitTestProject1 which is under the unittests branch. There are tests on here that test for the functionality of the quicknotes and login use case outlined in the Beta release SRS document. They are added as another project in the Saveyour.sln.

  If you would like to run the unit tests currently available (or even write your own), you can do so by means of the test project mentioned immediately above. Once you have successfully built the Saveyour solution in Visual Studio, you should then open the UnitTestProject1 project. 
  
#### 3.2.1 Opening the Unit Test Project
  Do this by navigating to Saveyour\UnitTestProject1\ and right-clicking on UnitTestProject1. Choose to "Open With" Microsoft Visual Studio 2013. The project should then load in a new instance of Visual Studio.

#### 3.2.2 Viewing Unit Test Source Code
  Once open in Visual Studio, view UnitTest1.cs by double clicking it in the Solution Explorer. There you will see the existing source code for our tests and you can choose to add new tests should you wish to do so.
  
  Should you decide you would like to write your own Unit Tests but are unfamiliar with how to do this in Visual Studio, a broad overview of unit testing with Visual Studio can be found at [this link to Microsoft's documentation](https://msdn.microsoft.com/en-us/library/dd264975.aspx).
  
#### 3.2.3 Running the Unit Tests
  To run the client unit tests, first the test explorer needs to be opened. This can be opened by selecting the "TEST" menu at the top of Visual Studio. Then click on "Windows > Test Explorer". A test explorer should now open. The available tests will load once the solution is compiled. Simply hit "Run All" in the test explorer and Visual Studio will automatically build the solution and then attempt to run the tests. Alternatively, the solution could be built first by selecting "BUILD > Build Solution" at the top of Visual Studio. Then the test explorer will load all available tests and mark them as "Not Run Tests". The tests can then be run by using the "Run All" or "Run..." button. Successful or passed tests will show a check mark next to the name of the test, while unsuccssful or failed tests will show an X next to the name of the test.

#### 3.2.4 A Bug in Visual Studio that Causes the Tests to sometimes not Compile
  An additional important note that may cause the tests not to work is that there seems to be a bug with Visual Studio not properly detecting Saveyour as a reference; in other words it isn't recognizing Saveyour as part of the project for unit test. When this bug occurs, "using Saveyour" will be underlined in red showing that it isn't recognized. Additionally, other variables that depend on this namespace will also be underlined in red. In order to fix this bug, simply right click on "References" in the solution explorer for "UnitTestProj1" and click "Add Reference". Saveyour should be selected on the list of available projects to include as references. It should be readded as a reference by unchecking Saveyour, hitting "OK" and then using the "Add References" button to readd Saveyour as a reference.
  
  Official steps provided by Microsoft on how to run unit tests in Visual Studio can be found [here](https://msdn.microsoft.com/en-us/library/hh270865.aspx#BKMK_Run_tests_in_Test_Explorer).
  
#### 3.2.5 Reporting Failed Unit Tests
  If you run the tests from the unit test project and Saveyour happens to fail one or more of the tests, please feel free to log your findings using the GitHub issue tracking system [here](https://github.com/Saveyour-Team/Saveyour/issues). Again, you can learn how to create and log a new issue in section 2.2 above.

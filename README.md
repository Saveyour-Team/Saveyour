# Saveyour

# 1. Source control and build process instructions:

Version control of repository for SaveYour is stored on github. To access repository, follow the link: https://github.com/Saveyour-Team

## 1.1 Client Build and Updates
### 1.1.1 Build Process Instructions
  The code can be found at https://github.com/Saveyour-Team/Saveyour. This repository can be cloned by typing in a terminal with git installed: 
“git clone https://github.com/Saveyour-Team/Saveyour.git”. 
  There is a Saveyour.sln in the Saveyour folder that will open Visual Studio. Visual Studio 2013 Ultimate was used for this project. Clicking “Start” near the top of Visual Studio will build a debug version of the application and run the application. Additionally, selecting the Build tab and then clicking Build Solution will create a executable (.exe) file in Saveyour/Saveyour/bin/Debug.

  Some current mockups of usernames/passwords on the database are: 
  Brendan / ih8slackbot 
  John / mongoDBisDaBest 
  Simran / pyth0n
  Using any of these pairs should allow you to login using the Saveyour client, and using the wrong password will not.

  For client mockup data stored on the database, Brendan has “data2”, John has “data6”, and Simran has “i like rocks”, which will also be fetched and displayed when the user logs in.

### 1.1.2 Updating and Obtaining Saveyour
  The Client release can be downloaded in the Saveyour-Team Release repository, or directly at https://github.com/Saveyour-Team/Release/raw/master/Saveyour.exe. 
  The latest version of the Saveyour client will be built and pushed to this repository. For now, the client must download the newest version of Saveyour from the release repository. Users should frequently monitor GitHub for updates and releases. 

### 1.1.3 Future Ways of Updating and Obtaining Saveyour
  Releasing updates to GitHub is a preliminary design. In future releases, there will be an in-client update function that will allow the user to either set automatic updates or manually update from within the client. This feature is currently in development and is unfinished. Additionally, if time allows, a website to download the Saveyour client will be created. This website will have the latest version of Saveyour at all times.

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

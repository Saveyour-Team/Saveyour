#Saveyour Release Notes
## Installation of Saveyour
To install Saveyour, please download the installer at https://saveyour.herokuapp.com.
* * *
## 1.0 Release - Wednesday April 30, 2015
### General improvements
- Overhaul in UI design with a white and blue color scheme.
- All modules spawn in a set location. 
  - A future feature will have them spawn where the user left the features. 
    - There will also be a “Reset to Default Position” in Settings.
- General styling and bug fixes.

### Homework module
- Updated so that it no longer spans horizontally when adding a subject.
  - This design decision was made to encompass those who have more than 5 subjects and do not want to clutter their desktop. Instead, we use a tabbed interface for homework module with an “All” tab to see homeworks in ascending order.

### Settings module
- Now has hotkey macros updated to be able to show the user what to press to hide/show modules
  - Hotkeys include a variety of key presses. 
  - Currently, the hotkeys are not customizable, however, there will be an update pushed to customize these hotkeys sometime in the future.

### Weekly To-Do module
- Now has color coding based off the weight of tasks:
  - Green: Total weight of less than 5
  - Yellow: Total weight between 5 - 7
  - Orange: Total weight between 8 - 9
  - Red: Total weight of greater than or equal to 10

### Google Calendar WebView
- Google Calendar WebView has been implemented to allow for one to login and see their calendar

### Known Issues
#### Fixes from last release
- Weekly To-Do is now fully functional.
- Logging in now gives visual feedback by showing a "Logging in" message on the login screen after entering your credentials.
- As stated above, the Setting module now has functionality for showing and hiding individual modules.

#### Additional Known Issues
Can be found at our issues page [here](https://github.com/Saveyour-Team/Saveyour/issues).

### Additional Information
Including build instructions for developers, how to view changelogs, how to file an issue, and more can be found in the README [here](https://github.com/Saveyour-Team/Saveyour/blob/master/README.md).
* * *
## Beta Release - Wednesday April 1, 2015
### Log-in
TLS Certificate was added to supply a secure connection between the client and the server. Pressing tab/enter in the username field will allow one to jump to the password field. You must click Add Certificate before being able to log in.
### Quick Notes Module
Quicknotes module created and is able to be resized. Exiting the module causes the application to save the user data to the server as well as locally
### Weekly To-Do Module
Weekly To-Do module created and is able to add tasks for each day of the week. Window can be resized only horizontally.
### Settings Module
Buttons in the settings should be able to show/hide modules. Showing and hiding the modules does not destroy the process, it keeps it running which is one of the key aspects behind Saveyour. 

### Known Issues
#### Known Issue #1: Weekly To-Do has limited functionality
Weekly To-Do can only have one task per day display at a time. The days also seemed to be cramped. There is also no option to resize it vertically which is something that needs to be worked on for the final release.
#### Known Issue #2: No loading modules sign
When logging in, it may take a few seconds before seeing the modules load. There is no visual feedback to tell the user this so one may think that the app has crashed. 
#### Known Issue #3: Settings Module does not have show/hide for all of the modules
The settings module does not have show/hide for all of the modules. Also, other than showing/hiding, the settings module is limited so far. This will be changed in the final release.
* * *
## Alpha Release - Monday March 9, 2015
### Log-in
Added functinoality to login.
### Feedback Window
To show data from the database/server a window is created with the username and a string taken as data from a server.

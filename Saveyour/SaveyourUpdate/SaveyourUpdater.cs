using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SaveyourUpdate
{
    /// <summary>
    /// The purpose of this class is to create an updater that controls of the components of SaveyourUpdater
    /// This controller will make calls to check for updates and apply updates.
    /// </summary>
    public class SaveyourUpdater
    {
        private SaveyourUpdatable applicationInfo;
        private BackgroundWorker bgWorker;

        public SaveyourUpdater(SaveyourUpdatable applicationInfo)
        {
            this.applicationInfo = applicationInfo;

            //A new background worker is created so that handling moving and downloading operations do not cause the program to freeze
            this.bgWorker = new BackgroundWorker();
            this.bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }

        /*
         * This function allows other projects that implement SaveyourUpdate to call the doUpdate function. Namely, Saveyour is going
         * to be using this function to do an update.
         * */
        public void doUpdate()
        {
            if (!this.bgWorker.IsBusy)
                this.bgWorker.RunWorkerAsync(this.applicationInfo);
        }

        /*
         * This event handler handles the event where the background worker has completed its work. Upon completing its work, it will
         * check to see if the update that it fetched is newer than the current version. If so, it will display SaveyourUpdateAcceptWindow
         * which will prompt the user if they want to update. Otherwise, a message is shown displaying that there is no update to download.
         * */
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                SaveyourUpdateXML update = (SaveyourUpdateXML)e.Result;

                if (update != null && update.IsNewerThan(this.applicationInfo.ApplicationAssembly.GetName().Version))
                {
                    if (new SaveyourUpdateAcceptWindow(this.applicationInfo, update).ShowDialog() == true)
                        this.DownloadUpdate(update);
                }
                else
                {
                    new SaveyourNoUpdateWindow().ShowDialog();
                }
            }
        }

        /*
         * This function downloads the update from the update source given in SaveyourupdateXML. It will attempt to download
         * the update to the a temporary .tmp file found in the temporary folder. It will also get and save the new file path where
         * Saveyour is supposed to be located. Finally, it will call another function which will update Saveyour called UpdateApplication().
         * After calling that function, Saveyour will shut download.
         * */
        private void DownloadUpdate(SaveyourUpdateXML update)
        {
            SaveyourUpdateDownloadWindow window = new SaveyourUpdateDownloadWindow(update.Uri, update.MD5);
            bool? result = window.ShowDialog();

            if (result == true)
            {
                String currentPath = this.applicationInfo.ApplicationAssembly.Location;
                String newPath = Path.GetDirectoryName(currentPath) + "\\" + update.FileName;

                UpdateApplication(window.TempFilePath, currentPath, newPath, update.LaunchArgs);

                Application.Current.Shutdown(); //Shuts down Saveyour
            }
            else if (result == null)
            {
                MessageBox.Show("The update download was cancelled and was not applied.", "Update Download Cancelled", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("There was a problem downloading the update. Please try again later", "Update Download Error",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /*
         * This function launches command prompt. Command prompt has set arguments that will do the replacing of the old Saveyour with the new Saveyour.
         * Since Saveyour will be closed already, Saveyour.exe will be deleted and then the new Saveyour.exe will be moved to where the old Saveyour
         * was located before. Finally, command prompt will run the new Saveyour.
         * */
        private void UpdateApplication(string tempFilePath, string currentPath, string newPath, string launchArgs)
        {

            String argument = "/C Choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\" & Choice /C Y /N /D Y /T 2 & Move /Y \"{1}\" \"{2}\" & Start \"\" /D \"{3}\" \"{4}\" {5}";

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "runas";
            info.Arguments = string.Format(argument, currentPath, tempFilePath, newPath, Path.GetDirectoryName(newPath), Path.GetFileName(newPath), launchArgs);
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            Process.Start(info);
        }

        /*
         * This background worker will check if there is an update on the server. If it exists, then the program will update. If there is no
         * update, then it will not.
         * */
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SaveyourUpdatable application = (SaveyourUpdatable)e.Argument;

            if (!SaveyourUpdateXML.ExistsOnServer(application.UpdateXmlLocation))
                e.Cancel = true;

            else
                e.Result = SaveyourUpdateXML.Parse(application.UpdateXmlLocation, application.ApplicationID);
        }
    }
}

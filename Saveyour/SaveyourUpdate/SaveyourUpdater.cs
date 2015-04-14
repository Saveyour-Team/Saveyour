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
    public class SaveyourUpdater
    {
        private SaveyourUpdatable applicationInfo;
        private BackgroundWorker bgWorker;

        public SaveyourUpdater(SaveyourUpdatable applicationInfo)
        {
            this.applicationInfo = applicationInfo;

            this.bgWorker = new BackgroundWorker();
            this.bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }

        public void doUpdate()
        {
            if (!this.bgWorker.IsBusy)
                this.bgWorker.RunWorkerAsync(this.applicationInfo);
        }

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
            }
        }

        private void DownloadUpdate(SaveyourUpdateXML update)
        {
            SaveyourUpdateDownloadWindow window = new SaveyourUpdateDownloadWindow(update.Uri, update.MD5);
            bool? result = window.ShowDialog();

            if (result == true)
            {
                String currentPath = this.applicationInfo.ApplicationAssembly.Location;
                String newPath = Path.GetDirectoryName(currentPath) + "\\" + update.FileName;

                UpdateApplication(window.TempFilePath, currentPath, newPath, update.LaunchArgs);

                Application.Current.Shutdown();
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

            /*
            Debug.WriteLine(currentPath + "," + tempFilePath + "," + newPath);
            File.Delete(currentPath);
            File.Move(tempFilePath, newPath);
            Process.Start(newPath);
             */
        }

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

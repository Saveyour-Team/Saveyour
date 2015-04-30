using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Net;
using System.Diagnostics;

namespace SaveyourUpdate
{
    /// <summary>
    /// Interaction logic for SaveyourUpdateDownloadWindow.xaml
    /// The purpose of this class is to create a Window that displays and downloads the newest version of Saveyour.
    /// </summary>

    internal partial class SaveyourUpdateDownloadWindow : Window
    {
        private WebClient webClient;
        private BackgroundWorker bgWorker;
        private String tempFile;
        private String md5;

        /*
         * This function returns tempFile which is a file path to a temporary location. This is where Saveyour is downloaded to first.
         * */
        internal String TempFilePath
        {
            get { return this.tempFile; }
        }


        internal SaveyourUpdateDownloadWindow(Uri location, String md5) 
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen; //Centers window to screen

            tempFile = System.IO.Path.GetTempFileName();
            //A temporary path file (.tmp) is created in the Windows temporary folder and the path is given and assigned to tempFile

            Debug.WriteLine(tempFile);
            this.md5 = md5;

            /*
             * A web client is created to interact with the internet. In this case, two event handlers are created, one for updating the 
             * progress bar of how much is downloaded, and the second is what to do after it is done downloading.
             * */

            webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted +=webClient_DownloadFileCompleted;

            /*
             * A background worker is created so that the download and update process is multithreaded. This will allow the program not to 
             * freeze when updating.
             * */

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);

            /*
             * First, the program will attempt to download the file. If it cannot download it, then a reponse is sent to SaveyourUpdater saying that
             * the update could not be downloaded. Then the Window closes.
             * */
            try
            {
                webClient.DownloadFileAsync(location, tempFile);
            }

            catch
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        /*
         * This event handler handles what the background worker does when it has finished all of its work. It will send a response to SaveyourUpdater
         * and let it know that it has finished running. Then it will close.
         * */
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = (bool)e.Result;
            this.Close();
        }


        /*
         * This event handler handles what the background worker does when it is created. It is going to check if the MD5 hash of the file that is
         * downloaded is the same as the MD5 hash listed in the update. If they are the same, then it is known that the file was downloaded correctly. If
         * they are not the same, then there was an error downloading the file.
         * */
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string file = ((string[])e.Argument)[0];
            string updateMD5 = ((string[])e.Argument)[1];


            if (Hasher.HashFile(file, HashType.MD5) != updateMD5)
            {
                e.Result = false;
            }
            else
            {
                e.Result = true;
            }
        }

        /*
         * This event handler hands what the web client does when it has finished downloading the file. If there was no cancellation
         * by the user, or any other errors, then the file is verified by MD5 hash by the background worker. If there was something to
         * interrupt the download, then a reponse is sent to SaveyourUpdater notifying it that the file was not downloaded.
         * */
        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.DialogResult = false;
                this.Close();
            }
            else if (e.Cancelled)
            {
                this.DialogResult = null;
                this.Close();
            }
            else
            {
                lblProgress.Content = "Verifying Download...";
                progressBar.IsIndeterminate = true;

                bgWorker.RunWorkerAsync(new string[] { tempFile, this.md5 });
            }
        }

        /*
         *  This event handler handles what happens when the web client is currently downloading the file. In particular, it is going
         *  to update the progress bar found on the Window based on how much is downloaded. It will also give a textual update
         *  to the label that displays how many bytes, kilobytes, megabytes, or gigabytes have been downloaded.
         * */
        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            this.lblProgress.Content = String.Format("Downloaded {0} of {1}", FormatBytes(e.BytesReceived, 2, true),
                FormatBytes(e.TotalBytesToReceive, 2, true));
        }

        /*
         * This is a helper function for updating the amount of progress downloaded. It formats the bytes such that if it is greater than 1KB,
         * then it will use KB rather than B to display. If it is greater than 1MB, then it will use the MB notation. If it is greater than 1GB,
         * then it will use the GB notation.
         * */
        private String FormatBytes(long bytes, int decimalPlaces, bool showByteType)
        {
            double newBytes = bytes;
            String formatString = "{0";
            String byteType = "B";

            if (newBytes > 1024 && newBytes < 1048576)
            {
                newBytes /= 1024;
                byteType = "KB";
            }
            else if (newBytes > 1048576 && newBytes < 1073741824)
            {
                newBytes /= 1048576;
                byteType = "MB";
            }
            else
            {
                newBytes /= 1073741824;
                byteType = "GB";
            }

            if (decimalPlaces > 0)
                formatString += ":0.";

            for (int i = 0; i < decimalPlaces; i++)
                formatString += "0";

            formatString += "}";

            if (showByteType)
                formatString += byteType;

            return String.Format(formatString, newBytes);

        }

        /*
         * This event handler handles wheat happens if the Window is closed by the user. Although there is no X button for the user
         * to press to cancel it, if for some reason the program is closed while downloading, this event handler will ensure that nothing is corrupted.
         * If the web client is working, then the download is canceled and SaveyourUpdater is notified that the update was cancelled.
         * If the background worker was working, then it will be cancelled and SaveyourUpdater will be notified that there was an error updating.
         * */
        private void Window_Closed(object sender, EventArgs e)
        {
            if (webClient.IsBusy)
            {
                webClient.CancelAsync();
                this.DialogResult = null;
            }

            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
                this.DialogResult = null;
            }
        }

        /*
         * This event hanlder allows the window to be dragged around the screen. There is a "title bar" that can be
         * dragged around due to this event handler.
         * */
        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /*
         * This event handler checks for what type of mouse press occurred. Since DragMove() does not handle right
         * clicks, they are ignored due to this event handler.
         * */
        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed)
                e.Handled = true;
        }
    }
}

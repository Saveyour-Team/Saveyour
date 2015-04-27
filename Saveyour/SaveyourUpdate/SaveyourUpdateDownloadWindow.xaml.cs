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
    /// </summary>

    internal partial class SaveyourUpdateDownloadWindow : Window
    {
        private WebClient webClient;
        private BackgroundWorker bgWorker;
        private String tempFile;
        private String md5;

        internal String TempFilePath
        {
            get { return this.tempFile; }
        }
        internal SaveyourUpdateDownloadWindow(Uri location, String md5) 
        {
            //may need to add an Icon object to be passed in
            //Supposedly, WPF windows are supposed to have all windows created as the same icon
            //However, Saveyour did not have an icon at the time I was writing this so I can't confirm
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            tempFile = System.IO.Path.GetTempFileName();
            Debug.WriteLine(tempFile);
            this.md5 = md5;
            webClient = new WebClient();

            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted +=webClient_DownloadFileCompleted;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);

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

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = (bool)e.Result;
            this.Close();
        }

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

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            this.lblProgress.Content = String.Format("Downloaded {0} of {1}", FormatBytes(e.BytesReceived, 2, true),
                FormatBytes(e.TotalBytesToReceive, 2, true));
        }

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

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

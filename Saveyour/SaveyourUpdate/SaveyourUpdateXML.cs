using System;
using System.Net;
using System.Xml;

namespace SaveyourUpdate
{
    /// <summary>
    /// This class is the class that will parse the XML file located on the server. It will parse and check if there is any update to get.
    /// </summary>
    internal class SaveyourUpdateXML
    {
        private Version version;
        private Uri uri;
        private String fileName;
        private String md5;
        private String description;
        private String launchArgs;

        internal SaveyourUpdateXML(Version version, Uri uri, String fileName, String md5, String description, String launchArgs)
        {
            this.version = version;
            this.uri = uri;
            this.fileName = fileName;
            this.md5 = md5;
            this.description = description;
            this.launchArgs = launchArgs;
        }

        /*
         * This function checks if the input version if greater than or less than the current version.
         * */
        internal bool IsNewerThan(Version version)
        {
            return this.version > version;
        }

        /*
         * This function checks if there is anything at the URI has anything at that location. It will return true
         * if there is something found at that URI, and false otherwise.
         * */
        internal static bool ExistsOnServer(Uri location)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(location.AbsoluteUri);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                resp.Close();

                return resp.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        /*
         * This function parses the XML file and attempts to get a version, url, file name, md5 hash, description and launch arguments.
         * */
        internal static SaveyourUpdateXML Parse(Uri location, String appID)
        {
            Version version = null;
            String url = "", fileName = "", md5 = "", description = "", launchArgs = "";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(location.AbsoluteUri);

                XmlNode node = doc.DocumentElement.SelectSingleNode("//update[@appId='" + appID + "']");

                if (node == null)
                    return null;

                version = Version.Parse(node["version"].InnerText);
                url = node["url"].InnerText;
                fileName = node["fileName"].InnerText;
                md5 = node["md5"].InnerText;
                description = node["description"].InnerText;
                launchArgs = node["launchArgs"].InnerText;

                return new SaveyourUpdateXML(version, new Uri(url), fileName, md5, description, launchArgs);

            }
            catch
            {
                return null;
            }
        }

        internal Version Version
        {
            get { return this.version; }
        }

        internal Uri Uri
        {
            get { return this.uri; }
        }

        internal String FileName
        {
            get { return this.fileName; }
        }

        internal String MD5
        {
            get { return this.md5; }
        }

        internal String Description
        {
            get { return this.description; }
        }

        internal String LaunchArgs
        {
            get { return this.launchArgs; }
        }
    }
}

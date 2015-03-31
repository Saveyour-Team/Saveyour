using System;
using System.Net;
using System.Xml;
using System.Diagnostics;

namespace SaveyourUpdate
{
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

        internal bool IsNewerThan(Version version)
        {
            return this.version > version;
        }

        internal static bool ExistsOnServer(Uri location)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(location.AbsoluteUri);
                //req.Timeout = 10; //Set time out to 10ms. This is not working as intended and is breaking the functionality so it is commented out for Beta.
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                resp.Close();

                return resp.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

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

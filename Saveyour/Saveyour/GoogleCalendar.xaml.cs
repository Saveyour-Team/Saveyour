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
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for GoogleCalendar.xaml
    /// </summary>
    public partial class GoogleCalendar : Window, Module
    {
        WebBrowser gcalendar = null;
        public GoogleCalendar()
        {
            InitializeComponent();
            gcalendar = (WebBrowser)FindName("calendar");
            gcalendar.Navigate("https://www.google.com/calendar/");


            _DeleteUserLoginCookie();
            HideScriptErrors(gcalendar, true);
            
        }

        public String moduleID()
        {
            return "Google Calendar";
        }

        public Boolean update()
        {
            return false;
        }

        public String save()
        {
            return "Not yet implemented";
        }

        public Boolean load(String pasta)
        {
            return true; // Not yet implementd.
        }

        public Boolean Equals(Module other)
        {
            return other == this;
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser)
                .GetField("calendar",
                          BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember(
                "Silent", BindingFlags.SetProperty, null, objComWebBrowser,
                new object[] { Hide });
        }


        static readonly Uri url1 = new Uri("https://www.google.com/calendar/");
        static readonly Uri url2 = new Uri("https://google.com/");
        private static void _DeleteUserLoginCookie()
        {
            _DeleteEveryCookie(url1);
            _DeleteEveryCookie(url2);
        }
        private static void _DeleteEveryCookie(Uri url)
        {
            string cookie = string.Empty;
            try
            {
                // Get every cookie (Expiration will not be in this response)
                cookie = Application.GetCookie(url);
            }
            catch (Win32Exception)
            {
                // "no more data is available" ... happens randomly so ignore it.
            }
            if (!string.IsNullOrEmpty(cookie))
            {
                // This may change eventually, but seems quite consistent for Facebook.com.
                // ... they split all values w/ ';' and put everything in foo=bar format.
                string[] values = cookie.Split(';');
                foreach (string s in values)
                {
                    if (s.IndexOf('=') > 0)
                    {
                        // Sets value to null with expiration date of yesterday.
                        _DeleteSingleCookie(s.Substring(0, s.IndexOf('=')).Trim(), url);
                    }
                }
            }
        }
        private static void _DeleteSingleCookie(string name, Uri url)
        {
            try
            {
                // Calculate "one day ago"
                DateTime expiration = DateTime.UtcNow - TimeSpan.FromDays(1);
                // Format the cookie as seen on FB.com.  Path and domain name are important factors here.
                string cookie = String.Format("{0}=; expires={1}; path=/; domain=.facebook.com", name, expiration.ToString("R"));
                // Set a single value from this cookie (doesnt work if you try to do all at once, for some reason)
                Application.SetCookie(url, cookie);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Crap. Something happened.");
            }
        }

        public bool Equals(GoogleCalendar other)
        {
            return true; //Since there are no private fields. All Google Calendar modules will be the same.
        }


    }
}

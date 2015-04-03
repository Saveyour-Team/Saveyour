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
            gcalendar = (WebBrowser) FindName("calendar");
            gcalendar.Navigate("https://www.google.com/calendar/");
            
            gcalendar.Navigated += 

            //HideScriptErrors(gcalendar, true);
            
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

        public void HideScriptErrors(WebBrowser wb, bool hide)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null)
            {
                wb.Navigated += (o, s) => HideScriptErrors(wb, hide); //In case we are to early
                return;
            }
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }
        
    }
}

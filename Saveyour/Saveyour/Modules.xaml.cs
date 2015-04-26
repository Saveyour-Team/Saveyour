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
using PluginContracts;

namespace Saveyour
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Modules : Window, Module
    {
        Window qnotes;
        Window weeklytd;
        Window gcalendar;

        public Modules()
        {
            InitializeComponent();

            Dictionary<string, IPlugin> dict = Shell.getPlugins();

            int currentVertical = 0;
            StackPanel stack = new StackPanel();

            PluginGrid.Children.Add(stack);

            foreach (KeyValuePair<string, IPlugin> module in dict)
            {
                if (module.Value != null)
                {
                    Label plugin = new Label();
                    plugin.Width = 295;
                    plugin.Content = module.Value.Name;
                    plugin.FontSize = 16;
                    plugin.BorderBrush = Brushes.Black;
                    plugin.BorderThickness = new Thickness(3);
                    plugin.HorizontalAlignment = HorizontalAlignment.Left;
                    plugin.HorizontalContentAlignment = HorizontalAlignment.Center;
                    plugin.Margin = new Thickness(0, currentVertical, 0, 0);
                    currentVertical += 48;
                    //plugin.VerticalAlignment = VerticalAlignment.Top; //86 down each time

                    stack.Children.Add(plugin);
                }
            }
            stack.Visibility = Visibility.Visible;

        }

        private void QN_Click(object sender, RoutedEventArgs e)
        {
            if (qnotes.IsVisible)
                qnotes.Hide();
            else if (qnotes.IsLoaded)
                qnotes.Show();
        }

        private void WTD_Click(object sender, RoutedEventArgs e)
        {
            if (weeklytd.IsVisible)
                weeklytd.Hide();
            else if (weeklytd.IsLoaded)
                weeklytd.Show();
        }

        private void GC_Click(object sender, RoutedEventArgs e)
        {
            if (gcalendar.IsVisible)
                gcalendar.Hide();
            else if (gcalendar.IsLoaded)
                gcalendar.Show();
        }

        public void addQNotes(Window module)
        {
            qnotes = module;
        }

        public void addWTD(Window module)
        {
            weeklytd = module;
        }

        public void addGC(Window module)
        {
            gcalendar = module;
        }

        public String moduleID()
        {
            return "Settings";
        }

        public Boolean update()
        {
            return false;
        }

        public String save()
        {
            return "";
        }

        public Boolean load(String data)
        {
            return false;
        }

        public Boolean Equals(Module other)
        {
            return false;
        }

        
    }
}

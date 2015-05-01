using System.Windows;
namespace PluginContracts
{
	public interface IPlugin
	{
		string Name { get; }
		void Do();
        
        /*
         * This function only needs to be implemented if the plugin has a Window to display. If it does not, simply return null.
         * */
        Window getInstance();
	}
}

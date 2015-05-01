using System.Windows;
namespace PluginContracts
{
	public interface IPlugin
	{
		string Name { get; }
		void Do();
        Window getInstance();
	}
}

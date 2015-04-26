using PluginContracts;

namespace TaskBatch
{
    public class TaskBatch : IPlugin
    {
        #region IPlugin Members

        public string Name
        {
            get
            {
                return "Task Batch";
            }
        }

        public void Do()
        {
            System.Windows.MessageBox.Show("Do Something in Second Plugin");
        }

        #endregion
    }
}

using PluginContracts;
using System.Windows;

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
            TaskBatchWindow taskBatch = new TaskBatchWindow();
            taskBatch.Show();
        }

        #endregion
    }
}

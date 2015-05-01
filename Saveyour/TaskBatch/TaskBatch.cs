using PluginContracts;
using System.Windows;

namespace TaskBatch
{
    public class TaskBatch : IPlugin
    {
        #region IPlugin Members

        TaskBatchWindow taskBatch;
        public string Name
        {
            get
            {
                return "Task Batch";
            }
        }

        public void Do()
        {
            if(taskBatch == null)
                taskBatch = new TaskBatchWindow();
            taskBatch.Show();
        }

        public Window getInstance()
        {
            if (taskBatch == null)
                taskBatch = new TaskBatchWindow();
            return taskBatch;
        }

        #endregion
    }
}

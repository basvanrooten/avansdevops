using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Backlogs
{
    public class Backlog
    {
        private Project _project;
        private List<BacklogItem> _backlogItems;

        public Backlog(Project project)
        {
            _project = project;
            _backlogItems = new List<BacklogItem>();
        }

        public Project GetProject()
        {
            return _project;
        }

        public void AddBacklogItem(BacklogItem backlogItem)
        {
            if (_backlogItems.Contains(backlogItem))
                throw new NotSupportedException("Can't add the same backlogItem twice");

            _backlogItems.Add(backlogItem);
        }

        public List<BacklogItem> GetBacklogItems()
        {
            return _backlogItems;
        }
    }
}

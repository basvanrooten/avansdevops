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

        public Backlog(Project project)
        {
            _project = project;
        }

        public Project GetProject()
        {
            return _project;
        }

        public void AddBacklogItem()
        {

        }
    }
}

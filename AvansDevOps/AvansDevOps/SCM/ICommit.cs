using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs;

namespace AvansDevOps.SCM
{
    public interface ICommit
    {
        string GetTitle();
        string GetDescription();
        BacklogItem GetBacklogItem();
    }
}

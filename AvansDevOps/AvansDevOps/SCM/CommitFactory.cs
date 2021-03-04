using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs;

namespace AvansDevOps.SCM
{
    public class CommitFactory
    {
        public ICommit MakeCommit(string title, string description, BacklogItem backlogItem)
        {
            return new GitCommit(title, description, backlogItem);
        }
    }
}

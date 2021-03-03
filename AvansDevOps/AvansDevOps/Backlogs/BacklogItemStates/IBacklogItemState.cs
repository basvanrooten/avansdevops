using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Backlogs.BacklogItemStates
{
    public interface IBacklogItemState
    {
        void AddTask(Task task);
        void RemoveTask(Task task);
        void SetName(string newName);
        void SetDescription(string description);
        void SetEffort(int newEffort);
        void NextState();
        void PreviousState();
    }
}

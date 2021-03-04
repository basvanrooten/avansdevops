using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs;
using AvansDevOps.Persons;
using AvansDevOps.Reviews;

namespace AvansDevOps.Sprints.SprintStates
{
    public interface ISprintState
    {
        void SetName(string name);
        void SetReview(Review review);
        void SetStartDate(DateTime startDate);
        void SetEndDate(DateTime endDate);
        void AddDeveloper(Person developer);
        void AddToSprintBacklog(BacklogItem backlogItem);
        void StartStateAction();
        void ToNextState();
        void ToPreviousState();

    }
}

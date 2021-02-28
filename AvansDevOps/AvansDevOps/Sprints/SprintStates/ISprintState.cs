using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;
using AvansDevOps.Reviews;

namespace AvansDevOps.Sprints.SprintStates
{
    public interface ISprintState
    {
        void SetName(string name);
        // TODO: SET REVIEW
        void SetReview(Review review);
        void SetStartDate(DateTime startDate);
        void SetEndDate(DateTime endDate);
        void AddDeveloper(Person developer);
        // TODO: Add backlog items to sprint
        void AddToSprintBacklog();
        void startStateAction();
        void ToNextState();
        void ToPreviousState();

    }
}

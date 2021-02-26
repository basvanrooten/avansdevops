using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Sprints.SprintStates
{
    public interface ISprintState
    {
        void SetSprint(ISprint sprint);
        void SetName(string name);
        // TODO: SET REVIEW
        void SetReview();
        void SetStartDate(DateTime startDate);
        void SetEndDate(DateTime endDate);
        // TODO: Add backlog items to sprint
        void AddToSprintBacklog();
        void startStateAction();

    }
}

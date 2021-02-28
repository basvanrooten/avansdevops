using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;

namespace AvansDevOps.Sprints.SprintStates
{
    public class InitializedState : ISprintState
    {
        private ISprint sprint;

        public InitializedState(ISprint sprint)
        {
            this.sprint = sprint;
        }
        public void SetSprint(ISprint sprint)
        {
            throw new NotImplementedException();
        }

        public void SetName(string name)
        {
            this.sprint.SetName(name);
        }

        public void SetReview()
        {
            throw new NotSupportedException("Can't add a review in Initialized State");
        }

        public void SetStartDate(DateTime startDate)
        {
            this.sprint.SetStartDate(startDate);
        }

        public void SetEndDate(DateTime endDate)
        {
            this.sprint.SetEndDate(endDate);
        }

        public void AddDeveloper(Person developer)
        {
            this.sprint.AddDeveloper(developer);
        }

        public void AddToSprintBacklog()
        {
            throw new NotImplementedException();
        }

        public void startStateAction()
        {
            throw new NotImplementedException();
        }
    }
}

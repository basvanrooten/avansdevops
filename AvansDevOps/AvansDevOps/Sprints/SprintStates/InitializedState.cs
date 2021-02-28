using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;
using AvansDevOps.Reviews;

namespace AvansDevOps.Sprints.SprintStates
{
    public class InitializedState : ISprintState
    {
        private readonly ISprint _sprint;

        public InitializedState(ISprint sprint)
        {
            this._sprint = sprint;
        }

        public void SetName(string name)
        {
            this._sprint.SetName(name);
        }

        public void SetReview(Review review)
        {
            throw new NotSupportedException("Can't add a review in Initialized State");
        }

        public void SetStartDate(DateTime startDate)
        {
            this._sprint.SetStartDate(startDate);
        }

        public void SetEndDate(DateTime endDate)
        {
            this._sprint.SetEndDate(endDate);
        }

        public void AddDeveloper(Person developer)
        {
            this._sprint.AddDeveloper(developer);
        }

        public void AddToSprintBacklog()
        {
            throw new NotImplementedException();
        }

        public void startStateAction()
        {
            throw new NotImplementedException();
        }

        public void ToNextState()
        {
            this._sprint.ChangeState(new ActiveState(_sprint));
                
        }

        public void ToPreviousState()
        {
            throw new NotSupportedException("There is no previous state for initialized");
        }
    }
}

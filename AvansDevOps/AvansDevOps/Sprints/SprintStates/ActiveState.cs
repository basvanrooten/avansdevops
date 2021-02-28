using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;

namespace AvansDevOps.Sprints.SprintStates
{
    public class ActiveState : ISprintState
    {
        private readonly ISprint _sprint;

        public ActiveState(ISprint sprint)
        {
            this._sprint = sprint;
        }

        public void SetName(string name)
        {
            throw new NotSupportedException("Can't change name when sprint is active");
        }

        public void SetReview()
        {
            throw new NotSupportedException("Can't add a review when sprint is active.");
        }

        public void SetStartDate(DateTime startDate)
        {
            throw new NotSupportedException("Can't change startDate when sprint is active");
        }

        public void SetEndDate(DateTime endDate)
        {
            throw new NotSupportedException("Can't change endDate when sprint is active");
        }

        public void AddDeveloper(Person developer)
        {
            this._sprint.AddDeveloper(developer);
        }

        public void AddToSprintBacklog()
        {
            // Probably we should allow this since this can happen in practice a lot.
            throw new NotSupportedException("Can't add to sprint backlog when sprint is active");
        }

        public void startStateAction()
        {
            throw new NotImplementedException();
        }

        public void ToNextState()
        {
            throw new NotImplementedException();
        }

        public void ToPreviousState()
        {
            this._sprint.ChangeState(new InitializedState(_sprint));
        }
    }
}

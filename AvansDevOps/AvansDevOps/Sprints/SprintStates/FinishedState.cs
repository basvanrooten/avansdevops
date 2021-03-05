using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs;
using AvansDevOps.Persons;
using AvansDevOps.Pipelines;
using AvansDevOps.Reviews;

namespace AvansDevOps.Sprints.SprintStates
{
    public class FinishedState : ISprintState
    {
        private readonly ISprint _sprint;
        private IPipeline _pipeline;

        public FinishedState(ISprint sprint)
        {
            this._sprint = sprint;
        }

        public void SetName(string name)
        {
            throw new NotSupportedException("Can't change name when sprint is finished");
        }

        public void SetReview(Review review)
        {
            if (review.GetAuthor() == _sprint.GetScrumMaster())
            {
                this._sprint.SetReview(review);
            }
            else
            {
                throw new SecurityException("Only the scrum master can add a review to a sprint");
            }
        }

        public void SetStartDate(DateTime startDate)
        {
            throw new NotSupportedException("Can't change startDate when sprint is finished");
        }

        public void SetEndDate(DateTime endDate)
        {
            throw new NotSupportedException("Can't change endDate when sprint is finished");
        }

        public void AddDeveloper(Person developer)
        {
            throw new NotSupportedException("Can't add developer when sprint is finished");
        }

        public void AddToSprintBacklog(BacklogItem backlogItem)
        {
            throw new NotSupportedException("Can't add backlogItem when sprint is finished");
        }

        public void StartStateAction()
        {
            switch (_sprint.GetType().Name)
            {
                case "ReleaseSprint":
                    _pipeline = new DevelopmentPipeline(_sprint, EPipelineConfig.Automatic);
                    break;
                case "ReviewSprint":
                    _pipeline = new TestPipeline(_sprint, EPipelineConfig.Automatic);
                    break;
            }

            _pipeline.Execute();
        }

        public void ToNextState()
        {
            throw new NotImplementedException();
        }

        public void ToPreviousState()
        {
            this._sprint.ChangeState(new ActiveState(_sprint));
        }
    }
}

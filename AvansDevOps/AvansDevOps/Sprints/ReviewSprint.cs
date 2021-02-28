using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;
using AvansDevOps.Sprints.SprintStates;

namespace AvansDevOps.Sprints
{
    public class ReviewSprint : ISprint
    {
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;
        private ISprintState state;
        private readonly Project project;
        private readonly Person scrumMaster;
        private readonly List<Person> developers;
        
        //TODO
        //private List<BacklogItem> sprintBacklogItems;
        //private Review review;

        public ReviewSprint(string name, DateTime startDate, DateTime endDate, Project project, Person scrumMaster, List<Person> developers)
        {
            this._name = name;
            this._startDate = startDate;
            this._endDate = endDate;
            this.project = project;
            this.scrumMaster = scrumMaster;
            this.developers = developers;
            this.state = new InitializedState(this);

        }

        public void ChangeState(ISprintState state)
        {
            this.state = state;
        }

        public ISprintState GetState()
        {
            return this.state;
        }

        public void GenerateReport()
        {
            // TODO
            //state.GenerateReport();
        }

        public List<Person> GetDevelopers()
        {
            return developers;
        }

        public void AddDeveloper(Person developer)
        {
            this.developers.Add(developer);
        }

        public DateTime GetEndDate()
        {
            return _endDate;
        }

        public string GetName()
        {
            return _name;
        }

        public Project GetProject()
        {
            return project;
        }

        public Person GetScrumMaster()
        {
            return scrumMaster;
        }

        public DateTime GetStartDate()
        {
            return _startDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            _endDate = endDate;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetStartDate(DateTime startDate)
        {
            _startDate = startDate;
        }
    }
}

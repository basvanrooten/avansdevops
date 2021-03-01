using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;
using AvansDevOps.Reports;
using AvansDevOps.Reviews;
using AvansDevOps.Sprints.SprintStates;

namespace AvansDevOps.Sprints
{
    class ReleaseSprint : ISprint
    {
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;
        private ISprintState _state;
        private readonly Project _project;
        private readonly Person _scrumMaster;
        private readonly List<Person> _developers;

        //TODO
        //private List<BacklogItem> sprintBacklogItems;
        private Review _review;

        public ReleaseSprint(string name, DateTime startDate, DateTime endDate, Project project, Person scrumMaster, List<Person> developers)
        {
            this._name = name;
            this._startDate = startDate;
            this._endDate = endDate;
            this._project = project;
            this._scrumMaster = scrumMaster;
            this._developers = developers;

            this._state = new InitializedState(this);
        }
        public void ChangeState(ISprintState state)
        {
            this._state = state;
        }

        public ISprintState GetState()
        {
            return _state;
        }

        public Person GetScrumMaster()
        {
            return this._scrumMaster;
        }

        public List<Person> GetDevelopers()
        {
            return this._developers;
        }

        public void AddDeveloper(Person person)
        {
            if (this._developers.Contains(person))
                throw new NotSupportedException("Can't add the same person twice");
            this._developers.Add(person);
        }

        public Project GetProject()
        {
            return this._project;
        }

        public string GetName()
        {
            return this._name;
        }

        public void SetName(string name)
        {
            this._name = name;
        }

        public DateTime GetStartDate()
        {
            return this._startDate;
        }

        public void SetStartDate(DateTime startDate)
        {
            this._startDate = startDate;
        }

        public DateTime GetEndDate()
        {
            return this._endDate;
        }

        public void SetReview(Review review)
        {
            this._review = review;
        }

        public Review GetReview()
        {
            return this._review;
        }

        public void SetEndDate(DateTime endDate)
        {
            this._endDate = endDate;
        }

        public Report GenerateReport(EReportBranding branding, List<string> contents, string version, DateTime date, EReportFormat format)
        {
            return branding == EReportBranding.Avans ? ReportDirector.BuildAvansReport(this, contents, version, date, format) : ReportDirector.BuildAvansPlusReport(this, contents, version, date, format);

        }
    }
}

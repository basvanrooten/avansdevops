﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs;
using AvansDevOps.Persons;
using AvansDevOps.Reports;
using AvansDevOps.Reviews;
using AvansDevOps.Sprints.SprintStates;

namespace AvansDevOps.Sprints
{
    public class ReviewSprint : ISprint
    {
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;
        private ISprintState _state;
        private readonly Project _project;
        private readonly Person _scrumMaster;
        private readonly List<Person> _developers;
        private readonly List<BacklogItem> _sprintBacklogItems;
        private Review _review;

        public ReviewSprint(string name, DateTime startDate, DateTime endDate, Project project, Person scrumMaster, List<Person> developers)
        {
            this._name = name;
            this._startDate = startDate;
            this._endDate = endDate;
            this._project = project;
            this._scrumMaster = scrumMaster;
            this._developers = developers;
            this._sprintBacklogItems = new List<BacklogItem>();

            this._state = new InitializedState(this);

        }

        public void ChangeState(ISprintState state)
        {
            this._state = state;
        }

        public ISprintState GetState()
        {
            return this._state;
        }

        public List<Person> GetDevelopers()
        {
            return _developers;
        }

        public void AddDeveloper(Person person)
        {
            this._developers.Add(person);
        }

        public DateTime GetEndDate()
        {
            return _endDate;
        }

        public Review GetReview()
        {
            return this._review;
        }

        public void SetReview(Review review)
        {
            this._review = review;
        }

        public string GetName()
        {
            return _name;
        }

        public List<BacklogItem> GetBacklogItems()
        {
            return _sprintBacklogItems;
        }

        public Project GetProject()
        {
            return _project;
        }

        public Person GetScrumMaster()
        {
            return _scrumMaster;
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

        public Report GenerateReport(EReportBranding branding, List<string> contents, string version, DateTime date, EReportFormat format)
        {
            return branding == EReportBranding.Avans ? ReportDirector.BuildAvansReport(this, contents, version, date, format) : ReportDirector.BuildAvansPlusReport(this, contents, version, date, format);
        }

        public void AddToSprintBacklog(BacklogItem backlogItem)
        {
            if (_sprintBacklogItems.Contains(backlogItem)) return;
            backlogItem.SetSprint(this);
            _sprintBacklogItems.Add(backlogItem);
        }
    }
}

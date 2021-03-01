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
    public interface ISprint
    {
        void ChangeState(ISprintState state);
        ISprintState GetState();
        Person GetScrumMaster();
        List<Person> GetDevelopers();
        void AddDeveloper(Person person);
        Project GetProject();
        string GetName();
        void SetName(string name);
        DateTime GetStartDate();
        void SetStartDate(DateTime startDate);
        DateTime GetEndDate();
        void SetReview(Review review);
        Review GetReview();
        void SetEndDate(DateTime endDate);
        Report GenerateReport(EReportBranding branding, List<string> contents, string version, DateTime date, EReportFormat format);
    }
}

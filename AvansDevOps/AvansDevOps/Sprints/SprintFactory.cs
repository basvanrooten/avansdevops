using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;

namespace AvansDevOps.Sprints
{
    public class SprintFactory
    {

        public void MakeReleaseSprint(string name, DateTime startDate, DateTime endDate, Project project, Person person,
            List<Person> developers)
        {

        }

        public ISprint MakeReviewSprint(string name, DateTime startDate, DateTime endDate, Project project, Person scrumMaster,
            List<Person> developers)
        {
            return new ReviewSprint(name, startDate, endDate, project, scrumMaster, developers);
        }

    }
}

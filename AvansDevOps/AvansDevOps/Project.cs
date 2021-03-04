using AvansDevOps.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs;
using AvansDevOps.Sprints;

namespace AvansDevOps
{
    public class Project
    {
        private Person productOwner;
        private string name;
        private readonly List<ISprint> _sprints;
        private Backlog _backlog;
        private readonly List<Person> _testers;

        public Project(string name, Person productOwner)
        {
            this.name = name;
            this.productOwner = productOwner;
            this._sprints = new List<ISprint>();
            this._testers = new List<Person>();
        }

        public Person GetProductOwner()
        {
            return this.productOwner;
        }

        public void AddBacklog(Backlog backlog)
        {
            _backlog = backlog;
        }

        public Backlog GetBacklog()
        {
            return _backlog;
        }

        public void AddSprint(ISprint sprint)
        {
            this._sprints.Add(sprint);
        }

        public List<ISprint> GetSprints()
        {
            return this._sprints;
        }

        public string GetName()
        {
            return this.name;
        }

        public void AddTester(Person tester)
        {
            if (tester.GetRole() != ERole.Tester)
                throw new NotSupportedException(
                    "Can't add a person to project testers if he doesn't have the tester role.");

            if (_testers.Contains(tester))
                throw new NotSupportedException("Can't add the same person to project testers twice");

            _testers.Add(tester);
        }

        public List<Person> GetTesters()
        {
            return _testers;
        }
    }
}

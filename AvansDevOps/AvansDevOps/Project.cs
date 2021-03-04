using AvansDevOps.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Sprints;

namespace AvansDevOps
{
    public class Project
    {
        private Person productOwner;
        private string name;
        private readonly List<ISprint> _sprints;

        public Project(string name, Person productOwner)
        {
            this.name = name;
            this.productOwner = productOwner;
            this._sprints = new List<ISprint>();
        }

        public Person GetProductOwner()
        {
            return this.productOwner;
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
    }
}

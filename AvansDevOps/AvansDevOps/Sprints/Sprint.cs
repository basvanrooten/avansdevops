using AvansDevOps.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Sprints
{
    public class Sprint
    {
        private readonly int id;
        private readonly string name;
        private readonly string description;
        private readonly Person scrumMaster;
        private readonly List<Person> developers = new List<Person>();

        public Sprint(int id, string name, string description, Person scrumMaster)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.scrumMaster = scrumMaster;
        }

        public int GetId()
        {
            return this.id;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public Person GetScrumMaster()
        {
            return this.scrumMaster;
        }

        public List<Person> GetDevelopers()
        {
            return this.developers;
        }

        public void AddDeveloper(Person developer)
        {
            this.developers.Add(developer);
        }

        public bool RemoveDeveloper(Person developer)
        {
            return this.developers.Remove(developer);
        }
    }
}

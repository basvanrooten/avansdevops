using AvansDevOps.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps
{
    public class Project
    {
        private Person productOwner;
        private string name;

        public Project(string name, Person productOwner)
        {
            this.name = name;
            this.productOwner = productOwner;
        }

        public Person GetProductOwner()
        {
            return this.productOwner;
        }

        public string GetName()
        {
            return this.name;
        }
    }
}

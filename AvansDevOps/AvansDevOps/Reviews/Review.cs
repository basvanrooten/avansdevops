using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;
using AvansDevOps.Sprints;

namespace AvansDevOps.Reviews
{
    public class Review
    {
        private string _review;
        private readonly Person _author;
        private readonly ISprint _sprint;

        public Review(ISprint sprint, Person author, string review)
        {
            this._sprint = sprint;
            this._author = author;
            this._review = review;
        }

        public Person GetAuthor()
        {
            return this._author;
        }
    }
}

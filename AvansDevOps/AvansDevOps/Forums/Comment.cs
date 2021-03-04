using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;

namespace AvansDevOps.Forums
{
    public class Comment
    {
        private Thread _thread;
        private Person _author;
        private DateTime _date;
        private string _comment;

        public Comment(Thread thread, Person author, DateTime date, string comment)
        {
            _thread = thread;
            _author = author;
            _date = date;
            _comment = comment;
        }

        public Thread GeThread()
        {
            return _thread;
        }

        public Person GetAuthor()
        {
            return _author;
        }

        public DateTime GetDate()
        {
            return _date;
        }

        public string GetContent()
        {
            return _comment;
        }
    }
}

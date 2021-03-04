using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using AvansDevOps.Persons;

namespace AvansDevOps.Forums
{
    public class Thread
    {
        private readonly List<Comment> _comments;
        private string _title;
        private DateTime _date;
        private Person _author;
        // private Task _taskReference;

        public Thread(string title, DateTime date, Person author)
        {
            _title = title;
            _date = date;
            _author = author;
            _comments = new List<Comment>();
        }
        
        private void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        private void DeleteComment(Comment comment)
        {
            _comments.Remove(comment);
        }
        private List<Comment> GetComments()
        {
            return _comments;
        }
    }
}

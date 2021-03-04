using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using AvansDevOps.Backlogs;
using AvansDevOps.Persons;

namespace AvansDevOps.Forums
{
    public class Thread
    {
        private readonly List<Comment> _comments;
        private string _title;
        private DateTime _date;
        private Person _author;
        private readonly Task _taskReference;

        public Thread(string title, DateTime date, Person author, Task task)
        {
            _title = title;
            _date = date;
            _author = author;
            _taskReference = task;
            _comments = new List<Comment>();

        }
        
        public void AddComment(Comment comment)
        {
            if (_taskReference.GetState() == ETaskState.Done)
                throw new NotSupportedException("Can't add a comment to thread when the task is marked as done.");

            if (string.IsNullOrWhiteSpace(comment.GetContent()))
                throw new ArgumentNullException(comment.GetContent(), "Content of comment cannot be empty.");
            
            _comments.Add(comment);
        }

        public void DeleteComment(Comment comment)
        {
            if (_taskReference.GetState() == ETaskState.Done)
                throw new NotSupportedException("Can't remove a comment to thread when the task is marked as done.");

            if (!_comments.Exists(comment.Equals))
                throw new NotSupportedException("Can't remove comment that does not exists.");

            _comments.Remove(comment);
        }

        public List<Comment> GetComments()
        {
            return _comments;
        }

        public Task GetTask()
        {
            return _taskReference;
        }

        public string GetTitle()
        {
            return _title;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs;

namespace AvansDevOps.Forums
{
    public class Forum
    {
        private readonly List<Thread> _threads;

        public Forum()
        {
            _threads = new List<Thread>();
        }

        public void NewThread(Thread thread)
        {
            if (thread.GetTask().GetState() == ETaskState.Done)
                throw new NotSupportedException("Can't add thread when the task is marked as done.");

            if (string.IsNullOrWhiteSpace(thread.GetTitle()))
                throw new ArgumentNullException(thread.GetTitle(), "Title of thread cannot be empty.");

            _threads.Add(thread);
        }

        public void ArchiveThread(Thread thread)
        {
            if(!_threads.Exists(thread.Equals))
                throw new NotSupportedException("Can't remove thread that does not exists.");

            _threads.Remove(thread);
        }
        
        public List<Thread> GetThreads()
        {
            return this._threads;
        }
    }
}

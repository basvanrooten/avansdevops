using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _threads.Add(thread);
        }

        public void ArchiveThread(Thread thread)
        {
            _threads.Remove(thread);
        }

        public List<Thread> GetThreads()
        {
            return this._threads;
        }
    }
}

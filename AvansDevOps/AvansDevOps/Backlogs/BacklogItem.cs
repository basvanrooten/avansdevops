using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Backlogs.BacklogItemStates;
using AvansDevOps.Persons;
using AvansDevOps.Sprints;

namespace AvansDevOps.Backlogs
{
    public class BacklogItem
    {
        private string _name;
        private string _description;
        private List<Task> _tasks;
        private Person _assignedPerson;
        private int _effort;
        private IBacklogItemState _state;
        private readonly Backlog _backlogReference;
        private ISprint _sprintReference;

        public BacklogItem(string name, string description, Person assignedPerson, int effort, Backlog backlog)
        {
            _name = name;
            _description = description;
            _assignedPerson = assignedPerson;
            _effort = effort;
            _state = new TodoState(this);
            _backlogReference = backlog;
        }

        public void SetSprint(ISprint sprint)
        {
            _sprintReference = sprint;
        }

        public ISprint GetSprint()
        {
            return _sprintReference;
        }

        public Backlog GetBacklog()
        {
            return _backlogReference;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public string GetDescription()
        {
            return _description;
        }

        public void SetEffort(int effort)
        {
            _effort = effort;
        }

        public int GetEffort()
        {
            return _effort;
        }

        public void AssignPerson(Person newAssignee)
        {
            _assignedPerson = newAssignee;
        }

        public Person GetAssignedPerson()
        {
            return _assignedPerson;
        }

        public void ChangeState(IBacklogItemState state)
        {
            // BR: The state of the backlogItem can only be changed once it has a sprint reference
            if (_sprintReference == null)
                throw new NotSupportedException(
                    "Can't change the state of a backlogItem because it is not in a sprint");
            _state = state;
        }

        public IBacklogItemState GetState()
        {
            return _state;
        }

        public List<Task> GetTasks()
        {
            return _tasks;
        }

        public void AddTask(Task task)
        {
            // BR: If a backlogItem has tasks, remove the assignedPerson, and create a new task for the assignedPerson with the description

            if (_assignedPerson  != null && task != null)
            {
                var convertedBacklogItemToTask = new Task(_description, _assignedPerson);
                _assignedPerson = null;
                _description = null;

                _tasks = new List<Task>() { convertedBacklogItemToTask };
            }


            if (_tasks == null)
            {
                _tasks = new List<Task>() { task };
            }
            else
            {
                _tasks.Add(task);
            }
        }

        public bool RemoveTask(Task task)
        {
            if (_tasks == null)
            {
                throw new NullReferenceException("There are no tasks in this backlogItem");
            }

            return _tasks.Remove(task);
        }

    }
}

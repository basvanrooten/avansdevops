﻿using System;
using System.Collections.Generic;
using AvansDevOps.Persons;

namespace AvansDevOps.Backlogs.BacklogItemStates
{
    public class DoneState : IBacklogItemState
    {
        private readonly BacklogItem _backlogItem;

        public DoneState(BacklogItem backlogItem)
        {
            _backlogItem = backlogItem;

            // BR: Let's send every developer from the backlogItem a notification that their item is approved.

            var tempList = new List<Person>();

            if (_backlogItem.GetTasks().Count >= 1)
                foreach (var task in backlogItem.GetTasks())
                {
                    tempList.Add(task.GetAssignedPerson());
                }

            if (_backlogItem.GetAssignedPerson() != null)
                tempList.Add(_backlogItem.GetAssignedPerson());

            // Send messages
            foreach (var involvedPerson in tempList)
            {
                involvedPerson.SendNotification($"Hi {involvedPerson.GetName()}, backlog item {_backlogItem.GetName()} is done! Kudos!");
            }

        }

        public void AddTask(Task task)
        {
            throw new NotSupportedException("Can't add task when the backlogItem is finished");
        }

        public void RemoveTask(Task task)
        {
            throw new NotSupportedException("Can't remove task when the backlogItem is finished");
        }

        public void SetName(string newName)
        {
            throw new NotSupportedException("Can't set name when the backlogItem is finished");
        }

        public void SetDescription(string description)
        {
            throw new NotSupportedException("Can't set description when the backlogItem is finished");
        }

        public void SetEffort(int newEffort)
        {
            throw new NotSupportedException("Can't set effort when the backlogItem is finished");
        }

        public void NextState()
        {
            throw new NotSupportedException("Can't go to next state. BacklogItem is finished");
        }

        public void PreviousState()
        {
            throw new NotSupportedException("Can't undo the state of backlogItem, because it is already finished.");
        }
    }
}

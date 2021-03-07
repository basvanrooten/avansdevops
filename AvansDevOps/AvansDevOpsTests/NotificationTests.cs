using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps;
using AvansDevOps.Backlogs;
using AvansDevOps.Backlogs.BacklogItemStates;
using AvansDevOps.Notifications;
using AvansDevOps.Persons;
using AvansDevOps.Sprints;
using Task = AvansDevOps.Backlogs.Task;
using Xunit;

namespace AvansDevOpsTests
{
    class NotificationTests
    {

    }

    public partial class Backlog_Notification_Tests
    {
        [Fact]
        public void Backlog_Can_Register_One_Observer()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);

            var backlogItem = new BacklogItem("User can login into the platform", "Foo", p2, 3, backlog);

            backlogItem.AssignPerson(p2);
            backlog.AddBacklogItem(backlogItem);

            sprint.AddToSprintBacklog(backlogItem);

            project.AddBacklog(backlog);

            var task1 = new Task("Bar", p1);
            backlogItem.GetState().AddTask(task1);

            var backlogItemObserver = new BacklogItemObserver();

            // Act
            backlogItem.Register(backlogItemObserver);

            // Assert
            Assert.Single(backlogItem.GetObservers());
        }

        [Fact]
        public void Backlog_Can_Register_Multiple_Observers()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);

            var backlogItem = new BacklogItem("User can login into the platform", "Foo", p2, 3, backlog);

            backlogItem.AssignPerson(p2);
            backlog.AddBacklogItem(backlogItem);

            sprint.AddToSprintBacklog(backlogItem);

            project.AddBacklog(backlog);

            var task1 = new Task("Bar", p1);
            backlogItem.GetState().AddTask(task1);

            var backlogItemObserver = new BacklogItemObserver();

            // Act
            // TODO: Add sprint observer
            backlogItem.Register(backlogItemObserver);

            // Assert
            Assert.Single(backlogItem.GetObservers());
        }

        [Fact]
        public void Backlog_Can_Unregister_Observer()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);

            var backlogItem = new BacklogItem("User can login into the platform", "Foo", p2, 3, backlog);

            backlogItem.AssignPerson(p2);
            backlog.AddBacklogItem(backlogItem);

            sprint.AddToSprintBacklog(backlogItem);

            project.AddBacklog(backlog);

            var task1 = new Task("Bar", p1);
            backlogItem.GetState().AddTask(task1);

            var backlogItemObserver = new BacklogItemObserver();

            // Act
            backlogItem.Register(backlogItemObserver);
            backlogItem.Unregister(backlogItemObserver);

            // Assert
            Assert.Empty(backlogItem.GetObservers());
        }

        [Fact]
        public void Backlog_Can_Not_Unregister_Observer()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);

            var backlogItem = new BacklogItem("User can login into the platform", "Foo", p2, 3, backlog);

            backlogItem.AssignPerson(p2);
            backlog.AddBacklogItem(backlogItem);

            sprint.AddToSprintBacklog(backlogItem);

            project.AddBacklog(backlog);

            var task1 = new Task("Bar", p1);
            backlogItem.GetState().AddTask(task1);

            var backlogItemObserver = new BacklogItemObserver();

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => backlogItem.Unregister(backlogItemObserver));
            Assert.Empty(backlogItem.GetObservers());
        }

        [Fact]
        public void Backlog_Can_Not_Register_The_Same_Observer()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);

            var backlogItem = new BacklogItem("User can login into the platform", "Foo", p2, 3, backlog);

            backlogItem.AssignPerson(p2);
            backlog.AddBacklogItem(backlogItem);

            sprint.AddToSprintBacklog(backlogItem);

            project.AddBacklog(backlog);

            var task1 = new Task("Bar", p1);
            backlogItem.GetState().AddTask(task1);

            var backlogItemObserver = new BacklogItemObserver();

            // Act
            backlogItem.Register(backlogItemObserver);

            // Assert
            Assert.Throws<NotSupportedException>(() => backlogItem.Register(backlogItemObserver));
            Assert.Single(backlogItem.GetObservers());
        }

    }
}

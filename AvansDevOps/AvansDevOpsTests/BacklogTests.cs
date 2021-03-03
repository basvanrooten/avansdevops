using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvansDevOps;
using AvansDevOps.Backlogs;
using AvansDevOps.Backlogs.BacklogItemStates;
using AvansDevOps.Persons;
using AvansDevOps.Sprints;
using Xunit;

namespace AvansDevOpsTests
{
    public class BacklogTests
    {
        [Fact]
        public void Adding_A_Backlog_To_A_Project_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);
            Person p3 = new Person("Derk Jan", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            // Assert
            Assert.Equal(backlog, project.GetBacklog());

        }

        [Fact]
        public void Adding_BacklogItems_To_A_Project_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);
            Person p3 = new Person("Derk Jan", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);
            project.AddBacklog(backlog);

            // Act
            var backlogItem1 = new BacklogItem("User can login into the platform", "Foo", p2, 3, backlog);
            backlog.AddBacklogItem(backlogItem1);

            // Assert
            Assert.Contains(backlogItem1, project.GetBacklog().GetBacklogItems());
            Assert.Equal("TodoState", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetState().GetType().Name);
        }

        [Fact]
        public void Adding_Duplicate_BacklogItems_To_A_Backlog_Should_Throw_NotSupportedException()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);
            Person p3 = new Person("Derk Jan", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);
            project.AddBacklog(backlog);

            // Act
            var backlogItem1 = new BacklogItem("User can login into the platform", "Foo", p2, 3, backlog);
            
            backlog.AddBacklogItem(backlogItem1);

            // Assert
            Assert.Throws<NotSupportedException>(() => backlog.AddBacklogItem(backlogItem1));
        }

        [Fact]
        public void Adding_Tasks_To_A_BacklogItem_Should_Convert_BacklogItem_To_Another_Task()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);
            Person p3 = new Person("Derk Jan", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);
            project.AddBacklog(backlog);

            // Act
            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);
            backlog.AddBacklogItem(backlogItem1);

            var newTask = new Task("Don't look here", p1);

            backlogItem1.AddTask(newTask);

            // Assert
            Assert.Equal(2, backlogItem1.GetTasks().Count);
            Assert.Equal(2, backlog.GetBacklogItems().First().GetTasks().Count);
            Assert.Null(backlog.GetBacklogItems().First().GetAssignedPerson());
            Assert.Equal("Look here",
                backlog.GetBacklogItems().First().GetTasks().Find((task => task.GetDescription() == "Look here"))
                    .GetDescription());

            Assert.Equal(p2,
                backlog.GetBacklogItems().First().GetTasks().Find((task => task.GetAssignedPerson() == p2))
                    .GetAssignedPerson());

            Assert.Equal(p1,
                backlog.GetBacklogItems().First().GetTasks().Find((task => task.GetAssignedPerson() == p1))
                    .GetAssignedPerson());

        }

        [Fact]
        public void Changing_A_BacklogItem_State_Without_A_Sprint_Should_Throw_NotSupportedException()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Roos", ERole.Developer);
            Person p3 = new Person("Derk Jan", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            var backlog = new Backlog(project);
            project.AddBacklog(backlog);

            // Act
            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);
            backlog.AddBacklogItem(backlogItem1);

            // Assert
            Assert.Throws<NotSupportedException>(() => backlogItem1.ChangeState(new DoingState(backlogItem1)));
        }
    }
}

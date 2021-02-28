using System;
using System.Collections.Generic;
using System.Linq;
using AvansDevOps;
using AvansDevOps.Persons;
using AvansDevOps.Sprints;
using Xunit;

namespace AvansDevOpsTests
{
    public class SprintTests
    {
        [Fact]
        public void Changing_SprintName_Should_Not_Throw_Exception_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas"));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom");
            Person p2 = new Person("Jan Peter");

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project,  p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            // Act

            sprint.GetState().SetName("foo");

            // Assert
            Assert.Equal("foo", project.GetSprints().First().GetName());

        }

        [Fact]
        public void Changing_StartDate_Should_Not_Throw_Exception_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas"));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom");
            Person p2 = new Person("Jan Peter");

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            // Act

            sprint.GetState().SetStartDate(DateTime.Parse("2010-10-10T00:00:00Z"));


            // Assert
            Assert.Equal(DateTime.Parse("2010-10-10T00:00:00Z"), project.GetSprints().First().GetStartDate());

        }

        [Fact]
        public void Changing_EndDate_Should_Not_Throw_Exception_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas"));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom");
            Person p2 = new Person("Jan Peter");

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            // Act

            sprint.GetState().SetEndDate(DateTime.Parse("2010-10-10T00:00:00Z").AddDays(7));


            // Assert
            Assert.Equal(DateTime.Parse("2010-10-17T00:00:00Z"), project.GetSprints().First().GetEndDate());

        }

        [Fact]
        public void Adding_Developer_Should_Not_Throw_Exception_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas"));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom");
            Person p2 = new Person("Jan Peter");
            Person p3 = new Person("Dick Bruna");

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            
            // Act
            sprint.GetState().AddDeveloper(p3);


            // Assert
            Assert.Contains(p3, sprint.GetDevelopers());

        }

        [Fact]
        public void Set_Review_Should_Throw_NotSupportedException_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas"));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom");
            Person p2 = new Person("Jan Peter");

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetReview());

        }
    }
}

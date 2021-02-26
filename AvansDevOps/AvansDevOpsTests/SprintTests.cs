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

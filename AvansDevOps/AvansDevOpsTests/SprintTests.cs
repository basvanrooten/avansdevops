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


    }

    public partial class Sprint_InitializedState_Tests {
        /***
        *      _____       _ _      _____ _        _         _______        _       
        *     |_   _|     (_) |    / ____| |      | |       |__   __|      | |      
        *       | |  _ __  _| |_  | (___ | |_ __ _| |_ ___     | | ___  ___| |_ ___ 
        *       | | | '_ \| | __|  \___ \| __/ _` | __/ _ \    | |/ _ \/ __| __/ __|
        *      _| |_| | | | | |_   ____) | || (_| | ||  __/    | |  __/\__ \ |_\__ \
        *     |_____|_| |_|_|\__| |_____/ \__\__,_|\__\___|    |_|\___||___/\__|___/
        *                                                                           
        *                                                                           
        */

        [Fact]
        public void Changing_SprintName_Should_Not_Throw_Exception_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
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

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

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

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

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

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);
            Person p3 = new Person("Dick Bruna", ERole.Developer);

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

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetReview());

        }

        [Fact]
        public void Changing_To_Next_State_Should_Not_Throw_Exception_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            // Act
            project.GetSprints().First().GetState().ToNextState();


            // Assert
            Assert.Equal("ActiveState", project.GetSprints().First().GetState().GetType().Name);

        }

        [Fact]
        public void Changing_To_Previous_State_Should_Throw_NotSupportedException_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().ToPreviousState());

        }
    }

    public partial class Sprint_ActiveState_Tests
    {

        /***
         *                   _   _              _____ _        _         _______        _       
         *         /\       | | (_)            / ____| |      | |       |__   __|      | |      
         *        /  \   ___| |_ ___   _____  | (___ | |_ __ _| |_ ___     | | ___  ___| |_ ___ 
         *       / /\ \ / __| __| \ \ / / _ \  \___ \| __/ _` | __/ _ \    | |/ _ \/ __| __/ __|
         *      / ____ \ (__| |_| |\ V /  __/  ____) | || (_| | ||  __/    | |  __/\__ \ |_\__ \
         *     /_/    \_\___|\__|_| \_/ \___| |_____/ \__\__,_|\__\___|    |_|\___||___/\__|___/
         *                                                                                      
         *                                                                                      
         */

        [Fact]
        public void Changing_SprintName_Should_Throw_NotSupportedException_In_ActiveState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            // Act
            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetName("Foo"));

        }

        [Fact]
        public void Changing_StartDate_Should_Throw_NotSupportedException_In_ActiveState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetStartDate(DateTime.Parse("2010-10-10T00:00:00Z")));

        }

        [Fact]
        public void Changing_EndDate_Should_Throw_NotSupportedException_In_ActiveState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetEndDate(DateTime.Parse("2010-10-17T00:00:00Z")));

        }

        [Fact]
        public void Adding_Developer_Should_Not_Throw_Exception_In_ActiveState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);
            Person p3 = new Person("Dick Bruna", ERole.Developer);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();

            // Act
            sprint.GetState().AddDeveloper(p3);


            // Assert
            Assert.Contains(p3, sprint.GetDevelopers());

        }

        [Fact]
        public void Set_Review_Should_Throw_NotSupportedException_In_ActiveState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetReview());

        }

        [Fact]
        public void Changing_To_Previous_State_Should_Not_Throw_Exception_In_ActiveState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();

            // Act
            project.GetSprints().First().GetState().ToPreviousState();


            // Assert
            Assert.Equal("InitializedState", project.GetSprints().First().GetState().GetType().Name);

        }
    }
}

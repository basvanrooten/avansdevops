using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using AvansDevOps;
using AvansDevOps.Persons;
using AvansDevOps.Reports;
using AvansDevOps.Reviews;
using AvansDevOps.Sprints;
using Xunit;

namespace AvansDevOpsTests
{
    public class SprintTests
    {
        [Fact]
        public void Generating_A_Report_Should_Not_Throw_Exception()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            ISprint sprint2 = factory.MakeReleaseSprint("Sprint 2", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddSprint(sprint2);

            // Act
            DateTime now = DateTime.Now;
            Report sprint1GeneratedAvansReport = sprint.GenerateReport(EReportBranding.Avans, new List<string>() { "Burndown chart: foo", "Velocity: 21" }, "v1.0", now, EReportFormat.PDF);
            Report sprint1GeneratedAvansPlusReport = sprint.GenerateReport(EReportBranding.AvansPlus, new List<string>() { "Burndown chart: bar", "Velocity: 13" }, "v2.0", now, EReportFormat.PNG);


            Report sprint2GeneratedAvansReport = sprint.GenerateReport(EReportBranding.Avans, new List<string>() { "Burndown chart: foo", "Velocity: 21" }, "v1.0", now, EReportFormat.PDF);
            Report sprint2GeneratedAvansPlusReport = sprint.GenerateReport(EReportBranding.AvansPlus, new List<string>() { "Burndown chart: bar", "Velocity: 13" }, "v2.0", now, EReportFormat.PNG);


            // Assert
            Assert.Equal(now, sprint1GeneratedAvansReport.Header.Date);
            Assert.Equal("Avans", sprint1GeneratedAvansReport.Header.CompanyName);
            Assert.Equal(new List<string>() { "Burndown chart: foo", "Velocity: 21" }, sprint1GeneratedAvansReport.Contents);
            Assert.Equal("v1.0", sprint1GeneratedAvansReport.Header.ReportVersion);
            Assert.Equal(EReportFormat.PDF, sprint1GeneratedAvansReport.Format);

            Assert.Equal(now, sprint1GeneratedAvansPlusReport.Header.Date);
            Assert.Equal("Avans+", sprint1GeneratedAvansPlusReport.Header.CompanyName);
            Assert.Equal(new List<string>() { "Burndown chart: bar", "Velocity: 13" }, sprint2GeneratedAvansPlusReport.Contents);
            Assert.Equal("v2.0", sprint1GeneratedAvansPlusReport.Header.ReportVersion);
            Assert.Equal(EReportFormat.PNG, sprint1GeneratedAvansPlusReport.Format);

            Assert.Equal(now, sprint2GeneratedAvansReport.Header.Date);
            Assert.Equal("Avans", sprint2GeneratedAvansReport.Header.CompanyName);
            Assert.Equal(new List<string>() { "Burndown chart: foo", "Velocity: 21" }, sprint2GeneratedAvansReport.Contents);
            Assert.Equal("v1.0", sprint2GeneratedAvansReport.Header.ReportVersion);
            Assert.Equal(EReportFormat.PDF, sprint1GeneratedAvansReport.Format);

            Assert.Equal(now, sprint2GeneratedAvansPlusReport.Header.Date);
            Assert.Equal("Avans+", sprint2GeneratedAvansPlusReport.Header.CompanyName);
            Assert.Equal(new List<string>() { "Burndown chart: bar", "Velocity: 13" }, sprint2GeneratedAvansPlusReport.Contents);
            Assert.Equal("v2.0", sprint2GeneratedAvansPlusReport.Header.ReportVersion);
            Assert.Equal(EReportFormat.PNG, sprint2GeneratedAvansPlusReport.Format);
        }

    }

    public partial class FeedbackSprint_InitializedState_Tests {
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

            Review review = new Review(sprint, p1, "No good");
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetReview(review));

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

    public partial class FeedbackSprint_ActiveState_Tests
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
            Review review = new Review(sprint, p1, "No good");

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetReview(review));

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

    public partial class FeedbackSprint_FinishedState_Tests
    {

        /***
         *      ______ _       _     _              _    _____ _        _         _______        _       
         *     |  ____(_)     (_)   | |            | |  / ____| |      | |       |__   __|      | |      
         *     | |__   _ _ __  _ ___| |__   ___  __| | | (___ | |_ __ _| |_ ___     | | ___  ___| |_ ___ 
         *     |  __| | | '_ \| / __| '_ \ / _ \/ _` |  \___ \| __/ _` | __/ _ \    | |/ _ \/ __| __/ __|
         *     | |    | | | | | \__ \ | | |  __/ (_| |  ____) | || (_| | ||  __/    | |  __/\__ \ |_\__ \
         *     |_|    |_|_| |_|_|___/_| |_|\___|\__,_| |_____/ \__\__,_|\__\___|    |_|\___||___/\__|___/
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
        public void Changing_EndDate_Should_Throw_NotSupportedException_In_FinishedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetEndDate(DateTime.Parse("2010-10-17T00:00:00Z")));

        }

        [Fact]
        public void Adding_Developer_Should_Throw_NotSupportedException_In_FinishedState()
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
            sprint.GetState().ToNextState();

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().AddDeveloper(p3));
            Assert.DoesNotContain(p3, sprint.GetDevelopers());

        }

        [Fact]
        public void Set_Review_Should_Not_Throw_Exception_In_FinishedState_When_Added_By_ScrumMaster()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();

            Review review = new Review(sprint, p1, "Good work guys!");

            // Act
            sprint.GetState().SetReview(review);


            // Assert
            Assert.Equal(review, sprint.GetReview());

        }

        [Fact]
        public void Set_Review_Should_Throw_SecurityException_In_FinishedState_When_Added_By_Someone_Else_But_ScrumMaster()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();

            Review review = new Review(sprint, p2, "Good work guys!");

            // Act

            // Assert
            Assert.Throws<SecurityException>(() => project.GetSprints().First().GetState().SetReview(review));

        }

        [Fact]
        public void Changing_To_Previous_State_Should_Not_Throw_Exception_In_FinishedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();

            // Act
            project.GetSprints().First().GetState().ToPreviousState();


            // Assert
            Assert.Equal("ActiveState", project.GetSprints().First().GetState().GetType().Name);

        }

    }

    public partial class ReleaseSprint_InitializedState_Tests
    {
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);

            Review review = new Review(sprint, p1, "No good");
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetReview(review));

        }

        [Fact]
        public void Changing_To_Next_State_Should_Not_Throw_Exception_In_InitializedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().ToPreviousState());

        }
    }

    public partial class ReleaseSprint_ActiveState_Tests
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            Review review = new Review(sprint, p1, "No good");

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetReview(review));

        }

        [Fact]
        public void Changing_To_Previous_State_Should_Not_Throw_Exception_In_ActiveState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();

            // Act
            project.GetSprints().First().GetState().ToPreviousState();


            // Assert
            Assert.Equal("InitializedState", project.GetSprints().First().GetState().GetType().Name);

        }
    }

    public partial class ReleaseSprint_FinishedState_Tests
    {

        /***
         *      ______ _       _     _              _    _____ _        _         _______        _       
         *     |  ____(_)     (_)   | |            | |  / ____| |      | |       |__   __|      | |      
         *     | |__   _ _ __  _ ___| |__   ___  __| | | (___ | |_ __ _| |_ ___     | | ___  ___| |_ ___ 
         *     |  __| | | '_ \| / __| '_ \ / _ \/ _` |  \___ \| __/ _` | __/ _ \    | |/ _ \/ __| __/ __|
         *     | |    | | | | | \__ \ | | |  __/ (_| |  ____) | || (_| | ||  __/    | |  __/\__ \ |_\__ \
         *     |_|    |_|_| |_|_|___/_| |_|\___|\__,_| |_____/ \__\__,_|\__\___|    |_|\___||___/\__|___/
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
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

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetStartDate(DateTime.Parse("2010-10-10T00:00:00Z")));

        }

        [Fact]
        public void Changing_EndDate_Should_Throw_NotSupportedException_In_FinishedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();
            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().SetEndDate(DateTime.Parse("2010-10-17T00:00:00Z")));

        }

        [Fact]
        public void Adding_Developer_Should_Throw_NotSupportedException_In_FinishedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);
            Person p3 = new Person("Dick Bruna", ERole.Developer);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Parse("2010-10-10T00:00:00Z"), DateTime.Parse("2010-10-10T00:00:00Z").AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => project.GetSprints().First().GetState().AddDeveloper(p3));
            Assert.DoesNotContain(p3, sprint.GetDevelopers());

        }

        [Fact]
        public void Set_Review_Should_Not_Throw_Exception_In_FinishedState_When_Added_By_ScrumMaster()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();

            Review review = new Review(sprint, p1, "Good work guys!");

            // Act
            sprint.GetState().SetReview(review);


            // Assert
            Assert.Equal(review, sprint.GetReview());

        }

        [Fact]
        public void Set_Review_Should_Throw_SecurityException_In_FinishedState_When_Added_By_Someone_Else_But_ScrumMaster()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();

            Review review = new Review(sprint, p2, "Good work guys!");

            // Act

            // Assert
            Assert.Throws<SecurityException>(() => project.GetSprints().First().GetState().SetReview(review));

        }

        [Fact]
        public void Changing_To_Previous_State_Should_Not_Throw_Exception_In_FinishedState()
        {
            // Arrange

            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            Person p2 = new Person("Jan Peter", ERole.Tester);

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            sprint.GetState().ToNextState();
            sprint.GetState().ToNextState();

            // Act
            project.GetSprints().First().GetState().ToPreviousState();


            // Assert
            Assert.Equal("ActiveState", project.GetSprints().First().GetState().GetType().Name);

        }

    }
}

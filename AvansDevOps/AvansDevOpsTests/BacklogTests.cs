using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvansDevOps;
using AvansDevOps.Backlogs;
using AvansDevOps.Backlogs.BacklogItemStates;
using AvansDevOps.Channels;
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
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));

            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

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

            backlogItem1.GetState().AddTask(newTask);

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

    public partial class TodoStateBacklogItemsTests
    {

        /***
         *      _______        _          _____ _        _         _______        _       
         *     |__   __|      | |        / ____| |      | |       |__   __|      | |      
         *        | | ___   __| | ___   | (___ | |_ __ _| |_ ___     | | ___  ___| |_ ___ 
         *        | |/ _ \ / _` |/ _ \   \___ \| __/ _` | __/ _ \    | |/ _ \/ __| __/ __|
         *        | | (_) | (_| | (_) |  ____) | || (_| | ||  __/    | |  __/\__ \ |_\__ \
         *        |_|\___/ \__,_|\___/  |_____/ \__\__,_|\__\___|    |_|\___||___/\__|___/
         *                                                                                
         *                                                                                
         */

        [Fact]
        public void Adding_And_Removing_Tasks_From_BacklogItem_In_InitialState_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            // Assert
            Assert.Equal(4, backlogItem1.GetTasks().Count);
            backlogItem1.GetState().RemoveTask(task1);
            Assert.Equal(3, backlogItem1.GetTasks().Count);
            Assert.NotNull(backlogItem1.GetTasks().Find((task => task.GetDescription() == "Look here" )));
            Assert.NotNull(backlogItem1.GetTasks().Find((task => task.GetDescription() == "lorem" )));
            Assert.NotNull(backlogItem1.GetTasks().Find((task => task.GetDescription() == "ipsum" )));
        }

        [Fact]
        public void Changing_Name_or_Effort_or_Description_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            backlogItem1.GetState().SetName("Test1");
            backlogItem1.GetState().SetDescription("Test2");
            backlogItem1.GetState().SetEffort(1);

            // Assert
            Assert.Equal("Test1", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetName());
            Assert.Equal("Test2", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetDescription());
            Assert.Equal(1, project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetEffort());
        }

        [Fact]
        public void To_Previous_State_Should_Throw_NotSupportedException()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            // Assert
            Assert.Throws<NotSupportedException>(() => backlogItem1.GetState().PreviousState());
        }

        [Fact]
        public void To_Next_State_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            backlogItem1.GetState().NextState();

            // Assert
            Assert.Equal("DoingState", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetState().GetType().Name);
        }
    }

    public partial class DoingStateBacklogItemsTests
    {

        /***
         *      _____        _                _____ _        _         _______        _       
         *     |  __ \      (_)              / ____| |      | |       |__   __|      | |      
         *     | |  | | ___  _ _ __   __ _  | (___ | |_ __ _| |_ ___     | | ___  ___| |_ ___ 
         *     | |  | |/ _ \| | '_ \ / _` |  \___ \| __/ _` | __/ _ \    | |/ _ \/ __| __/ __|
         *     | |__| | (_) | | | | | (_| |  ____) | || (_| | ||  __/    | |  __/\__ \ |_\__ \
         *     |_____/ \___/|_|_| |_|\__, | |_____/ \__\__,_|\__\___|    |_|\___||___/\__|___/
         *                            __/ |                                                   
         *                           |___/                                                    
         */


        [Fact]
        public void Adding_And_Removing_Tasks_From_BacklogItem_In_DoingState_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            backlogItem1.GetState().NextState();

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            // Assert
            Assert.Equal(4, backlogItem1.GetTasks().Count);
            backlogItem1.GetState().RemoveTask(task1);
            Assert.Equal(3, backlogItem1.GetTasks().Count);
            Assert.NotNull(backlogItem1.GetTasks().Find((task => task.GetDescription() == "Look here")));
            Assert.NotNull(backlogItem1.GetTasks().Find((task => task.GetDescription() == "lorem")));
            Assert.NotNull(backlogItem1.GetTasks().Find((task => task.GetDescription() == "ipsum")));
        }

        [Fact]
        public void Changing_Name_or_Effort_or_Description_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            backlogItem1.GetState().NextState();

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            backlogItem1.GetState().SetName("Test1");
            backlogItem1.GetState().SetDescription("Test2");
            backlogItem1.GetState().SetEffort(1);

            // Assert
            Assert.Equal("Test1", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetName());
            Assert.Equal("Test2", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetDescription());
            Assert.Equal(1, project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetEffort());
        }

        [Fact]
        public void To_Previous_State_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            backlogItem1.GetState().NextState();

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);


            backlogItem1.GetState().PreviousState();

            // Assert
            Assert.Equal("TodoState", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetState().GetType().Name);
        }

        [Fact]
        public void To_Next_State_With_Tasks_On_Todo_Should_Throw_NotSupportedException()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            backlogItem1.GetState().NextState();

            // Assert
            Assert.Throws<NotSupportedException>(() => backlogItem1.GetState().NextState());
            Assert.Equal("DoingState", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetState().GetType().Name);
        }

        [Fact]
        public void To_Next_State_With_No_Tasks_On_Todo_Should_Not_Throw_Exception()
        {
            // Arrange
            Project project = new Project("Test Project", new Person("Bas", ERole.Lead));
            SprintFactory factory = new SprintFactory();

            Person p1 = new Person("Tom", ERole.Developer);
            p1.AddChannel(new EmailChannel("tom@tom.com"));
            p1.AddChannel(new SlackChannel("teumaas"));
            Person p2 = new Person("Jan Roos", ERole.Developer);
            p2.AddChannel(new EmailChannel("jan-roos@c.com"));
            Person p3 = new Person("Derk Jan", ERole.Tester);
            p3.AddChannel(new SlackChannel("Derk-jan"));
            Person p4 = new Person("Berend botje", ERole.Developer);
            p4.AddChannel(new EmailChannel("berend@c.com"));
            Person p5 = new Person("Lars", ERole.Developer);
            p5.AddChannel(new EmailChannel("lars@c.com"));


            ISprint sprint = factory.MakeReleaseSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), project, p1, new List<Person>() { p2 });
            project.AddSprint(sprint);
            project.AddTester(p3);

            // Act
            var backlog = new Backlog(project);
            project.AddBacklog(backlog);


            var backlogItem1 = new BacklogItem("User can login into the platform", "Look here", p2, 3, backlog);

            backlog.AddBacklogItem(backlogItem1);
            sprint.AddToSprintBacklog(backlogItem1);
            backlogItem1.GetState().NextState();

            var task1 = new Task("Bar", p4);
            var task2 = new Task("lorem", p5);
            var task3 = new Task("ipsum", p5);
            backlogItem1.GetState().AddTask(task1);
            backlogItem1.GetState().AddTask(task2);
            backlogItem1.GetState().AddTask(task3);

            backlogItem1.GetTasks().First().NextState();
            task1.NextState();
            task2.NextState();
            task3.NextState();

            backlogItem1.GetState().NextState();

            // Assert
            Assert.Equal("ReadyToTestState", project.GetBacklog().GetBacklogItems().Find(item => item == backlogItem1).GetState().GetType().Name);
        }
    }
}

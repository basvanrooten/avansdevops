using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvansDevOps.Backlogs;
using AvansDevOps.Channels;
using AvansDevOps.Forums;
using AvansDevOps.Persons;
using Xunit;
using Xunit.Sdk;

namespace AvansDevOpsTests
{
    class ForumTests
    {
    }

    /***
     *      ______                           _______        _       
     *     |  ____|                         |__   __|      | |      
     *     | |__ ___  _ __ _   _ _ __ ___      | | ___  ___| |_ ___ 
     *     |  __/ _ \| '__| | | | '_ ` _ \     | |/ _ \/ __| __/ __|
     *     | | | (_) | |  | |_| | | | | | |    | |  __/\__ \ |_\__ \
     *     |_|  \___/|_|   \__,_|_| |_| |_|    |_|\___||___/\__|___/
     *                                                              
     *                                                              
     */
    public partial class Forum_InitializedState_Tests
    {
        [Fact]
        public void NewThread_Can_Add_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);
            
            // Act
            forum.NewThread(threadOne);

            // Assert
            Assert.Contains(threadOne, forum.GetThreads());
        }

        [Fact]
        public void NewThread_Can_Remove_Multiple_Threads()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);

            // Act
            forum.NewThread(threadOne);
            forum.ArchiveThread(threadOne);

            // Assert
            Assert.DoesNotContain(threadOne, forum.GetThreads());
        }

        [Fact]
        public void NewThread_Can_Remove_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);

            // Act
            forum.NewThread(threadOne);
            forum.ArchiveThread(threadOne);

            // Assert
            Assert.DoesNotContain(threadOne, forum.GetThreads());
        }

        [Fact]
        public void NewThread_Can_Not_Remove_Thread_Should_Throw_NotSupportedException()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => forum.ArchiveThread(threadOne));
            Assert.DoesNotContain(threadOne, forum.GetThreads());
        }

        [Fact]
        public void NewThread_Can_Add_Multiple_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";
            const string threadTwoName = "ThreadTwo title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);
            Thread threadTwo = new Thread(threadTwoName, currentDateTime, person, task);

            // Act
            forum.NewThread(threadOne);
            forum.NewThread(threadTwo);

            // Assert
            Assert.Contains(threadOne, forum.GetThreads());
            Assert.Contains(threadTwo, forum.GetThreads());
        }

        [Fact]
        public void NewThread_Can_Not_Remove_Multiple_Threads_Should_Throw_NotSupportedException()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";
            const string threadTwoName = "ThreadTwo title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);
            Thread threadTwo = new Thread(threadTwoName, currentDateTime, person, task);

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => forum.ArchiveThread(threadOne));
            Assert.Throws<NotSupportedException>(() => forum.ArchiveThread(threadTwo));
            Assert.DoesNotContain(threadOne, forum.GetThreads());
            Assert.DoesNotContain(threadTwo, forum.GetThreads());
        }

        [Fact]
        public void NewThread_Can_Add_Thread_On_Task_State_ToDo()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);

            // Act
            forum.NewThread(threadOne);

            // Assert
            Assert.Equal(ETaskState.Todo, task.GetState());
            Assert.Contains(threadOne, forum.GetThreads());
        }
        
        [Fact]
        public void NewThread_Can_Add_Thread_On_Task_State_Active()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);

            // Act
            task.NextState();
            forum.NewThread(threadOne);

            // Assert
            Assert.Equal(ETaskState.Active, task.GetState());
            Assert.Contains(threadOne, forum.GetThreads());
        }
        
        [Fact]
        public void NewThread_Cant_Add_Thread_On_Task_State_Done_Should_Throw_NotSupportedException()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);

            // Act
            task.NextState();
            task.NextState();

            // Assert
            Assert.Equal(ETaskState.Done, task.GetState());
            Assert.Throws<NotSupportedException>(() => forum.NewThread(threadOne));
        }

        [Fact]
        public void NewThread_Cant_Add_Thread_With_Empty_Title_Should_Throw_ArgumentNullException()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string threadOneName = "";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person, task);

            // Act


            // Assert
            Assert.Throws<ArgumentNullException>(() => forum.NewThread(threadOne));
        }

        [Fact]
        public void AddComment_Can_Add_Comment_To_Existing_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string name = "ThreadOne title.";

            Thread thread = new Thread(name, currentDateTime, person, task);

            Person personTwo = new Person("Tom", ERole.Developer);
            const string contentCommentOne = "This is a test comment one.";

            Comment comment = new Comment(thread, personTwo, currentDateTime, contentCommentOne);

            // Act
            forum.NewThread(thread);
            thread.AddComment(comment);

            // Assert
            Assert.Contains(thread, forum.GetThreads());
            Assert.Contains(comment, thread.GetComments());
        }

        [Fact]
        public void AddComment_Can_Add_Multiple_Comments_To_Existing_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string name = "ThreadOne title.";

            Thread thread = new Thread(name, currentDateTime, person, task);

            Person personTwo = new Person("Tom", ERole.Developer);
            const string contentCommentOne = "This is a test comment one.";

            Person personThree = new Person("Jan", ERole.Developer);
            const string contentCommentTwo = "This is a test comment two.";

            Comment commentOne = new Comment(thread, personTwo, currentDateTime, contentCommentOne);
            Comment commentTwo = new Comment(thread, personThree, currentDateTime, contentCommentTwo);

            // Act
            forum.NewThread(thread);
            thread.AddComment(commentOne);
            thread.AddComment(commentTwo);

            // Assert
            Assert.Contains(thread, forum.GetThreads());
            Assert.Contains(commentOne, thread.GetComments());
            Assert.Contains(commentTwo, thread.GetComments());
        }

        [Fact]
        public void AddComment_Can_Remove_Comment_To_Existing_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string name = "ThreadOne title.";

            Thread thread = new Thread(name, currentDateTime, person, task);

            Person personTwo = new Person("Tom", ERole.Developer);
            const string contentCommentOne = "This is a test comment one.";

            Comment comment = new Comment(thread, personTwo, currentDateTime, contentCommentOne);

            // Act
            forum.NewThread(thread);
            thread.AddComment(comment);
            thread.DeleteComment(comment);

            // Assert
            Assert.Contains(thread, forum.GetThreads());
            Assert.DoesNotContain(comment, thread.GetComments());
        }

        [Fact]
        public void AddComment_Can_Remove_Multiple_Comments_To_Existing_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            Task task = new Task("Sample Task", person);
            const string name = "ThreadOne title.";

            Thread thread = new Thread(name, currentDateTime, person, task);

            Person personTwo = new Person("Tom", ERole.Developer);
            const string contentCommentOne = "This is a test comment one.";

            Person personThree = new Person("Jan", ERole.Developer);
            const string contentCommentTwo = "This is a test comment two.";

            Comment commentOne = new Comment(thread, personTwo, currentDateTime, contentCommentOne);
            Comment commentTwo = new Comment(thread, personThree, currentDateTime, contentCommentTwo);

            // Act
            forum.NewThread(thread);
            thread.AddComment(commentOne);
            thread.AddComment(commentTwo);
            thread.DeleteComment(commentOne);
            thread.DeleteComment(commentTwo);

            // Assert
            Assert.Contains(thread, forum.GetThreads());
            Assert.DoesNotContain(commentOne, thread.GetComments());
            Assert.DoesNotContain(commentTwo, thread.GetComments());
        }
    }
}

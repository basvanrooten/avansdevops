using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Channels;
using AvansDevOps.Forums;
using AvansDevOps.Persons;
using Xunit;

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
        public void Forum_Can_Add_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            const string threadOneName = "ThreadOne title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person);
            
            // Act
            forum.NewThread(threadOne);

            // Assert
            Assert.Contains(threadOne, forum.GetThreads());
        }

        [Fact]
        public void Forum_Can_Add_Multiple_Thread()
        {
            // Arrange
            Forum forum = new Forum();

            DateTime currentDateTime = DateTime.Now;
            Person person = new Person("Bas", ERole.Lead);
            const string threadOneName = "ThreadOne title.";
            const string threadTwoName = "ThreadTwo title.";

            Thread threadOne = new Thread(threadOneName, currentDateTime, person);
            Thread threadTwo = new Thread(threadTwoName, currentDateTime, person);

            // Act
            forum.NewThread(threadOne);
            forum.NewThread(threadTwo);

            // Assert
            Assert.Contains(threadOne, forum.GetThreads());
        }

    }
}

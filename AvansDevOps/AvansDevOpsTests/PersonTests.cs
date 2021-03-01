using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Channels;
using AvansDevOps.Persons;
using Xunit;
using Xunit.Sdk;

namespace AvansDevOpsTests
{
    public class PersonTests
    {
    }

    public partial class Person_Notification_Tests
    {
        /***
         *      _____                            _   _       _   _  __ _           _   _               _______        _       
         *     |  __ \                          | \ | |     | | (_)/ _(_)         | | (_)             |__   __|      | |      
         *     | |__) |__ _ __ ___  ___  _ __   |  \| | ___ | |_ _| |_ _  ___ __ _| |_ _  ___  _ __      | | ___  ___| |_ ___ 
         *     |  ___/ _ \ '__/ __|/ _ \| '_ \  | . ` |/ _ \| __| |  _| |/ __/ _` | __| |/ _ \| '_ \     | |/ _ \/ __| __/ __|
         *     | |  |  __/ |  \__ \ (_) | | | | | |\  | (_) | |_| | | | | (_| (_| | |_| | (_) | | | |    | |  __/\__ \ |_\__ \
         *     |_|   \___|_|  |___/\___/|_| |_| |_| \_|\___/ \__|_|_| |_|\___\__,_|\__|_|\___/|_| |_|    |_|\___||___/\__|___/
         *                                                                                                                    
         *                                                                                                                    
         */

        [Fact]

        public void Person_Can_Add_One_Channel()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);

            // Act
            IChannel channelOne = channelFactory.CreateEmailChannel("bas@avans.nl");
            person.AddChannel(channelOne);

            // Assert
            Assert.Contains(channelOne, person.GetChannels());
        }

        [Fact]
        public void Person_Can_Add_Multiple_Channels()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);

            // Act
            IChannel channelOne = channelFactory.CreateEmailChannel("bas@avans.nl");
            IChannel channelTwo = channelFactory.CreateEmailChannel("@bas");
            person.AddChannel(channelOne);
            person.AddChannel(channelTwo);

            // Assert
            Assert.Contains(channelOne, person.GetChannels());
            Assert.Contains(channelTwo, person.GetChannels());
        }

        [Fact]
        public void EmailChannel_Send_Correct_Notification_Should_Not_Throw_Exception()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);
            IChannel channelEmail = channelFactory.CreateEmailChannel("bas@avans.nl");
            const string message = "This is a test e-mail.";

            // Act
            person.AddChannel(channelEmail);

            // Assert
            var ex = Record.Exception(() => person.SendNotification(message));
            Assert.Null(ex);
        }

        [Fact]
        public void SlackChannel_Send_Correct_Notification_Should_Not_Throw_Exception()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);
            IChannel channelSlack = channelFactory.CreateSlackChannel("@bas");
            const string message = "This is a test message.";

            // Act
            person.AddChannel(channelSlack);

            // Assert
            var ex = Record.Exception(() => person.SendNotification(message));
            Assert.Null(ex);
        }

        [Fact]
        public void Multiple_Channels_Send_Correct_Notification_Should_Not_Throw_Exception()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);
            IChannel channelEmail = channelFactory.CreateSlackChannel("bas@avans.nl");
            IChannel channelSlack = channelFactory.CreateSlackChannel("@bas");
            const string message = "This is a test message.";

            // Act
            person.AddChannel(channelSlack);
            person.AddChannel(channelEmail);

            // Assert
            var ex = Record.Exception(() => person.SendNotification(message));
            Assert.Null(ex);
        }

        [Fact]
        public void EmailChannel_Send_Empty_Notification_Should_Throw_ArgumentNullException()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);

            IChannel emailChannel = channelFactory.CreateEmailChannel("bas@avans.nl");

            const string message = "";

            // Act
            person.AddChannel(emailChannel);

            // Assert
            Assert.Throws<ArgumentNullException>(() => person.SendNotification(message));
        }

        [Fact]
        public void SlackChannel_Send_Empty_Notification_Should_Throw_ArgumentNullException()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);

            IChannel slackChannel = channelFactory.CreateEmailChannel("@bas");

            const string message = "";

            // Act
            person.AddChannel(slackChannel);

            // Assert
            Assert.Throws<ArgumentNullException>(() => person.SendNotification(message));
        }

        [Fact]
        public void EmailChannel_Send_Out_Of_Bounds_Notification_Should_Throw_ArgumentOutOfRangeException()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);

            IChannel emailChannel = channelFactory.CreateEmailChannel("bas@avans.nl");

            string message = "";

            while (message.Length <= 1600)
            {
                message += "0";
            }

            // Act
            person.AddChannel(emailChannel);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => person.SendNotification(message));
        }


        [Fact]
        public void SlackChannel_Send_Out_Of_Bounds_Notification_Should_Throw_ArgumentOutOfRangeException()
        {
            // Arrange
            ChannelFactory channelFactory = new ChannelFactory();
            Person person = new Person("Bas", ERole.Lead);

            IChannel slackChannel = channelFactory.CreateSlackChannel("@bas");

            string message = "";

            while (message.Length <= 1600)
            {
                message += "0";
            }

            // Act
            person.AddChannel(slackChannel);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => person.SendNotification(message));
        }
    }
}

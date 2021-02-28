using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Channels;
using AvansDevOps.Persons;
using Xunit;

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

        public void Person_Can_Add_Channel()
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
    }
}

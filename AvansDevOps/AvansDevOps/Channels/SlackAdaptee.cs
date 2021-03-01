﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Channels
{
    public class SlackAdaptee
    {
        private readonly string _username;

        public SlackAdaptee(string username)
        {
            this._username = username;
        }

        public void SendMessage(string message)
        {
            //Validate Inputs
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message), "Message cannot be empty.");

            //Must be less than or equal 1600 characters
            if (message.Length >= 1600)
                throw new ArgumentOutOfRangeException(nameof(message), "Message cannot be longer than 1600 chars");

            Console.WriteLine($"Slack message has been sent to: {this._username} Message: {message}");
        }
    }
}

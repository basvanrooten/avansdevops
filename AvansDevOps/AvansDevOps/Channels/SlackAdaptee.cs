using System;
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
            Console.WriteLine($"Slack message has been sent to: {this._username} Message: {message}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Channels
{
    public class EmailChannel : IChannel
    {
        private string _email;
        private readonly EmailAdaptee _adaptee;

        public EmailChannel(string email)
        {
            this._email = email;
            this._adaptee = new EmailAdaptee(this._email);
        }

        public void SendMessage(string message)
        {
            this._adaptee.SendMessage(message);
        }
    }
}

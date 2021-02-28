using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Channels
{
    public class EmailAdaptee
    {
        private readonly string _email;

        public EmailAdaptee(string email)
        {
            this._email = email;
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"E-mail has been sent to: {this._email} Message: {message}");
        }
    }
}

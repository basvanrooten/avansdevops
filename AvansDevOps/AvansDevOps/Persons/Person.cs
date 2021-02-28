using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Channels;

namespace AvansDevOps.Persons
{
    public class Person
    {
        private string _name;
        private readonly ERole _role;
        private readonly List<IChannel> _channels;

        public Person(string name, ERole role)
        {
            this._name = name;
            this._role = role;
            this._channels = new List<IChannel>();

        }

        public void SendNotification(string message)
        {
            foreach (var channel in _channels)
            {
                channel.SendMessage(message);
            }
        }

        public void AddChannel(IChannel channel)
        {
            this._channels.Add(channel);
        }

        public ERole GetRole()
        {
            return this._role;
        }

        public string GetName()
        {
            return this._name;
        }

        public void SetName(string name)
        {
            this._name = name;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Agent.Console
{
    public class MessageEventArgs : EventArgs
    {
        public IMessage Message { get; private set; }

        public MessageEventArgs(IMessage message)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message));

            Message = message;
        }
    }
}

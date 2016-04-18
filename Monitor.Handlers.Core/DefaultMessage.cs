using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor.Core
{
    public class DefaultMessage : IMessage
    {
        public DefaultMessage( string channel = "Default", 
                               string origin  = "Default" )
        {
            Channel = channel;
            Origin = origin;
        }
        public string Channel { get; set; }
        public string Origin { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> CustomAttributes { get; set; }
    }
}

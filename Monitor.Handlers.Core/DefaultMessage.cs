using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor.Core
{
    public class DefaultMessage : Message
    {
        public DefaultMessage( string channel = "Default", 
                               string origin  = "Default" )
        {
            Channel = channel;
            Origin = origin;
        }
    }
}

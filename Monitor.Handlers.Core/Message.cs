using System;
using System.Collections.Generic;

namespace Monitor.Core
{
    public abstract class Message
    {
        public string Channel { get; protected set; }
        public string Origin { get; protected set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> CustomAttributes { get; set; }
    }
}
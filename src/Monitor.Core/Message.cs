using System;
using System.Collections.Generic;

namespace Monitor.Core
{
    public abstract class Message
    {
        public string Channel { get; set; }
        public string Origin { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> CustomAttributes { get; set; }
    }
}
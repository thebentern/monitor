using System;
using System.Collections.Generic;

namespace Monitor.Core
{
    public interface IMessage
    {
        string Channel { get; set; }
        string Origin { get; set; }
        DateTime Timestamp { get; set; }
        string Content { get; set; }
        Dictionary<string, string> CustomAttributes { get; set; }
    }
}
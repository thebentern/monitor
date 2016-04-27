using System;
using NUnit.Framework;
using Fclp;
using FluentAssertions;
using Monitor.Agent.Console;

namespace Monitor.Tests.Units.Agent.Console
{
    public class MessageEventArgsTest
    {
        [Test]
        public void Throw_Exception_When_Message_Is_Missing() =>
           Assert.Throws<ArgumentNullException>(() => new MessageEventArgs(null));
    }
}

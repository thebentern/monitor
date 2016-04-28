using System;
using NUnit.Framework;
using Fclp;
using FluentAssertions;
using Monitor.Agent.Console;

namespace Monitor.Tests.Units.Agent.Console
{
    public class ProcessTest
    {
        [Test]
        public void Throw_Exception_When_Publisher_Is_Missing() =>
            Assert.Throws<ArgumentNullException>(() => new Process(null));
    }
}

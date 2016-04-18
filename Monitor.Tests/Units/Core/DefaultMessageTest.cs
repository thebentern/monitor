using System;
using NUnit.Framework;
using Monitor.Core;

namespace Monitor.Tests.Units.Core
{
    public class DefaultMessageTest
    {
        private readonly string DefaultChannel = "Default";
        private readonly string DefaultOrigin = "Default";

        [Test]
        public void Initializes_With_Default_Channel_And_Origin()
        {
            DefaultMessage message = new DefaultMessage();

            Assert.AreEqual( message.Channel, DefaultChannel );
            Assert.AreEqual( message.Origin, DefaultOrigin );
        }
    }
}

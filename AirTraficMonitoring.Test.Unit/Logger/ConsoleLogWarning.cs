using System;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Logger
{
    [Author("MSK")]

    [TestFixture]
    public class ConsoleLogWarning
    {
        private ConsoleLog _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new ConsoleLog();
        }

        [Test]
        public void LogWarning_ArgumentIsNull_ThrowsException()
        {
            string argument = null;

            Assert.That(() => _uut.LogWarning(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogWarning_ArgumentIsEmptyString_ThrowsException()
        {
            var argument = String.Empty;

            Assert.That(() => _uut.LogWarning(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogWarning_ArgumentIsWhiteSpaceString_ThrowException()
        {
            var argument = "   ";

            Assert.That(() => _uut.LogWarning(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

    }
}

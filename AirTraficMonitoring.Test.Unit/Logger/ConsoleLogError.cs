using System;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Logger
{
    [Author("MSK")]

    [TestFixture]
    public class ConsoleLogError
    {
        private ConsoleLog _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new ConsoleLog();
        }

        [Test]
        public void LogError_ArgumentIsNull_ThrowsException()
        {
            string argument = null;

            Assert.That(() => _uut.LogError(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogError_ArgumentIsEmptyString_ThrowsException()
        {
            var argument = string.Empty;

            Assert.That(() => _uut.LogError(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogError_ArgumentIsWhiteSpaceString_ThrowException()
        {
            const string argument = "   ";

            Assert.That(() => _uut.LogError(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogError_ArgumentIsNotNullOrWhiteSpace_NoExceptionThrown()
        {
            const string argument = "Valid String";

            Assert.That(() => _uut.LogError(argument), Throws.Nothing);
        }
    }
}

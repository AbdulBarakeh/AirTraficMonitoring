using System;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace AirTraficMonitoring.Test.Unit.Logger
{
    [Author("MSK")]

    [TestFixture]
    public class ConsoleLogError
    {
        private ConsoleLog _uut;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _uut = new ConsoleLog { _log = Substitute.For<ILogger>() };
            _logger = _uut._log;
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
            string argument = String.Empty;

            Assert.That(() => _uut.LogError(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogError_ArgumentIsWhiteSpaceString_ThrowException()
        {
            string argument = "   ";

            Assert.That(() => _uut.LogError(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogError_ArgumentValid_SerilogErrorCalled()
        {
            string argument = "Valid argument";

            _uut.LogError(argument);

            _logger.Received().Error(argument);
        }
    }
}

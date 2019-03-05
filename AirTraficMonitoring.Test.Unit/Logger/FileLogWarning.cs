using System;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace AirTraficMonitoring.Test.Unit.Logger
{
    [TestFixture]
    public class FileLogWarning
    {
        private FileLog _uut;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _uut = new FileLog { _log = Substitute.For<ILogger>() };
            _logger = _uut._log;
        }

        [Test]
        public void LogWarning_ArgumentIsNull_ThrowsException()
        {
            string argument = null;

            Assert.That(() => _uut.LogWarning(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>());
        }

        [Test]
        public void LogWarning_ArgumentIsEmptyString_ThrowsException()
        {
            string argument = String.Empty;

            Assert.That(() => _uut.LogWarning(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>());
        }

        [Test]
        public void LogWarning_ArgumentIsWhiteSpaceString_ThrowException()
        {
            string argument = "   ";

            Assert.That(() => _uut.LogWarning(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>());
        }

        [Test]
        public void LogWarning_ArgumentValid_SerilogWarningCalled()
        {
            string argument = "Valid argument";

            _uut.LogWarning(argument);

            _logger.Received().Warning(argument);
        }
    }
}

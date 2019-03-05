using System;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace AirTraficMonitoring.Test.Unit.Logger
{
    public class FileLogInformation
    {
        private FileLog _uut;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _uut = new FileLog();
            _uut._log = Substitute.For<ILogger>();
            _logger = _uut._log;

        }

        [Test]
        public void LogInformation_ArgumentIsNull_ThrowsException()
        {
            string argument = null;
            
            Assert.That(() => _uut.LogInformation(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>());
        }

        [Test]
        public void LogInformation_ArgumentIsEmptyString_ThrowsException()
        {
            string argument = String.Empty;

            Assert.That(() => _uut.LogInformation(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>());
        }

        [Test]
        public void LogInformation_ArgumentIsWhiteSpaceString_ThrowException()
        {
            string argument = "   ";

            Assert.That(() => _uut.LogInformation(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>());
        }

        [Test]
        public void LogInformation_ArgumentValid_SerilogInformationCalled()
        {
            string argument = "Valid argument";

            _uut.LogInformation(argument);

            _logger.Received().Information(argument);
        }
    }
}

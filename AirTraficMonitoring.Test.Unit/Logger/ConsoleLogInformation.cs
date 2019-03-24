using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Logger
{
    [Author("MSK")]

    [TestFixture]
    public class ConsoleLogInformation
    {
        private ConsoleLog _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new ConsoleLog();
        }

        [Test]
        public void LogInformation_ArgumentIsNull_ThrowsException()
        {
            string argument = null;

            Assert.That(() => _uut.LogInformation(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogInformation_ArgumentIsEmptyString_ThrowsException()
        {
            var argument = string.Empty;

            Assert.That(() => _uut.LogInformation(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogInformation_ArgumentIsWhiteSpaceString_ThrowException()
        {
            const string argument = "   ";

            Assert.That(() => _uut.LogInformation(argument), Throws.TypeOf<LoggerArgumentIsNullOrWhiteSpaceException>()
                .With.Message.EqualTo(LoggerExceptionMessage.ArgumentNotValid));
        }

        [Test]
        public void LogInformation_ArgumentIsNotNullOrWhiteSpace_NoExceptionThrown()
        {
            const string argument = "Valid String";

            Assert.That(() => _uut.LogInformation(argument), Throws.Nothing);
        }
    }
}

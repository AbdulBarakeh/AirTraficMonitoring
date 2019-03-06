using System;
using AirTraficMonitoring.Logger.Exceptions;
using Serilog;

namespace AirTraficMonitoring.Logger
{
    public class ConsoleLog : ILog
    {
        public ILogger _log { get; set; }

        public ConsoleLog()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void LogInformation(string info)
        {
            if (String.IsNullOrWhiteSpace(info))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException(LoggerExceptionMessage.ArgumentNotValid);
            }
            _log.Information(info);
        }

        public void LogWarning(string warning)
        {
            if (String.IsNullOrWhiteSpace(warning))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException(LoggerExceptionMessage.ArgumentNotValid);
            }
            _log.Warning(warning);
        }

        public void LogError(string error)
        {
            if (String.IsNullOrWhiteSpace(error))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException(LoggerExceptionMessage.ArgumentNotValid);
            }
            _log.Error(error);
        }
    }
}

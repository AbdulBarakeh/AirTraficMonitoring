using System;
using AirTraficMonitoring.Logger.Exceptions;
using Serilog;

namespace AirTraficMonitoring.Logger
{
    public class FileLog : ILog
    {
        public ILogger _log { get; set;}
        public FileLog()
        {
            _log = new LoggerConfiguration()
                .WriteTo.RollingFile("../../../Log/log-{Date}.txt")
                .CreateLogger();
        }

        public void LogInformation(string info)
        {
            if (String.IsNullOrWhiteSpace(info))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException(); 
            }
            _log.Information(info);
        }

        public void LogWarning(string warning)
        {
            if (String.IsNullOrWhiteSpace(warning))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException();
            }
            _log.Warning(warning);
        }

        public void LogError(string error)
        {
            if (String.IsNullOrWhiteSpace(error))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException();
            }
            _log.Error(error);
        }
    }
}

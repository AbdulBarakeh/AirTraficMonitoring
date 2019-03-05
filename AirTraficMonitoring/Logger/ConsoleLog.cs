using System;
using Serilog;

namespace AirTraficMonitoring.Logger
{
    public class ConsoleLog : ILog
    {
        private ILogger _log;

        public ConsoleLog()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void LogInformation(string info)
        {
            _log.Information(info);
        }

        public void LogWarning(string warning)
        {
            _log.Warning(warning);
        }

        public void LogError(string error)
        {
            _log.Error(error);
        }
    }
}

using System;
using AirTraficMonitoring.Logger.Exceptions;

namespace AirTraficMonitoring.Logger
{
    public class ConsoleLog : ILog
    {
        public void LogInformation(string info)
        {
            if (string.IsNullOrWhiteSpace(info))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException(LoggerExceptionMessage.ArgumentNotValid);
            }
            Console.WriteLine(info);
        }

        public void LogWarning(string warning)
        {
            if (string.IsNullOrWhiteSpace(warning))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException(LoggerExceptionMessage.ArgumentNotValid);
            }
            Console.WriteLine(warning);
        }

        public void LogError(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                throw new LoggerArgumentIsNullOrWhiteSpaceException(LoggerExceptionMessage.ArgumentNotValid);
            }
            Console.WriteLine(error);
        }
    }
}

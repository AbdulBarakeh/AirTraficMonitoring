using System;

namespace AirTraficMonitoring.Logger.Exceptions
{
    public class LoggerArgumentIsNullOrWhiteSpaceException : Exception
    {
        public LoggerArgumentIsNullOrWhiteSpaceException()
        {
        
        }

        public LoggerArgumentIsNullOrWhiteSpaceException(string message) : base(message)
        {
            
        }
    }
}

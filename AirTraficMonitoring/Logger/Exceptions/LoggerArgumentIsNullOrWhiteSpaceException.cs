using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

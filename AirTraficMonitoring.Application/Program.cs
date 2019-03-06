using System;
using System.Configuration;
using AirTraficMonitoring.Logger;

namespace AirTraficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog file = new ConsoleLog();

            file.LogInformation(ConfigurationManager.AppSettings["LoggerExceptionMessage"]);
        }
    }
}

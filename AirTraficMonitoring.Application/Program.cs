using System.Threading;
using AirTraficMonitoring.Decoder;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Monitor;
using AirTraficMonitoring.Separation;
using TransponderReceiver;

namespace AirTraficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var airspace = new Airspace(80000, 80000, 500, 20000);
            var decoder = new FlightDecoder(airspace, TransponderReceiverFactory.CreateTransponderDataReceiver());

            var separation = new FlightSeparation(airspace);
            var monitor = new ConsoleMonitor(new ConsoleLog(), airspace, separation);

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}

using AirTraficMonitoring.Decoder;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Separation;
using System.Threading;
using TransponderReceiver;

namespace AirTraficMonitoring.Application
{
    class Program
    {
        public static void Main(string[] args)
        {
            var airspace = new Airspace(80000, 80000, 100, 20000);
            var decoder = new FlightDecoder(airspace, TransponderReceiverFactory.CreateTransponderDataReceiver());

            var separation = new FlightSeparation(airspace);

            while (true)
            {
                Thread.Sleep(1000);
            }

        }
    }
}

using System.Threading;
using AirTraficMonitoring.Decoder;
using AirTraficMonitoring.FlightAirspace;
using TransponderReceiver;

namespace AirTraficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var airspace = new Airspace(80000, 80000, 500, 20000);
            var decoder = new FlightDecoder(airspace, TransponderReceiverFactory.CreateTransponderDataReceiver());

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}

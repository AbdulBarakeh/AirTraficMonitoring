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
            var airspace = new Airspace(0, 0, 0, 0);
            var decoder = new FlightDecoder(airspace, TransponderReceiverFactory.CreateTransponderDataReceiver());

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}

using AirTraficMonitoring.Decoder;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Separation;
using TransponderReceiver;

namespace AirTraficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var airspace = new Airspace(0, 0, 0, 0);
            var decoder = new FlightDecoder(airspace, TransponderReceiverFactory.CreateTransponderDataReceiver());
            var separation = new FlightSeparation(airspace);

            while (true) { }
        }
    }
}

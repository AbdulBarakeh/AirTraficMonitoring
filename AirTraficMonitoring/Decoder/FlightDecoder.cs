
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using System;
using TransponderReceiver;

namespace AirTraficMonitoring.Decoder
{
    public class FlightDecoder : IDecoder
    {
        private readonly IAirspace _airspace;

        public FlightDecoder(IAirspace airspace, ITransponderReceiver receiver)
        {
            _airspace = airspace;
            receiver.TransponderDataReady += DecoderEventHandler;
        }

        public void DecoderEventHandler(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                var separated = data.Split(';');

                _airspace.Add(new FlightTrack(
            separated[0],
           Convert.ToDouble(separated[1]),
           Convert.ToDouble(separated[2]),
            Convert.ToDouble(separated[3]),
           separated[4]));
            }
        }
    }
}
using AirTraficMonitoring.Track;
using System;
using TransponderReceiver;

namespace AirTraficMonitoring.Decoder
{
    public class FlightDecoder : IDecoder
    {
        public static ITransponderReceiver receiver;
        private Track.FlightTrack newTrack;
        public void DecoderEventHandler(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                var Seperated = data.Split(';');

                newTrack = new FlightTrack(Seperated[0], Convert.ToDouble(Seperated[1]), Convert.ToDouble(Seperated[2]), Convert.ToDouble(Seperated[3]), Seperated[4]);
            }
            
        }


    }
}
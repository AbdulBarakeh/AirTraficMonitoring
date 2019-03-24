
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using System;
using System.Collections.Generic;
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
            e.TransponderData.ForEach(data =>
            {
                var trackInfo = Decrypt(data);

                _airspace.Add(new FlightTrack(trackInfo[0], trackInfo[1], trackInfo[2], trackInfo[3], trackInfo[4]));
            });
        }

        public List<string> Decrypt(string transponderMessage)
        {
            return new List<string>(transponderMessage.Split(';'));
        }
    }
}
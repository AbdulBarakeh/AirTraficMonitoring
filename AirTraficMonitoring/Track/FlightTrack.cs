using System;
using System.Collections.Generic;

namespace AirTraficMonitoring.Track
{
    public class FlightTrack : ITrack
    {
        public FlightTrack(string tag, double xpos, double ypos, double alt, string time)
        {
            Tag = tag;
            XPosition = xpos;
            YPosition = ypos;
            Altitude = alt;
            TimeStamp = time;
            Velocity = 0;
            CompassCourse = 0;
        }

        public string Tag { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double Altitude { get; set; }
        public string TimeStamp { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }

        public static implicit operator List<object>(FlightTrack v)
        {
            throw new NotImplementedException();
        }
    }
}
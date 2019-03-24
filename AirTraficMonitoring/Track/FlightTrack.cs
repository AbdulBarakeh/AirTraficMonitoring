using System;
using System.Collections.Generic;

namespace AirTraficMonitoring.Track
{
    public class FlightTrack : ITrack
    {
        public FlightTrack(string tag, string xpos, string ypos, string alt, string time)
        {
            Tag = tag;
            XPosition = Convert.ToDouble(xpos);
            YPosition = Convert.ToDouble(ypos);
            Altitude = Convert.ToDouble(alt);
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
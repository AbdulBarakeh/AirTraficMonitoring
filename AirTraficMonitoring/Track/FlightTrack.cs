namespace AirTraficMonitoring.Track
{
    public class FlightTrack : ITrack
    {
        public FlightTrack(string tag, double xpos, double ypos, double alt)
        {
            Tag = tag;
            XPosition = xpos;
            YPosition = ypos;
            Altitude = alt;
            Velocity = 0;
            CompassCourse = 0;
        }

        public string Tag { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double Altitude { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }
    }
}
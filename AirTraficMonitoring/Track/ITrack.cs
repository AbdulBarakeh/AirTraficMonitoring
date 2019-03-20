namespace AirTraficMonitoring.Track
{
    public interface ITrack
    {
        string Tag { get; set; }
        double XPosition { get; set; }
        double YPosition { get; set; }
        double Altitude { get; set; }
        string TimeStamp { get; set; }
        double Velocity { get; set; }
        double CompassCourse { get; set; }
    }
}

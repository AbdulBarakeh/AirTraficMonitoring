using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Test.Unit.Monitor
{
    public static class TrackFactory
    {
        public static ITrack CreateTestTrack()
        {
            return new FlightTrack(
                "ABC123", 
               "0", 
               "0", 
                "0",
               "20190320193050000");
        }
    }
}

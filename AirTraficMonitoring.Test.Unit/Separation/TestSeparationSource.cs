using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Test.Unit.Separation
{
    public static class TestSeparationSource
    {
        public static ITrack CreateTrack()
        {
            return new FlightTrack("HTP909", "0", "0", "0", "1045");
        }
    }
}
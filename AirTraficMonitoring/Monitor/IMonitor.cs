using System.Collections.Generic;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Monitor
{
    public interface IMonitor
    {
        void ShowAllFlightsInAirspace(IList<ITrack> tracks);
        void ShowSeparationCondition(List<ITrack> tracks);
    }
}
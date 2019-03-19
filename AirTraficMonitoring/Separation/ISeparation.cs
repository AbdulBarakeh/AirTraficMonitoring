using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Separation
{
    public interface ISeparation
    {
        void Update(ITrack track);
    }
}
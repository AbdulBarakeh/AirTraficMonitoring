namespace AirTraficMonitoring.Logger
{
    public interface ILog
    {
        void LogInformation(string info);
        void LogWarning(string warning);
        void LogError(string error);
    }
}

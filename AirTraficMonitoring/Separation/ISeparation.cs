using System;

namespace AirTraficMonitoring.Separation
{
    public interface ISeparation
    {
        event EventHandler<SeparationWarningEventArg> SeparationWarningEvent;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirTraficMonitoring.Logger
{
    public interface ILog
    {
        void LogInformation(string info);
        void LogWarning(string warning);
        void LogError(string error);

    }
}

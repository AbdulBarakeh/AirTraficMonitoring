using AirTraficMonitoring.Test.Unit.Decoder;

namespace AirTraficMonitoring.Test.Unit
{
    public class FakeTransponderReceiver : ITransponderReceiver
    {
        private string FakeTransponderReceiver(string tag)
        {
            return tag;
        }
    }
}
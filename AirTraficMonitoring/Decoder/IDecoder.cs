using TransponderReceiver;

namespace AirTraficMonitoring.Decoder
{
    public interface IDecoder
    {
        void DecoderEventHandler(object sender, RawTransponderDataEventArgs e);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTraficMonitoring.Decoder
{
    public interface IDecoder
    {
        void DecoderEventHandler(object sender, RawTransponderDataEventArgs e);
    }
}
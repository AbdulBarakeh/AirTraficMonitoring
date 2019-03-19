using System;
using System.Configuration;
using System.Runtime.Remoting.Channels;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Decoder;
using TransponderReceiver;

namespace AirTraficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog file = new ConsoleLog();

            file.LogInformation("");

            //FlightDecoder.receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            //FlightDecoder.receiver.TransponderDataReady += FlightDecoder.DecoderEventHandler;

        }
    }
}

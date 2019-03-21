using System;
using System.Runtime.Remoting.Channels;
using AirTraficMonitoring.Decoder;
using AirTraficMonitoring.FlightAirspace;
using NSubstitute;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Decoder
{
    [Author("MB")]

    [TestFixture]
    public class DecoderTestUnit
    {
        private FlightDecoder _uut;
        private IDecoder _decoder;
        private IAirspace _airspace;
        private ITransponderReceiver _receiver

        [SetUp]
        public void Setup()
        {
            _uut = new FlightDecoder();

        }
    }
}
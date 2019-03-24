using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.Decoder;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Track;
using NUnit.Framework.Constraints;
using TransponderReceiver;

namespace AirTraficMonitoring.Test.Unit.DecoderTest
{
    [Author("MB")]
    [TestFixture]
    public class Decoder
    {
        private IAirspace _airspace;
        private ITransponderReceiver _receiver;
        private IDecoder _uut; 

        [SetUp]
        public void Setup()
        {
            _airspace = Substitute.For<IAirspace>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _uut = new FlightDecoder(_airspace, _receiver);
        }

        [Test]
        void NewTrackReceived()
        {

        }
    }
}
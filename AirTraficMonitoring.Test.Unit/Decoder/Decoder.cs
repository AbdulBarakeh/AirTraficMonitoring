using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Validator;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Decoder;
using TransponderReceiver;

//namespace AirTraficMonitoring.Test.Unit.Decoder
//{
//    public class Decoder
//    {

//    }
//}

namespace AirTraficMonitoring.Test.Unit.Decoder
{
    [Author("MB")]
    [TestFixture]
    public class Decoder
    {
        private IDecoder _uut;
        private ITransponderReceiver _receiver;
        private IAirspace _airspace;
        private RawTransponderDataEventArgs _receiverEventArg;
        private List<String> myList = new List<String>();

        public object TrackFactory { get; private set; }

        [SetUp]
        public void Setup()
        {
            _airspace = Substitute.For<IAirspace>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _uut = new FlightDecoder(_airspace, _receiver);
        }

        [Test]
        public void FlightAdded_ListIsNotEmpty_()
        {
            string newTrack = "0";
             myList.Add(newTrack);
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(transponderData: myList));
            Assert.That(_receiverEventArg.TransponderData, Is.Empty);
        }
    }
}


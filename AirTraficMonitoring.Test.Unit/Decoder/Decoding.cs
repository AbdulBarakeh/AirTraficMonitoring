using System.Collections.Generic;
using AirTraficMonitoring.Decoder;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTraficMonitoring.Test.Unit.Decoder
{
    [TestFixture]
    public class Decoding
    {
        private FlightDecoder _uut;
        private IAirspace _airspace;
        private ITransponderReceiver _receiver;

        private bool _eventHandled;
        private RawTransponderDataEventArgs _transponderDataEventArgs;

        [SetUp]
        public void Setup()
        {
            _eventHandled = false;

            _airspace = Substitute.For<IAirspace>();
            _receiver = Substitute.For<ITransponderReceiver>();

            _uut = new FlightDecoder(_airspace, _receiver);

            _receiver.TransponderDataReady += (sender, args) => _eventHandled = true;
            _receiver.TransponderDataReady += (sender, args) => _transponderDataEventArgs = args;
        }

        [Test]
        public void HandleTransponderDataReadyEvent_DataReceived_EventIsHandled()
        {
            var data = new List<string>()
            {
                "ATR423;39045;15932;14001;20151006213456789",
                "ATR555;37025;11932;14002;20151006213456789",
                "ATR999;34085;19320;14003;20151006213456789"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(data));

            Assert.That(_eventHandled, Is.True);
        }

        [Test]
        public void HandleTransponderDataReadyEvent_DataReceived_ListOfStringsReceived()
        {
            var data = new List<string>()
            {
                "ATR423;39045;15932;14001;20151006213456789",
                "ATR555;37025;11932;14002;20151006213456789",
                "ATR999;34085;19320;14003;20151006213456789"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(data));

            Assert.That(_transponderDataEventArgs.TransponderData, Is.EqualTo(data));
        }

        [Test]
        public void TransponderDataReady_DataReceived_FlightAddedToAirspace()
        {
            var data = new List<string>()
            {
                "ATR423;39045;15932;14001;20151006213456789",
                "ATR555;37025;11932;14002;20151006213456789",
                "ATR999;34085;19320;14003;20151006213456789"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(data));

            _airspace.Received().Add(Arg.Any<ITrack>());
        }

        
        [TestCase("ÆOO111", "1", "1", "1", "2015")]
        [TestCase("ØOO222", "12", "12", "12", "201510")]
        [TestCase("ÅOO333", "12", "12", "12", "20151006")]
        [TestCase("ATR444", "123", "123", "123", "201510062134")]
        [TestCase("ATR423", "1234", "1234", "1234", "20151006213456")]
        [TestCase("ATR423", "12345", "12345", "12345", "20151006213456789")]
        [TestCase("ATR:423", "-12345", "-12345", "-12345", "20151006213456789")]
        public void DecryptData_DifferentStringData_DecryptionIsCorrect(string tag, string xpos, string ypos, string alt, string time)
        {
            var expectedData = new List<string>(){tag, xpos, ypos, alt, time};

            var data = string.Join(";", expectedData);
            var transponderData = _uut.Decrypt(data);

            Assert.That(transponderData, Is.EqualTo(expectedData));
        }

    }
}
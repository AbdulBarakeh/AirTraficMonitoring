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
        private List<String> _myList = new List<String>();

        [SetUp]
        public void Setup()
        {
            _airspace = Substitute.For<IAirspace>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _uut = new FlightDecoder(_airspace, _receiver);
            _receiverEventArg = new RawTransponderDataEventArgs(_myList);
        }

        [Test]
        public void Separator_ListIsEmpty()
        {
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(transponderData: _myList));
            Assert.That(_receiverEventArg.TransponderData, Is.Empty);
        }

        [Test]
        public void Separator_ListIsNotEmpty()
        {
            string newTrack = "st;10;10;10;10230";
            _myList.Add(newTrack);
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(transponderData: _myList));
            Assert.That(_receiverEventArg.TransponderData, Is.Not.Empty);
        }

        [Test]
        public void Separator_IsEqualTo()
        {
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(transponderData: _myList));
            Assert.That(_myList[0], Is.EqualTo("st"));
            //Assert.That(_myList[1], Is.EqualTo(10));
            //Assert.That(_myList[2], Is.EqualTo(10));
            //Assert.That(_myList[3], Is.EqualTo(10));
            //Assert.That(_myList[4], Is.EqualTo("10230"));
        }

        [Test]
        public void FlightAdded_IsNotEqualTo_LowValue()
        {
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(transponderData: _myList));
            Assert.That(_myList[0], Is.Not.EqualTo("sp"));
            Assert.That(_myList[0], Is.Not.EqualTo(9));
            Assert.That(_myList[0], Is.Not.EqualTo(9));
            Assert.That(_myList[0], Is.Not.EqualTo(9));
            Assert.That(_myList[0], Is.Not.EqualTo("10229"));
        }

        [Test]
        public void FlightAdded_IsNotEqualTo_HighValue()
        {
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(transponderData: _myList));
            Assert.That(_myList[0], Is.Not.EqualTo("at"));
            Assert.That(_myList[0], Is.Not.EqualTo(11));
            Assert.That(_myList[0], Is.Not.EqualTo(11));
            Assert.That(_myList[0], Is.Not.EqualTo(11));
            Assert.That(_myList[0], Is.Not.EqualTo("10231"));
        }
    }
}


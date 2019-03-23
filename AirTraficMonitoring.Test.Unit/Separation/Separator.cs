using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Track;
using NUnit.Framework.Constraints;

namespace AirTraficMonitoring.Test.Unit.Separation
{
    [Author("MB")]
    [TestFixture]
    public class Separator
    {
        private FlightSeparation _uut;

        private FlightAddedEventArg _receivedEventArg;
        private IAirspace _airspace;
        private List<ITrack> myList = new List<ITrack>();

        [SetUp]
        public void Setup()
        {
            _airspace = Substitute.For<IAirspace>();
            _uut = new FlightSeparation(_airspace);

        }

        [Test]
        void testhasfjg()
        {
            var mytrack = new FlightTrack("GE", 0, 0, 0, "1782");
            myList.Add(mytrack);
            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg { Tracks = myList });
            Assert.That(_receivedEventArg.Tracks, Is.Null);
        }
    }
}
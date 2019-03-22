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
using NUnit.Framework.Constraints;

namespace AirTraficMonitoring.Test.Unit.Airspace
{

    [Author("AMBA")]

    [TestFixture]

    public class CalculatorTest
    {
        private FlightAirspace.Airspace _uut;
       // private FlightTrack _testTrack;

        [SetUp]

        public void Setup()
        {
            _uut = new FlightAirspace.Airspace(80000,80000,500,20000);
        }

        [Test]
        public void Adding1Flights_Testing_calculate_SpeedUpdate()
        {
           var _testTrack = new FlightTrack("MSK024", 25684, 68556, 6666, "20190322085020678");
            _uut.calculate(_testTrack);
            Assert.That(_testTrack.Velocity, Is.EqualTo(0));
        }


        [Test]
        public void Adding2Flights_Testing_calculate_SpeedUpdate_DifferentForZero()
        {
            var _testTrack = new FlightTrack("MSK024", 25684, 68556, 6666, "20190322085020678");
            var _testTrack2 = new FlightTrack("MSK024", 50000, 75000, 6666, "20190322105020678");
            _uut.Add(_testTrack);
            _uut.calculate(_testTrack2);
            Assert.That(_testTrack.Velocity, Is.Not.EqualTo(0));
        }

    }
}

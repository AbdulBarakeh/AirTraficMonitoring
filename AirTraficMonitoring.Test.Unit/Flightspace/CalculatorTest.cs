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

        private FlightValidator validator = new FlightValidator();

        //private FlightAddedEventArg eventTotest;


        [SetUp]

        public void Setup()
        {
            //eventTotest = null;
            _uut = new FlightAirspace.Airspace(validator,80000,80000,500,20000);
            //_uut.FlightAddedEvent += (o, args) =>
            //{
            //    eventTotest = args;
            //};
        }

        [Test]
        public void Adding1Flight_Testing_calculate_SpeedUpdate()
        {
            
           var _testTrack1 = new FlightTrack("MSK024", "25684", "68556", "6666", "20190322085020678");
           _uut.Calculate(_testTrack1);
            Assert.That(_testTrack1.Velocity, Is.EqualTo(0));
        }


        [Test]
        public void Adding1Flight_Testing_calculate_CourseUpdate()
        {
            var _testTrack4 = new FlightTrack("MSK024", "25684", "68556", "6666", "20190322085020678");
            _uut.Calculate(_testTrack4);
            Assert.That(_testTrack4.CompassCourse, Is.EqualTo(0));
        }

        [Test]
        public void Adding2Flights_Testing_calculate_SpeedUpdate_DifferentForZero()
        {

            var _testTrack2 = new FlightTrack("MSK024", "25684", "68556", "6666", "20190322085020678");
            var _testTrack3 = new FlightTrack("MSK024", "50000", "75000", "6666", "20190322085220678");

            _uut.Add(_testTrack2);
            _uut.Add(_testTrack3);
           
            Assert.That(_testTrack3.Velocity, Is.Not.EqualTo(0.0));

        }

        [Test]
        public void Adding2Flights_Testing_calculate_CourseUpdate_DifferentForZero()
        {
            var _testTrack5 = new FlightTrack("MSK024", "25684", "68556", "6666", "20190322085020678");
            var _testTrack6 = new FlightTrack("MSK024", "56899", "72520", "6666", "20190322085220678");
            _uut.Add(_testTrack5);
            _uut.Add(_testTrack6);
            Assert.That(_testTrack6.CompassCourse, Is.Not.EqualTo(0.0));
        }

    }
}

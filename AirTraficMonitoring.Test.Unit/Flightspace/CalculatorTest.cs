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

            var _testTrack2 = new FlightTrack("MSK024", "25684", "68556", "6666", "20190322080020678");
            var _testTrack3 = new FlightTrack("MSK024", "50000", "75000", "6666", "20190322085220678");

            _uut.Add(_testTrack2);
            _uut.Add(_testTrack3);
            
           
            Assert.That(_testTrack3.Velocity, Is.EqualTo(Math.Round(8.0626208332560637821,2)));

        }

        [Test]
        public void Adding2Flights_Testing_calculate_CourseUpdate_DifferentForZero()
        {
            var _testTrack5 = new FlightTrack("MSK024", "25684", "68556", "6666", "20190322085020678");
            var _testTrack6 = new FlightTrack("MSK024", "56899", "72520", "6666", "20190322085220678");
            _uut.Add(_testTrack5);
            _uut.Add(_testTrack6);
            Assert.That(_testTrack6.CompassCourse, Is.EqualTo(Math.Round(82.762733899493966,3)));
        }
        //<SpeedTest>
        [Test]

        public void Adding2Flights_TestingWith_XEqualZero_Track1_Speed()
        {
            var _testTrack7 = new FlightTrack("GIG025", "0","53000","6666","20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "10000", "53000", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.Velocity,Is.EqualTo(Math.Round(55.556, 2)));
        }

        [Test]
        public void Adding2Flights_TestingWith_XEqualZero_Track2_Speed()
        {
            var _testTrack7 = new FlightTrack("GIG025", "10000", "53000", "6666", "20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "0", "53000", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.Velocity, Is.EqualTo(Math.Round(55.556, 2)));
        }

        [Test]

        public void Adding2Flights_TestingWith_YEqualZero_Track1_Speed()
        {
            var _testTrack7 = new FlightTrack("GIG025", "26000", "0", "6666", "20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "27000", "53000", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.Velocity, Is.EqualTo(Math.Round(294.497, 2)));
        }

        [Test]
        public void Adding2Flights_TestingWith_YEqualZero_Track2_Speed()
        {
            var _testTrack7 = new FlightTrack("GIG025", "26000", "53000", "6666", "20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "27000", "0", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.Velocity, Is.EqualTo(Math.Round(294.497, 2)));
        }

        [Test]
        public void Adding2Flights_TestingWith_ZEqualZero_Track1_Speed()
        {
            var _testTrack9 = new FlightTrack("MMB759", "78842","32654", "0", "20190411133536356");
            var _testTrack10 = new FlightTrack("MMB759", "78842", "32654", "7562", "20190411133936356");
            _uut.Add(_testTrack9);
            _uut.Add(_testTrack10);
            Assert.That(_testTrack10.Velocity,Is.EqualTo(0));
            //TestTrack 9 is not valid thus is not inserted into list. Which means there is no possibility of calulating speed
        }

        [Test]
        public void Adding2Flights_TestingWith_ZEqualZero_Track2_Speed()
        {
            var _testTrack9 = new FlightTrack("MMB759", "78842", "32654", "7562", "20190411133536356");
            var _testTrack10 = new FlightTrack("MMB759", "78842", "32654", "0", "20190411133936356");
            _uut.Add(_testTrack9);
            _uut.Add(_testTrack10);
            Assert.That(_testTrack10.Velocity, Is.EqualTo(0));
            //TestTrack 10 is not valid thus is not inserted into list. Which means there is no possibility of calulating speed
        }
        //</SpeedTest>






        
        //<CompassCourseTest>
        [Test]

        public void Adding2Flights_TestingWith_XEqualZero_Track1_Compass()
        {
            var _testTrack7 = new FlightTrack("GIG025", "0", "53000", "6666", "20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "10000", "53000", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.CompassCourse, Is.EqualTo(Math.Round(270.000, 2)));
        }

        [Test]
        public void Adding2Flights_TestingWith_XEqualZero_Track2_Compass()
        {
            var _testTrack7 = new FlightTrack("GIG025", "10000", "53000", "6666", "20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "0", "53000", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.CompassCourse, Is.EqualTo(Math.Round(55.556, 2)));
        }

        [Test]

        public void Adding2Flights_TestingWith_YEqualZero_Track1_Compass()
        {
            var _testTrack7 = new FlightTrack("GIG025", "26000", "0", "6666", "20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "27000", "53000", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.CompassCourse, Is.EqualTo(Math.Round(294.497, 2)));
        }

        [Test]
        public void Adding2Flights_TestingWith_YEqualZero_Track2_Compass()
        {
            var _testTrack7 = new FlightTrack("GIG025", "26000", "53000", "6666", "20190411131320654");
            var _testTrack8 = new FlightTrack("GIG025", "27000", "0", "6666", "20190411131620654");
            _uut.Add(_testTrack7);
            _uut.Add(_testTrack8);
            Assert.That(_testTrack8.CompassCourse, Is.EqualTo(Math.Round(294.497, 2)));
        }

        [Test]
        public void Adding2Flights_TestingWith_ZEqualZero_Track1_Compass()
        {
            var _testTrack9 = new FlightTrack("MMB759", "78842", "32654", "0", "20190411133536356");
            var _testTrack10 = new FlightTrack("MMB759", "78842", "32654", "7562", "20190411133936356");
            _uut.Add(_testTrack9);
            _uut.Add(_testTrack10);
            Assert.That(_testTrack10.CompassCourse, Is.EqualTo(0));
            //TestTrack 9 is not valid thus is not inserted into list. Which means there is no possibility of calulating speed
        }

        [Test]
        public void Adding2Flights_TestingWith_ZEqualZero_Track2_Compass()
        {
            var _testTrack9 = new FlightTrack("MMB759", "78842", "32654", "7562", "20190411133536356");
            var _testTrack10 = new FlightTrack("MMB759", "78842", "32654", "0", "20190411133936356");
            _uut.Add(_testTrack9);
            _uut.Add(_testTrack10);
            Assert.That(_testTrack10.CompassCourse, Is.EqualTo(0));
            //TestTrack 10 is not valid thus is not inserted into list. Which means there is no possibility of calulating speed
        }
        //</CompassCourseTest>


    }
}

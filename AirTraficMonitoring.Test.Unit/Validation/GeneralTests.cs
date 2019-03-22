using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Validator;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace AirTraficMonitoring.Test.Unit.Validation
{
    [Author("GIG")]

    [TestFixture]

    public class GeneralTests
    {
        
        private Validator.FlightValidator _vUUT;
        private Track.FlightTrack _testTrack;
        private FlightAirspace.Airspace _testAirspace;
        
        [SetUp]

        public void Setup()
        {
            _testAirspace = new FlightAirspace.Airspace(80000, 80000, 500, 20000);
            _vUUT = new Validator.FlightValidator();
        }

        [Test]

        public void TestingValidateCorrectData()
        {
            var testTrack1 = new Track.FlightTrack("au555", 78000, 55000, 15000, "20151006213456785");
            bool validationValue = _vUUT.Validate(_testAirspace, testTrack1);
            Assert.That(validationValue, Is.True);
        }
            
        [Test]

        public void TestingValidateWrongData()
        {
            var testTrack2 = new FlightTrack("auC556", 81000, 81000, 21000, "20151006213456783");
            bool validationValue = _vUUT.Validate(_testAirspace, testTrack2);
            Assert.That(validationValue, Is.False);
        }

        [Test]

        public void TestingNegativeXPosData()
        {
            var testTrack3 = new FlightTrack("down555", -500, 30000, 5000, "20151006213456783");
            bool validationValue = _vUUT.Validate(_testAirspace, testTrack3);
            Assert.That(validationValue, Is.False);
        }

        [Test]

        public void TestingNegativeYPosData()
        {
            var testTrack3 = new FlightTrack("down555", 25000, -12, 5000, "20151006213456783");
            bool validationValue = _vUUT.Validate(_testAirspace, testTrack3);
            Assert.That(validationValue, Is.False);
        }
    }
}

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

namespace AirTraficMonitoring.Test.Unit.Airspace
{
    [Author("AMBA")]

    [TestFixture]

    public class GeneralTest
    {
        private FlightAirspace.Airspace _asUUT;

        private FlightAddedEventArg _receivedEventArg;

        private FlightTrack _track;

        [SetUp]

        public void Setup()
        {
            _receivedEventArg = null;
            _asUUT = new FlightAirspace.Airspace(80000, 80000, 500, 20000);
            _track = new FlightTrack("ATR423", 39045, 12932, 14000, "20151006213456789");

            _asUUT.Add(_track);

            //Subscribes aka listening to event.
            _asUUT.FlightAddedEvent += (x, args) => { _receivedEventArg = args; };
            
        }

        [Test]
        public void Checking_If_Listofflights_Is_Empty()
        {
            var _testTrackOne = new FlightTrack("ATQ123", 52222, 45623, 600, "20190321155458675");
            _asUUT.Add(_testTrackOne);
            Assert.That(_receivedEventArg, Is.Not.Null);
        }
        //[Test]

        //public void AirspaceDimension_VariabelsSetCorrect()
        //{
        //    Assert.That(_asUUT.Height,Is.EqualTo(80000));
        //    Assert.That(_asUUT.Width, Is.EqualTo(80000));
        //    Assert.That(_asUUT.MinAlt, Is.EqualTo(500));
        //    Assert.That(_asUUT.MaxAlt, Is.EqualTo(20000));
        //}

        //[Test]

        //public void AirspaceDimension_VariabelsSetIncorrect()
        //{
        //    Assert.That(_asUUT.Height, Is.Not.EqualTo(2000));
        //    Assert.That(_asUUT.Width, Is.Not.EqualTo(500));
        //    Assert.That(_asUUT.MinAlt, Is.Not.EqualTo(50));
        //    Assert.That(_asUUT.MaxAlt, Is.Not.EqualTo(15000));
        //}

        //[Test]

        //public void calculation_And_AddingFlight_To_List_Correct()
        //{
            
        //    FlightTrack testTrack = new FlightTrack("ATR423", 39045, 12932,14000, "20151006213456789");
            
        //    _asUUT.Add(testTrack);

        //    Assert.That(_asUUT.ListOfFlights.Count, Is.EqualTo(1));
        //}

        //[Test]

        //public void calculation_And_AddingFlight_To_List_InCorrect_TrackParameters()
        //{

        //    FlightTrack testTrack = new FlightTrack("ATR423", 800000, 12932, 14000, "20151006213456789");

        //    _asUUT.Add(testTrack);

        //    Assert.That(_asUUT.ListOfFlights.Count, Is.EqualTo(0));
        //}

        //[Test]

        //public void Adding_SameFlight_Twice_Correct_Answer_CountEqualsOne()
        //{
        //    _asUUT.ListOfFlights.RemoveRange(0,_asUUT.ListOfFlights.Count);
        //    FlightTrack testTrackOne = new FlightTrack("ATR423", 70000, 12000, 14000, "20151006213456789");
        //    FlightTrack testTrackTwo = new FlightTrack("ATR423", 20999, 50000, 14000, "20151006213456789");

        //    _asUUT.Add(testTrackOne);
        //    _asUUT.Add(testTrackTwo);

        //    FlightTrack testTrackThree = new FlightTrack("ATR423", 2099, 5000, 4000, "20151006213456789");

        //    _asUUT.Add(testTrackThree);

        //    Assert.That(_asUUT.ListOfFlights.Count, Is.EqualTo(1));
        //}

        //[Test]

        //public void Adding_Two_DifferentFlights_Correct_Answer_CountEqualsTwo()
        //{
        //    //_asUUT.ListOfFlights.RemoveRange(0, _asUUT.ListOfFlights.Count);
        //    FlightTrack testTrackOne = new FlightTrack("ATR423", 70000, 12000, 14000, "20151006213456789");
        //    FlightTrack testTrackTwo = new FlightTrack("ATR423", 20999, 50000, 14000, "20151006213456789");

        //    _asUUT.Add(testTrackOne);
        //    _asUUT.Add(testTrackTwo);

        //    FlightTrack testTrackThree = new FlightTrack("BTR423", 2099, 5000, 4000, "20151006213456789");

        //    _asUUT.Add(testTrackThree);

        //    Assert.That(_asUUT.ListOfFlights.Count, Is.EqualTo(2));
        //}

        //[Test]

        //public void Adding_Unvalid_Track_Expecting_ErrorMsg()
        //{
        //    FlightTrack testTrackFour = new FlightTrack("ABR588", 80001, 5000, 4000, "20190320182830500");
        //    _asUUT.Add(testTrackFour);
        //    Assert.That(_asUUT.ListOfFlights.Count, Is.EqualTo(0));
        //}
    }
}

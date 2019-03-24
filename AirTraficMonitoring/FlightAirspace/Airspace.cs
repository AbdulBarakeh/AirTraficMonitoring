using System;
using System.Collections.Generic;
using System.Linq;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Validator;
using System.Globalization;

namespace AirTraficMonitoring.FlightAirspace
{
    public class Airspace  : IAirspace
    {
        public List<ITrack> ListOfFlights;
        public event EventHandler<FlightAddedEventArg> FlightAddedEvent;
        private const string DateFormat = "yyyyMMddHHmmssfff";
        private readonly CultureInfo _cultureInfo;
        private readonly IValidator _flightValidator;

        public Airspace(IValidator validator, double width, double height, double minAlt, double maxAlt)
        {
            ListOfFlights = new List<ITrack>();
            _cultureInfo = new CultureInfo("dk-DK");
            _flightValidator = validator;

            Width = width;
            Height = height;
            MinAlt = minAlt;
            MaxAlt = maxAlt;
        }

        protected virtual void OnFlightAddedEvent(FlightAddedEventArg e)
        {
            FlightAddedEvent?.Invoke(this, e);
        }

        public void Add(ITrack track)
        {
            if (_flightValidator.Validate(this, track))
            {
                ListOfFlights.Add(Calculate(track));
                OnFlightAddedEvent(new FlightAddedEventArg {Tracks = ListOfFlights});
            }
            else
            {
                if (ListOfFlights.FirstOrDefault(t => t.Tag == track.Tag) == null)
                    return;

                ListOfFlights.RemoveAll(t => t.Tag == track.Tag);
            }
        }
        
        public ITrack Calculate(ITrack track)
        {
            var flightTrack = ListOfFlights.FirstOrDefault(t => t.Tag == track.Tag);

            if (flightTrack == null) return track;

            var delta = new
            {
                X = track.XPosition - flightTrack.XPosition,
                Y = track.YPosition - flightTrack.YPosition
            };

            var timestamp = new
            {
                Old = DateTime.ParseExact(flightTrack.TimeStamp, DateFormat, _cultureInfo),
                New = DateTime.ParseExact(flightTrack.TimeStamp, DateFormat, _cultureInfo)
            };

            var time = (timestamp.New - timestamp.Old).TotalSeconds;     
            var distance = Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2));

            var flight = new
            {
                speed = distance / time,
                course = (Math.Atan2(delta.Y, delta.X) * (180 / Math.PI)) - 90
            };

            track.Velocity = flight.speed;
            track.CompassCourse = flight.course;

            ListOfFlights.RemoveAll(t => t.Tag == track.Tag);

            return track;
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public double MinAlt { get; set; }
        public double MaxAlt { get; set; }
    }
}


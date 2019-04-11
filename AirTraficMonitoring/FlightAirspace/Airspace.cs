using System;
using System.Collections.Generic;
using System.Linq;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Validator;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AirTraficMonitoring.FlightAirspace
{
    public delegate void addLimitEventHandler(object source, FlightAddedEventArg e);

    public class Airspace  : IAirspace
    {
        public List<ITrack> ListOfFlights { get; set; }
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
            //FlightAddedEventArg flightAddedEvent = new FlightAddedEventArg();
            if (_flightValidator.Validate(this, track))
            {
                ListOfFlights.Add(Calculate(track));
                OnFlightAddedEvent(new FlightAddedEventArg { Tracks = ListOfFlights });
                //flightAddedEvent.Tracks.Add(Calculate(track));
                //OnFlightAddedEvent( flightAddedEvent.Tracks);

            }
            else
            {
                switch (ListOfFlights.FirstOrDefault(t => t.Tag == track.Tag))
                {
                    case null:
                        return;
                    default:
                        ListOfFlights.RemoveAll(t => t.Tag == track.Tag);
                        break;
                }
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
                New = DateTime.ParseExact(track.TimeStamp, DateFormat, _cultureInfo)
            };

            var time = (timestamp.New - timestamp.Old).TotalSeconds;   
            var distance = Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2));

            double InitialDegree = ((Math.Atan2(delta.Y, delta.X) * (180 / Math.PI)) - 90);

            if (InitialDegree < 0)
            {
                InitialDegree += 360;
            }


            var flight = new
            {
                speed = distance / time,
                
                course = InitialDegree
                
            };

            track.Velocity = Math.Round(flight.speed,2);
            track.CompassCourse = Math.Round(flight.course,3);

            ListOfFlights.RemoveAll(t => t.Tag == track.Tag);

            return track;
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public double MinAlt { get; set; }
        public double MaxAlt { get; set; }
    }
}


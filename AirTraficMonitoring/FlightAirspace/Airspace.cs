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
        public List<ITrack> ListOfFlights = new List<ITrack>();

        public event EventHandler<FlightAddedEventArg> FlightAddedEvent;
        public double Width { get; set; }
        public double Height { get; set; }
        public double MinAlt { get; set; }
        public double MaxAlt { get; set; }

        public Airspace(double width, double height, double minAlt, double maxAlt)
        {
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
            #region TO DO
            // Tilføj flightvalidator til constructoren (dependency injection)
            // Der laves en flightvalidator objekt hver gang der kommer en nyt fly. Dette er ikke nødvendigt.
            #endregion
            var obj = new FlightValidator();
            if (obj.Validate(this, track))
            {
                 ListOfFlights.Add(Calculate(track));
                 OnFlightAddedEvent(new FlightAddedEventArg {Tracks = ListOfFlights});
            }
            else
            {
                #region TO DO
                // Hvad vil der ske hvis et fly ikke allerede er i listen men det ikke bliver valideret som true?
                // Jeg tror du vil prøve at fjerne noget som ikke er i listen? Hvad sker der så?
                // Der skal kigges i listen om der har været et fly som nu er uden for airspace - ellers hvis det ikke findes tidligere i listen så skal der ikke gøres noget
                #endregion
                ListOfFlights.RemoveAll(t => t.Tag == track.Tag);
                Console.WriteLine("No valid track inserted");
            }
        }

        #region TO DO
        // Hvad gør denne culture info? behøver den at blive oprettet her i klassen?
        // Kan den blive injected istedet? eller lavet som static? (dependency injection)
        // Lad den blive initialiseret i constructoren
        #endregion
        private readonly CultureInfo _cultureInfo = new CultureInfo("dk-DK");

        public ITrack Calculate(ITrack track)
        {
            #region TO DO
            // Der bør kun kigges efter et fly da der ikke kan være flere fly med samme tag?
            // Brug eventuelt FirstOrDefault til at finde et element
            #endregion
            var flightTracks = ListOfFlights.FindAll(flight => flight.Tag == track.Tag);


            #region TO DO
            // Har du nogensinde set en liste med under 0 elementer i? Det bør ikke være nødvendigt at tjekke.
            #endregion
            if (flightTracks.Count < 0)
                throw new ArgumentException("FlightList can not contain less than 0", nameof(flightTracks));
            
            if (flightTracks.Count == 0)
                return track;

            #region TO DO
            // Der er rigtige mange kommentar som forvirrer helt vildt. Brug gerne mere beskrivende navne og funktioner for at gøre det mere læseligt.
            // Kommentar på denne måde har ikke den rigtige effekt for læseren.
            #endregion
            //Extract Track from list
            var specificTrack = flightTracks.First();
            //Find old corrdinates
            var oldXPosition = specificTrack.XPosition;
            var oldYPosition = specificTrack.YPosition;
            //Find distance of travel with the use of new and old position
            var deltaXPosition = track.XPosition - oldXPosition;
            var deltaYPosition = track.YPosition - oldYPosition;
            //Calculate distance
            var distance = Math.Sqrt(Math.Pow(deltaXPosition, 2) + Math.Pow(deltaYPosition, 2));
            //Take Timestamp as string and convert to datetime
            //var format = "yyyy/MM/dd HH:mm:ss.fff";
            var format = "yyyyMMddHHmmssfff";
            DateTime timeStampOld = DateTime.ParseExact(specificTrack.TimeStamp,format, _cultureInfo);
            DateTime timeStampNew = DateTime.ParseExact(track.TimeStamp,format, _cultureInfo);
            //Find difference in seconds
            var timediff = (timeStampNew - timeStampOld).TotalSeconds;
            //Calculate average speed between last and current location(Velocity)
            var speed = (distance / timediff);

            //Calculate CompasCourse
            //var CompCourse = Math.Asin(deltaYPosition / distance) - 90;//Take away 90 because north is in 0 degrees

            var compassCourse = (Math.Atan2(deltaYPosition, deltaXPosition) * (180 / Math.PI)) - 90;// 180/3.14 radToDeg
            
            specificTrack.Tag = track.Tag;
            specificTrack.XPosition = track.XPosition;
            specificTrack.YPosition = track.YPosition;
            specificTrack.Altitude = track.Altitude;
            specificTrack.TimeStamp = track.TimeStamp;
            specificTrack.Velocity = speed;
            specificTrack.CompassCourse = compassCourse;

            ListOfFlights.RemoveAll(t => t.Tag == track.Tag);

            return specificTrack;
        }        
    }
}


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Decoder;
using AirTraficMonitoring.FlightValidation;


namespace AirTraficMonitoring.FlightAirspace
{
    public class Airspace  : IAirspace
    {
        public List<ITrack> ListOfFlights = new List<ITrack>();
        List<ISeparation> obsList = new List<ISeparation>();

        public Airspace(double width, double height, double minAlt, double maxAlt)
        {

            Width = width;
            Height = height;
            MinAlt = minAlt;
            MaxAlt = maxAlt;
        }

        public void attachment(ISeparation separation)
        {
            obsList.Add(separation);
        }

        public void Add(ITrack track)
        {
            FlightValidationn obj = new FlightValidationn();
            if (obj.Validation(track,this))
            {
                //Iterate through list of flights
                Notify(track);

                var results = ListOfFlights.FindAll(a => a.Tag == track.Tag);

                if (results.Count < 0)
                {
                    throw new System.ArgumentException("Parameter can't be less than 0", "results");
                }
                else if (results.Count == 0)
                {
                    ListOfFlights.Add(track);
                }
                else
                {
                    //Extract Track from list
                    var specificTrack = results.ElementAt(1);
                    //Find old corrdinates
                    var oldXPosition = specificTrack.XPosition;
                    var oldYPosition = specificTrack.YPosition;
                    //Find distance of travel with the use of new and old position
                    var deltaXPosition = track.XPosition - oldXPosition;
                    var deltaYPosition = track.YPosition - oldYPosition;
                    //Calculate distance
                    var distance = Math.Sqrt(Math.Pow(deltaXPosition, 2) + Math.Pow(deltaYPosition, 2));
                    //Take Timestamp as string and convert to datetime
                    DateTime timeStampOld = DateTime.Parse(specificTrack.TimeStamp);
                    DateTime timeStampNew = DateTime.Parse(track.TimeStamp);
                    //Find difference in seconds
                    var timediff = (timeStampNew - timeStampOld).TotalSeconds;
                    //Calculate average speed between last and current location(Velocity)
                    var speed = (distance / timediff);

                    //Calculate CompasCourse
                    //var CompCourse = Math.Asin(deltaYPosition / distance) - 90;//Take away 90 because north is in 0 degrees

                    var CompCourse = (Math.Atan2(deltaYPosition, deltaXPosition) * (180 / Math.PI)) - 90;// 180/3.14 radToDeg

                    //Update values
                    specificTrack.Tag = track.Tag;
                    specificTrack.XPosition = track.XPosition;
                    specificTrack.YPosition = track.YPosition;
                    specificTrack.Altitude = track.Altitude;
                    specificTrack.TimeStamp = track.TimeStamp; //Inserted as string again instead of DateTime type
                    specificTrack.Velocity = speed;
                    specificTrack.CompassCourse = CompCourse;

                    //Insert back into list
                    ListOfFlights.RemoveAll(t => t.Tag == track.Tag); //remove current
                    ListOfFlights.Add(specificTrack);//Add updated version

                }

            }


            ListOfFlights.ForEach(flight =>
            {
                Console.WriteLine($"{flight.Tag}\n");
            });
        }

        void Notify(ITrack track)
        {
            foreach (var obs in obsList)
            {
                obs.Update(track);
            }
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public double MinAlt { get; set; }
        public double MaxAlt { get; set; }
        
    }
}


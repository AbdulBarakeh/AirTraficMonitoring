using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightAirspace
{
    class Airspace : ITrack 
    {
        List<FlightTrack> ListOfFlights = new List<FlightTrack>();

        Airspace(FlightTrack flight)
        {
            //Iterate through list of flights
            

            var results = ListOfFlights.FindAll(a => a.Tag == flight.Tag);

            if (results.Count < 0)
            {
                throw new System.ArgumentException("Parameter can't be less than 0", "results");
            }
            else if (results.Count == 0)
            {
                ListOfFlights.Add(flight);
            }
            else
            {
                //Extract Track from list
                var specificTrack = results.ElementAt(1);
                //Find old corrdinates
                var oldXPosition = specificTrack.XPosition;
                var oldYPosition = specificTrack.YPosition;
                //Find distance of travel with the use of new and old position
                var deltaXPosition = flight.XPosition - oldXPosition;
                var deltaYPosition = flight.YPosition - oldYPosition;
                //Calculate distance
                var distance = Math.Sqrt(Math.Pow(deltaXPosition,2) + Math.Pow(deltaYPosition,2));
                //Take Timestamp as string and convert to datetime
                DateTime timeStampOld = DateTime.Parse(specificTrack.TimeStamp);
                DateTime timeStampNew = DateTime.Parse(flight.TimeStamp);
                //Find difference in seconds
                var timediff = (timeStampNew - timeStampOld).TotalSeconds;
                //Calculate average speed between last and current location(Velocity)
                var speed = (distance / timediff);

                //Calculate CompasCourse
                //var CompCourse = Math.Asin(deltaYPosition / distance) - 90;//Take away 90 because north is in 0 degrees

                var CompCourse = (Math.Atan2(deltaYPosition, deltaXPosition) * (180 / Math.PI)) - 90;// 180/3.14 radToDeg

                //Update values
                specificTrack.Tag = flight.Tag;
                specificTrack.XPosition = flight.XPosition;
                specificTrack.YPosition = flight.YPosition;
                specificTrack.Altitude = flight.Altitude;
                specificTrack.TimeStamp = flight.TimeStamp; //Inserted as string again instead of DateTime type
                specificTrack.Velocity = speed;
                specificTrack.CompassCourse = CompCourse;

                //Insert back into list
                ListOfFlights.RemoveAll(t => t.Tag == flight.Tag); //remove current
                ListOfFlights.Add(specificTrack);//Add updated version

                //var distance = Math.Sqrt(distanceSquared);
               //find Xpos
               //find Ypos
               //find nyXpos
               //find nyYpos
               //find hypotonusen
               //find difference mellem timestamps
                //Udregn fart 
                // Opdaterer koordinater
                //opdaterer tid
                //opdatere resterende 
            }

        }

        public string Tag { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double Altitude { get; set; }
        public string TimeStamp { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }
    }
}

using System;

namespace FlightSearchAPI.Models
{
    public class Flight
    {
        public string Origin { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public string DestinationTime { get; set; }
        public string Price { get; set; }
    }
}

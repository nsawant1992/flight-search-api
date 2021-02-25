using CsvHelper.Configuration;
using FlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearchAPI.Mapper
{
    public sealed class FlightMap : ClassMap<Flight>
    {
        public FlightMap()
        {
            Map(x => x.Origin).Name("Origin");
            Map(x => x.DepartureTime).Name("Departure Time");
            Map(x => x.Destination).Name("Destination");
            Map(x => x.DestinationTime).Name("Destination Time");
            Map(x => x.Price).Name("Price");
        }
    }
}

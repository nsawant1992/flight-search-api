using FlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearchAPI.Interface
{
    public interface ISearchService
    {
        List<Flight> GetFlights(SearchRequest searchRequest);
    }
}

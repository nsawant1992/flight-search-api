using CsvHelper;
using FlightSearchAPI.Interface;
using FlightSearchAPI.Mapper;
using FlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchAPI.Service
{
    public class SearchService : ISearchService
    {
        public List<Flight> GetFlights(SearchRequest searchRequest)
        {
            var flights = new List<Flight>();
            var filteredFlights = new List<Flight>();
            try
            {
                flights = ReadFlightDataFromCSV();
                // First find the Direct flight
                filteredFlights = flights.Where(x => x.Origin == searchRequest.OriginCode
                                                && x.Destination == searchRequest.DestinationCode
                                                && x.DepartureTime >= searchRequest.DepartureDate).ToList();

                // Then find connecting flights and add to filtered flights

                var flightsToDestination = flights.Where(x => x.Destination == searchRequest.DestinationCode && x.DepartureTime >= searchRequest.DepartureDate).ToList();
                var flightToSource = (from flight in flights
                                      join flToDest in flightsToDestination on flight.Destination equals flToDest.Origin
                                      where flight.DepartureTime >= searchRequest.DepartureDate
                                      select flight).ToList();

                filteredFlights.AddRange(flightToSource);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            // Sorting flights based on Sort By Parameter.
            if(searchRequest.SortBy == "Price")
            {
                filteredFlights.OrderBy(x => x.Price);
            } else if(searchRequest.SortBy == "Departure Date")
            {
                filteredFlights.OrderBy(x => x.DepartureTime);
            }
            return filteredFlights;
        }


        #region Private methods
        private List<Flight> ReadFlightDataFromCSV()
        {
            List<Flight> flights = new List<Flight>();
            string[] paths = {
                @"F:\FlightSearch\Data\Provider1.csv",
                @"F:\FlightSearch\Data\Provider2.csv",
                @"F:\FlightSearch\Data\Provider3.csv"
            };


            try
            {
                // Adding data to flights list fromm all the three providers
                for (int i = 0; i < paths.Length; i++)
                {
                    using (var reader = new StreamReader(paths[i]))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<FlightMap>();
                        flights.AddRange(csv.GetRecords<Flight>().ToList());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return flights;
        }
        #endregion Private Methods
    }
}

using FlightSearchAPI.Interface;
using FlightSearchAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ISearchService _searchService { get; set; }

        public SearchController(ISearchService searchService)
        {
            _searchService  = searchService;
        }

        [HttpPost]
        public IActionResult Get([FromBody]SearchRequest searchRequest)
        {
            List<Flight> flights = new List<Flight>();
            try
            {
                if (!ModelState.IsValid) 
                {
                    return BadRequest(ModelState);
                }

                flights = _searchService.GetFlights(searchRequest);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return Ok(flights);
        }
    }
}

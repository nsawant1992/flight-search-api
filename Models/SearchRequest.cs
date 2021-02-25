using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearchAPI.Models
{
    public class SearchRequest
    {
        [Required]
        [StringLength(3)]
        public string OriginCode { get; set; }

        [Required]
        [StringLength(3)]
        public string DestinationCode { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }
        public string SortBy { get; set; }
    }
}

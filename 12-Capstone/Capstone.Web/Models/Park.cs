using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Park
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public string Acreage { get; set; }
        public string Elevation { get; set; }
        public int Miles { get; set; }
        public string Campsites { get; set; }
        public string Climate { get; set; }
        public int Founded { get; set; }
        public string Visitors { get; set; }
        public string Quote { get; set; }
        public string QuoteSource { get; set; }
        public string Description { get; set; }
        public int Fee { get; set; }
        public int Animals { get; set; }

    }
}

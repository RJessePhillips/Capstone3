using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkDetailVM
    {
        public Park Park { get; set; }
        public List<Weather> Weather { get; set; }
        public string Temperature { get; set; }


    }
}

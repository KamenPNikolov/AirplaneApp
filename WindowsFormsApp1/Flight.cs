using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Flight
    {
        public string FlightID { get; set; }
        public string planeType{ get; set; }
        public DateTime DateTakeoff{ get; set; }
        public DateTime DateLanding{ get; set; }
        public string takeOffDestination{ get; set; }
        public string landingDestination{ get; set; }
        public string stopOver{ get; set; }

    }
}

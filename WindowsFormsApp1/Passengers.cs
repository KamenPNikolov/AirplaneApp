using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Passengers
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string way{ get; set; }
        public DateTime dateOfFlight { get; set; }
        public string row { get; set; }
        public string seatAl { get; set; }
        public Flight flightOfPassanger;
    }
}

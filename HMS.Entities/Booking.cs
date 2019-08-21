using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class Booking
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        /// <summary>
        /// No of stays nights
        /// </summary>
        public int Duration { get; set; }

        public int AccomadationID { get; set; }
        public virtual Accomadation Accomadation { get; set; } // Accomadation object
    }
}

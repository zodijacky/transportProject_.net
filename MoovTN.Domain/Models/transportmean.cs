using System;
using System.Collections.Generic;

namespace MoovTn.Domain.Models
{
    public partial class TransportMean
    {
        public TransportMean()
        {
            this.rents = new List<Rent>();
            this.trips = new List<Trip>();
            this.rents1 = new List<Rent>();
        }

        public string serial { get; set; }
        public string state { get; set; }
        public string type { get; set; }
        public virtual ICollection<Rent> rents { get; set; }
        public virtual ICollection<Trip> trips { get; set; }
        public virtual ICollection<Rent> rents1 { get; set; }
    }
}

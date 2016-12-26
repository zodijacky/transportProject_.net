using System;
using System.Collections.Generic;
namespace MoovTn.Domain.Models
{
    public partial class Rent
    {
        public Rent()
        {
            this.transportmeans = new List<TransportMean>();
        }

        public int id { get; set; }
        public int members { get; set; }
        public Nullable<System.DateTime> rentEnd { get; set; }
        public Nullable<System.DateTime> rentStart { get; set; }
        public string status { get; set; }
        public Nullable<int> idDepart { get; set; }
        public Nullable<int> idDestination { get; set; }
        public string transportMean_serial { get; set; }
        public string user_id { get; set; }
        public virtual Location location { get; set; }
        public virtual Location location1 { get; set; }
        public virtual TransportMean transportmean { get; set; }
        public virtual User user { get; set; }
        public virtual ICollection<TransportMean> transportmeans { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MoovTn.Domain.Models
{
    public partial class Location
    {
        public Location()
        {
            this.rents = new List<Rent>();
            this.rents1 = new List<Rent>();
        }

        public int id { get; set; }
        public string adress { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public virtual ICollection<Rent> rents { get; set; }
        public virtual ICollection<Rent> rents1 { get; set; }
    }
}

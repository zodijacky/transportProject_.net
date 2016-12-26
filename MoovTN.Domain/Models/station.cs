using System;
using System.Collections.Generic;

namespace MoovTn.Domain.Models
{
    public partial class Station
    {
        public Station()
        {
            this.linestations = new List<Linestation>();
        }

        public int id { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public virtual ICollection<Linestation> linestations { get; set; }
    }
}

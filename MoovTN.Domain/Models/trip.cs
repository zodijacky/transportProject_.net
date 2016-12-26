using System;
using System.Collections.Generic;
namespace MoovTn.Domain.Models
{
    public partial class Trip
    {
        public Trip()
        {
            this.lines = new List<Line>();
        }

        public int id { get; set; }
        public Nullable<System.DateTime> arrivalTime { get; set; }
        public Nullable<bool> canceled { get; set; }
        public Nullable<System.DateTime> departureTime { get; set; }
        public Nullable<int> line_id { get; set; }
        public string transportMean_serial { get; set; }
        public virtual Line line { get; set; }
        public virtual TransportMean transportmean { get; set; }
        public virtual ICollection<Line> lines { get; set; }
    }
}

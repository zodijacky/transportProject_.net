using System;
using System.Collections.Generic;

namespace MoovTn.Domain.Models
{
    public partial class SubscriptionCard
    {
        public SubscriptionCard()
        {
            this.lines = new List<Line>();
        }

        public int id { get; set; }
        public bool expired { get; set; }
        public bool locked { get; set; }
        public Nullable<System.DateTime> validityEnd { get; set; }
        public Nullable<System.DateTime> validityStart { get; set; }
        public string user_id { get; set; }
        public virtual User user { get; set; }
        public virtual ICollection<Line> lines { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MoovTn.Domain.Models
{
    public partial class Claim
    {
        public int id { get; set; }
        public Nullable<System.DateTime> claimDate { get; set; }
        public string question { get; set; }
        public Nullable<bool> questionRead { get; set; }
        public string response { get; set; }
        public Nullable<bool> responseRead { get; set; }
        public string users_id { get; set; }
        public virtual User user { get; set; }
    }
}

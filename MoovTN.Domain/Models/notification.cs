using System;
using System.Collections.Generic;

namespace MoovTn.Domain.Models
{
    public partial class Notification
    {
        public int id { get; set; }
        public bool broadcast { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public Nullable<int> idLine { get; set; }
        public virtual Line line { get; set; }
    }
}

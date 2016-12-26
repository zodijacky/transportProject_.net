using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoovTn.Domain.Models
{
    public partial class Linestation
    {
    

        public int line_fk { get; set; }
    
        public int station_fk { get; set; }
        public int ordre { get; set; }
        public virtual Line line { get; set; }
        public virtual Station station { get; set; }
    }
}


using MoovTn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoovTn.Web.Models
{
    public class GeneratePDFModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime DateCreation { get; set; }
        public DateTime Expire { get; set; }

        public String Job { get; set; }

        public ICollection<Line> Lines { get; set; }
        public String img { get; set; }
    }
}
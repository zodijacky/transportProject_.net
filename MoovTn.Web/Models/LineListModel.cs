using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoovTn.Web.Models
{
    public class LineListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }

        public string Stations { get; set; }
    }
}
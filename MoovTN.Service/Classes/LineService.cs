using MoovTn.Data.Infrastructure;
using MoovTn.Domain.Models;
using MoovTn.Service.Interfaces;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Service.Classes
{
    public class LineService : Service<Line>, ILineService
    {
        public IEnumerable<Line> GetLinesByName(string type, string name)
        {
            return GetMany(l => l.type == type && l.name == name).OrderBy(l => l.name);
        }

        public IEnumerable<Line> GetLinesByType(string type)
        {
            return GetMany(l=>l.type==type).OrderBy(l=>l.name);
        }

        public string LinesListToJson(List<Line> liste)
        {
            
            String json = "{\"Lines\":[";
            int i = 0;
            foreach (Line l in liste)
            {
                if (i != 0)
                {
                    json += ",";
                }
                i++;
                json += "{\"id\": " + l.id + ",\"name\": " + l.name + "}";
            }
            json += "]}";
            return json;
        }

        public string LinesPathsToJson(List<Line> liste)
        {
            String json = "{\"points\":[";
            int i = 0;
            foreach (Line l in liste)
            {
                if (i != 0)
                {
                    json += ",";
                }
                i++;
                json += l.path;
            }

            json += "]}";

            return json;
        }

        public IEnumerable<Line> TestStationInLine(int ids1)
        {
            return GetMany(l => l.linestations.Any(ls => ls.station_fk == ids1));
        }
    }
}

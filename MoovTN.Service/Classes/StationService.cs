using MoovTn.Data.Infrastructure;
using MoovTn.Domain.Models;
using MoovTn.Service.Interfaces;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Service.Classes
{
    public class StationService : Service<Station>, IStationService
    {
        public IEnumerable<Station> GetStationsByType(string type)
        {
            if(type=="bus" || type=="tram" || type=="metro" || type == "train")
            {
                return GetMany(s => s.type == type || s.type == "transfert");
            }
            return new List<Station>();
            
        }

       
        public string StationsListToJson(List<Station> liste)
        {
            String json = "{\"stations\":[";
            int i = 0;
            foreach (Station s in liste)
            {
                if (i != 0)
                {
                    json += ",";
                }
                i++;
                json += "{\"name\": \"" + s.name + "\",\"lat\": " + s.latitude + ",\"lng\": " + s.longitude + ",\"type\": \"" + s.type + "\"}";
            }
            json += "]}";
            return json;
        }

        public string findAllToJson()
        {
            List<Station> liste = GetMany().ToList();
            String json = "{\"stations\":[";
            int i = 0;
            foreach (Station s in liste)
            {
                if (i != 0)
                {
                    json += ",";
                }
                i++;
                json += "{\"name\": \"" + s.name + "\",\"lat\": " + s.latitude + ",\"lng\": " + s.longitude + ",\"type\": \"" + s.type + "\"}";
            }
            json += "]}";
            return json;

        }
  

        public string findOneToJson(int id)
        {
            Station s = GetById(id);
            String json = "{\"stations\":[";
            int i = 0;

            if (i != 0)
            {
                json += ",";
            }
            i++;
            json += "{\"name\": \"" + s.name + "\",\"lat\": " + s.latitude + ",\"lng\": " + s.longitude + ",\"type\": \"" + s.type + "\"}";

            json += "]}";
            return json;

        }

        public IEnumerable<Station> GetManyByLine(int lineId)
        {
            return GetMany(s=>s.linestations.Any(ls=>ls.line_fk == lineId));
        }
    }
}

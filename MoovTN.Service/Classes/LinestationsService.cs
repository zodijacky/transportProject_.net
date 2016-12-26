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
 public class LinestationsService : Service<Linestation>, ILinestationsService
    {
       public  int GetStationOrder(int lineId, int stationId)
        {
            return Get(ls=>(ls.line_fk==lineId && ls.station_fk == stationId)).ordre;
        }
    }
}

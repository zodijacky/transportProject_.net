using MoovTn.Domain.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Service.Interfaces
{
   public  interface ILinestationsService : IService<Linestation>
    {
        int GetStationOrder(int lineId,int stationId);
    }
}

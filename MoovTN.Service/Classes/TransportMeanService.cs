
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
    public class TransportMeanService : Service<TransportMean>, ITransportMeanService
    {
        public IEnumerable<TransportMean> GetAllByType(string type)
        {
            
            return GetMany(t=>t.type == type).OrderBy(t=>t.serial);
        }
    }
}

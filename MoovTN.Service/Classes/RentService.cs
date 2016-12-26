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
    public class RentService : Service<Rent>, IRentService
    {
        public bool isRent(TransportMean transport, DateTime rechDate)
        {
            IEnumerable<Rent> rents = GetMany(r=>((r.rentStart<=rechDate) && (r.rentEnd>=rechDate) && (r.transportmean.serial == transport.serial)));
            return rents.Any();
        }
    }
}

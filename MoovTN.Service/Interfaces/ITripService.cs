using MoovTn.Domain.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Service.Interfaces
{
    public interface ITripService : IService<Trip>
    {
        bool isOnTrip(TransportMean transport, DateTime tripStart, DateTime tripEnd);
        IEnumerable<Trip> GetTripByTime(DateTime d);


    }

}

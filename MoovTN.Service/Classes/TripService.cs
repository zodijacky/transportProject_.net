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
    public class TripService : Service<Trip>, ITripService
    {
        private IEnumerable<Trip> tripListe = null;

        public bool isOnTrip(TransportMean transport, DateTime tripStart, DateTime tripEnd)
        {
            IEnumerable<Trip> trips = GetMany((t=>((t.departureTime<= tripStart && t.arrivalTime>=tripStart) || (t.departureTime <= tripEnd && t.arrivalTime >= tripEnd)) && (t.transportmean.serial == transport.serial) ) );

            return trips.Any();
        }
        public IEnumerable<Trip> GetTripByTime(DateTime d)
        {
            DateTime oneday = d.AddDays(1);

            tripListe = GetMany(t => (t.arrivalTime >= d) && (t.arrivalTime <= oneday));

            return tripListe;
        }
    }
}

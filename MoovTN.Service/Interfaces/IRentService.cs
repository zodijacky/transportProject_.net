using MoovTn.Domain.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Service.Interfaces
{
    public interface IRentService : IService<Rent>
    {
        bool isRent(TransportMean transport, DateTime rechDate);
    }
}

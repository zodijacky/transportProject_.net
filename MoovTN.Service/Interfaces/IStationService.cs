using MoovTn.Domain.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Service.Interfaces
{
    public interface IStationService : IService<Station>
    {
        IEnumerable<Station> GetStationsByType(string type);
        string StationsListToJson(List<Station> liste);
        String findAllToJson();
      
        String findOneToJson(int id);

        IEnumerable<Station> GetManyByLine(int lineId);

    }
}

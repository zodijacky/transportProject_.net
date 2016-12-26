using MoovTn.Domain.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Service.Interfaces
{
    public interface ILineService : IService<Line>
    {
        IEnumerable<Line> GetLinesByType(String type);

        IEnumerable<Line> GetLinesByName(String type, String name);
        String LinesListToJson(List<Line> liste);

        String LinesPathsToJson(List<Line> liste);

        IEnumerable<Line> TestStationInLine(int ids1);
    }
}

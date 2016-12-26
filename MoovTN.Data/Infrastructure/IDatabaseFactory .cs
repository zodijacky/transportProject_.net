using MoovTn.Data;
using MoovTn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        transportdsContext DataContext { get; }
    }

}

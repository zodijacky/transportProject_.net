using MoovTn.Data;
using MoovTn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoovTn.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private transportdsContext dataContext;
        public transportdsContext DataContext { get { return dataContext; } }

        public DatabaseFactory()
        {
            dataContext = new transportdsContext();
        }
        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }

}

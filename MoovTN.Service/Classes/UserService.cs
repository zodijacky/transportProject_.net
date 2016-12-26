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
    public class UserService : Service<User>,IUserService
    {

    }
}

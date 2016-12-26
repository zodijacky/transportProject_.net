using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MoovTn.Domain.Models
{
    public partial class User: IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var UserIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return UserIdentity;
        }
        public User()
        {
          
            //this.claims = new List<Claim>();
            //this.rents = new List<Rent>();
            //this.subscriptioncards = new List<SubscriptionCard>();
            //this.lines = new List<Line>();
        }

        public string type_user { get; set; }
      //  public int id { get; set; }
        public DateTime birthdate { get; set; }
      //  public string email { get; set; }
         public int cin { get; set; }
        public string firstName { get; set; }
        public string job { get; set; }
        public string lastName { get; set; }

      

       
        public bool DisplayItem { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<Claim> claims { get; set; }
        public virtual ICollection<Rent> rents { get; set; }
        public virtual ICollection<SubscriptionCard> subscriptioncards { get; set; }
        public virtual ICollection<Line> lines { get; set; }
    }
}

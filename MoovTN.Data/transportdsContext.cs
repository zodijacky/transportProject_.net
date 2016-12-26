using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using MoovTn.Data.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;
using MoovTn.Domain.Models;

namespace MoovTn.Data
{
    public partial class transportdsContext : IdentityDbContext<User>
    {
        static transportdsContext()
        {
            Database.SetInitializer<transportdsContext>(null);
        }

        public transportdsContext()
            : base("Name=transportdsContext")
        {
        }
        public static transportdsContext Create()
        {

            return new transportdsContext();
        }
        public DbSet<Claim> claims { get; set; }
        public DbSet<Line> lines { get; set; }
        public DbSet<Linestation> linestations { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<Rent> rents { get; set; }
        public DbSet<Station> stations { get; set; }
        public DbSet<SubscriptionCard> subscriptioncards { get; set; }
        public DbSet<TransportMean> transportmeans { get; set; }
        public DbSet<Trip> trips { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new claimMap());
            modelBuilder.Configurations.Add(new lineMap());
            modelBuilder.Configurations.Add(new linestationMap());
            modelBuilder.Configurations.Add(new locationMap());
            modelBuilder.Configurations.Add(new notificationMap());
            modelBuilder.Configurations.Add(new rentMap());
            modelBuilder.Configurations.Add(new stationMap());
            modelBuilder.Configurations.Add(new subscriptioncardMap());
            modelBuilder.Configurations.Add(new transportmeanMap());
            modelBuilder.Configurations.Add(new tripMap());
            modelBuilder.Configurations.Add(new userMap());
        }
    }
}

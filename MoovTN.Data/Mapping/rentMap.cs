using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoovTn.Data.Mapping
{
    public class rentMap : EntityTypeConfiguration<Rent>
    {
        public rentMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.status)
                .HasMaxLength(255);

            this.Property(t => t.transportMean_serial)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("rent");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.members).HasColumnName("members");
            this.Property(t => t.rentEnd).HasColumnName("rentEnd");
            this.Property(t => t.rentStart).HasColumnName("rentStart");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.idDepart).HasColumnName("idDepart");
            this.Property(t => t.idDestination).HasColumnName("idDestination");
            this.Property(t => t.transportMean_serial).HasColumnName("transportMean_serial");
            this.Property(t => t.user_id).HasColumnName("user_id");

            // Relationships
            this.HasMany(t => t.transportmeans)
                .WithMany(t => t.rents1)
                .Map(m =>
                    {
                        m.ToTable("transportmean_rent");
                        m.MapLeftKey("rent_id");
                        m.MapRightKey("TransportMean_serial");
                    });

            this.HasOptional(t => t.location)
                .WithMany(t => t.rents)
                .HasForeignKey(d => d.idDepart);
            this.HasOptional(t => t.location1)
                .WithMany(t => t.rents1)
                .HasForeignKey(d => d.idDestination);
            this.HasOptional(t => t.transportmean)
                .WithMany(t => t.rents)
                .HasForeignKey(d => d.transportMean_serial);
            this.HasOptional(t => t.user)
                .WithMany(t => t.rents)
                .HasForeignKey(d => d.user_id);

        }
    }
}

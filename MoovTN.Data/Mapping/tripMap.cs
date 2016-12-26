using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace MoovTn.Data.Mapping
{
    public class tripMap : EntityTypeConfiguration<Trip>
    {
        public tripMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.transportMean_serial)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("trip");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.arrivalTime).HasColumnName("arrivalTime");
            this.Property(t => t.canceled).HasColumnName("canceled");
            this.Property(t => t.departureTime).HasColumnName("departureTime");
            this.Property(t => t.line_id).HasColumnName("line_id");
            this.Property(t => t.transportMean_serial).HasColumnName("transportMean_serial");

            // Relationships
            this.HasMany(t => t.lines)
                .WithMany(t => t.trips1)
                .Map(m =>
                    {
                        m.ToTable("line_trip");
                        m.MapLeftKey("trips_id");
                        m.MapRightKey("Line_id");
                    });

            this.HasOptional(t => t.line)
                .WithMany(t => t.trips)
                .HasForeignKey(d => d.line_id);
            this.HasOptional(t => t.transportmean)
                .WithMany(t => t.trips)
                .HasForeignKey(d => d.transportMean_serial);

        }
    }
}

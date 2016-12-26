using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoovTn.Data.Mapping
{
    public class linestationMap : EntityTypeConfiguration<Linestation>
    {
        public linestationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.line_fk, t.station_fk });

            // Properties
            this.Property(t => t.line_fk)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.station_fk)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("linestation");
            this.Property(t => t.line_fk).HasColumnName("line_fk");
            this.Property(t => t.station_fk).HasColumnName("station_fk");
            this.Property(t => t.ordre).HasColumnName("ordre");

            // Relationships
            this.HasRequired(t => t.line)
                .WithMany(t => t.linestations)
                .HasForeignKey(d => d.line_fk);
            this.HasRequired(t => t.station)
                .WithMany(t => t.linestations)
                .HasForeignKey(d => d.station_fk);

        }
    }
}

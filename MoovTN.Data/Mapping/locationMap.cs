using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoovTn.Data.Mapping
{
    public class locationMap : EntityTypeConfiguration<Location>
    {
        public locationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adress)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("location");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adress).HasColumnName("adress");
            this.Property(t => t.latitude).HasColumnName("latitude");
            this.Property(t => t.longitude).HasColumnName("longitude");
        }
    }
}

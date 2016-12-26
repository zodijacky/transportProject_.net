using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace MoovTn.Data.Mapping
{
    public class transportmeanMap : EntityTypeConfiguration<TransportMean>
    {
        public transportmeanMap()
        {
            // Primary Key
            this.HasKey(t => t.serial);

            // Properties
            this.Property(t => t.serial)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.state)
                .HasMaxLength(255);

            this.Property(t => t.type)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("transportmean");
            this.Property(t => t.serial).HasColumnName("serial");
            this.Property(t => t.state).HasColumnName("state");
            this.Property(t => t.type).HasColumnName("type");
        }
    }
}

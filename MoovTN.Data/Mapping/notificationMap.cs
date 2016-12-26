using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoovTn.Data.Mapping
{
    public class notificationMap : EntityTypeConfiguration<Notification>
    {
        public notificationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.description)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("notification");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.broadcast).HasColumnName("broadcast");
            this.Property(t => t.creationDate).HasColumnName("creationDate");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.level).HasColumnName("level");
            this.Property(t => t.idLine).HasColumnName("idLine");

            // Relationships
            this.HasOptional(t => t.line)
                .WithMany(t => t.notifications)
                .HasForeignKey(d => d.idLine);

        }
    }
}

using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoovTn.Data.Mapping
{
    public class lineMap : EntityTypeConfiguration<Line>
    {
        public lineMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .HasMaxLength(255);

            this.Property(t => t.path)
                .HasMaxLength(65535);

            this.Property(t => t.type)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("line");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.path).HasColumnName("path");
            this.Property(t => t.type).HasColumnName("type");

            // Relationships
            this.HasMany(t => t.users)
                .WithMany(t => t.lines)
                .Map(m =>
                    {
                        m.ToTable("user_line");
                        m.MapLeftKey("lines_id");
                        m.MapRightKey("USER_id");
                    });


        }
    }
}

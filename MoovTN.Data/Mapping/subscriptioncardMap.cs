using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace MoovTn.Data.Mapping
{
    public class subscriptioncardMap : EntityTypeConfiguration<SubscriptionCard>
    {
        public subscriptioncardMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("subscriptioncard");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.expired).HasColumnName("expired");
            this.Property(t => t.locked).HasColumnName("locked");
            this.Property(t => t.validityEnd).HasColumnName("validityEnd");
            this.Property(t => t.validityStart).HasColumnName("validityStart");
            this.Property(t => t.user_id).HasColumnName("user_id");

            // Relationships
            this.HasMany(t => t.lines)
                .WithMany(t => t.subscriptioncards)
                .Map(m =>
                    {
                        m.ToTable("subscriptioncard_line");
                        m.MapLeftKey("SubscriptionCard_id");
                        m.MapRightKey("lines_id");
                    });

            this.HasOptional(t => t.user)
                .WithMany(t => t.subscriptioncards)
                .HasForeignKey(d => d.user_id);

        }
    }
}

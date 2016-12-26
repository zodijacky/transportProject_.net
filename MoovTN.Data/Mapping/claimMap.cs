using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoovTn.Data.Mapping
{
    public class claimMap : EntityTypeConfiguration<Claim>
    {
        public claimMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.question)
                .HasMaxLength(255);

            this.Property(t => t.response)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("claim");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.claimDate).HasColumnName("claimDate");
            this.Property(t => t.question).HasColumnName("question");
            this.Property(t => t.questionRead).HasColumnName("questionRead");
            this.Property(t => t.response).HasColumnName("response");
            this.Property(t => t.responseRead).HasColumnName("responseRead");
            this.Property(t => t.users_id).HasColumnName("users_id");

            // Relationships
            this.HasOptional(t => t.user)
                .WithMany(t => t.claims)
                .HasForeignKey(d => d.users_id);

        }
    }
}

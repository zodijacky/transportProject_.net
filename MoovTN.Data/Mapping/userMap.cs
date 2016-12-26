using MoovTn.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoovTn.Data.Mapping
{
    public class userMap : EntityTypeConfiguration<User>
    {
        public userMap()
        {
           
           

            // Table & Column Mappings
            this.ToTable("user");
            this.Property(t => t.type_user).HasColumnName("type_user");
           
            this.Property(t => t.birthdate).HasColumnName("birthdate");
            this.Property(t => t.firstName).HasColumnName("FirstName");
            this.Property(t => t.lastName).HasColumnName("LastName");
            this.Property(t => t.cin).HasColumnName("Cin");

            this.Property(t => t.job).HasColumnName("job");
           // this.Property(t => t.i).HasColumnName("image");

        }
    }
}

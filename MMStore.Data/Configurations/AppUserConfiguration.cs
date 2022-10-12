using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMStore.Entities;

namespace MMStore.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a => a.Name).HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(a => a.Surname).HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(a => a.Email).HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(a => a.Phone).HasColumnType("varchar(15)").HasMaxLength(15);
            builder.Property(a => a.Username).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(a => a.Password).HasColumnType("nvarchar(50)").HasMaxLength(50);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Auth;

namespace Data.Configurations.Auth
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles);

            builder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles);
        }
    }
}

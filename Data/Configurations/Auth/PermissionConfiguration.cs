using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Auth;
using Permission = Data.Models.Auth.Permission;

namespace Data.Configurations.Auth
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(p => p.PermissionTypeId)
                .HasConversion<int>();

            builder.HasKey(p => p.PermissionTypeId);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(p => p.Roles)
                .WithMany(r => r.Permissions);
        }
    }
}

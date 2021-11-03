using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Models.Species;

namespace Data.Configurations.Species
{
    class UsersGroupsConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(ug => ug.Id);

            builder.HasOne(ug => ug.User);

            builder.HasOne(ug => ug.Group);

            builder.HasOne(ug => ug.MemberType);
        }
    }
}

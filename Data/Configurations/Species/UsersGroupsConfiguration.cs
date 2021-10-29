using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Species;

namespace Data.Configurations.Species
{
    class UsersGroupsConfiguration : IEntityTypeConfiguration<UsersGroups>
    {
        public void Configure(EntityTypeBuilder<UsersGroups> builder)
        {
            builder.HasKey(ug => ug.Id);

            builder.HasOne(ug => ug.User);

            builder.HasOne(ug => ug.Group);

            builder.HasOne(ug => ug.MemberType);
        }
    }
}

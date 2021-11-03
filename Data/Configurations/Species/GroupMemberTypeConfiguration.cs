using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Species;
using GroupMemberType = Data.Models.Species.GroupMemberType;

namespace Data.Configurations.Species
{
    class GroupMemberTypeConfiguration : IEntityTypeConfiguration<GroupMemberType>
    {
        public void Configure(EntityTypeBuilder<GroupMemberType> builder)
        {
            builder.HasKey(gmt => gmt.Id);

            builder.Property(gmt => gmt.MemberTypeId)
                .IsRequired();

            builder.Property(gmt => gmt.Title)
                .IsRequired();
        }
    }
}

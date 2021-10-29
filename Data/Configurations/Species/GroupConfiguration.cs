using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Species;

namespace Data.Configurations.Species
{
    class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(g => g.Description)
                .HasMaxLength(1024);

            builder.HasMany(g => g.Subjects)
                .WithOne(s => s.Group);
        }
    }
}
